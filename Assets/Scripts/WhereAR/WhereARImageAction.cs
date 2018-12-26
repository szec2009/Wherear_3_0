using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhereAR
{
	public class WhereARImageAction : MonoBehaviour {

		public string imageUrl = "";
		public bool autoFit = true;

		void OnBecameVisible()
		{
			StartCoroutine ("loadImageFromUrl");
		}

		public IEnumerator loadImageFromUrl()
		{
			Texture2D tex;
			tex = new Texture2D(4, 4, TextureFormat.DXT5, false);
			using (WWW www = new WWW(imageUrl))
			{
				yield return www;
				www.LoadImageIntoTexture(tex);

				if(autoFit)
				{
					float _imageW = tex.width;
					float _imageH = tex.height;

					float texW = 1.0f;
					float texH = 1.0f;

					if (_imageW >= _imageH) {
						texH = _imageH / _imageW;
					} else {
						texW = _imageW / _imageH;
					}

					Debug.Log (texH + " - " + texW);
					if(texH >= 0.6f)
					{
						float scaleVal = texH / 0.6f;
						texH = 0.6f;
						texW = texW / scaleVal;

					}
					transform.localScale = new Vector3 (texW, 1.0f, texH);
				}
				GetComponent<Renderer>().material.mainTexture = tex;
			}		
		}
	}
}
