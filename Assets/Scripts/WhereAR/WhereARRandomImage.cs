using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhereAR
{

	public class WhereARRandomImage : MonoBehaviour {

		public Material material;
		public List<ARObjectImage> images;
		public Vector3 defaultPosition;
		public Vector3 defaultScale;
		public Quaternion defaultRotate;

		public GameObject[] enableWhenShowObjs;
		public GameObject[] disableWhenShowObjs;
		public GameObject[] disableWhenLostObjs;
		public GameObject[] enableWhenLostObjs;

		public enum ItemPosition{
			NONE,
			TOP_RIGHT,
			TOP_MIDDLE,
			TOP_LEFT,
			MIDDLE_RIGHT,
			MIDDLE,
			MIDDLE_LEFT,
			BOTTOM_RIGHT,
			BOTTOM_MIDDLE,
			BOTTOM_LEFT
		}

		void OnBecameVisible()
		{
			foreach(GameObject obj in enableWhenShowObjs)
			{
				obj.SetActive (true);
			}

			foreach(GameObject obj in disableWhenShowObjs)
			{
				obj.SetActive (false);
			}

			defaultPosition = gameObject.transform.localPosition;
			defaultScale = gameObject.transform.localScale;
			defaultRotate = gameObject.transform.localRotation;

			int random = Random.Range (0, images.Count);
			ARObjectImage currentImage = images [random];
			StartCoroutine (loadImageFromUrl(currentImage));
		}

		public IEnumerator loadImageFromUrl(ARObjectImage currentImage)
		{
			Texture2D tex;
			tex = new Texture2D(4, 4, TextureFormat.DXT5, false);
			using (WWW www = new WWW(currentImage.ImageUrl))
			{
				yield return www;
				www.LoadImageIntoTexture(tex);

				float _imageW = tex.width;
				float _imageH = tex.height;

				float texW = 1.0f;
				float texH = 1.0f;

				if (_imageW >= _imageH) {
					texH = _imageH / _imageW;
				} else {
					texW = _imageW / _imageH;
				}

				if(texH >= 0.6f)
				{
					float scaleVal = texH / 0.6f;
					texH = 0.6f;
					texW = texW / scaleVal;
				}

				Sprite sprite = Sprite.Create (tex, new Rect (0, 0, currentImage.width, currentImage.height), new Vector2 (0.5f, 0.5f), 100.0f);
				GetComponent<Renderer> ().material.mainTexture = sprite.texture;
				gameObject.transform.localScale = new Vector3 (texW, 1.0f, texH);
			}		
		}

		void OnBecameInvisible()
		{
			foreach(GameObject obj in disableWhenLostObjs)
			{
				obj.SetActive (false);
			}
			foreach(GameObject obj in enableWhenLostObjs)
			{
				obj.SetActive (true);
			}

			gameObject.transform.localPosition = defaultPosition;
			gameObject.transform.localScale = defaultScale;
			gameObject.transform.localRotation = defaultRotate;
		}

	}
}
