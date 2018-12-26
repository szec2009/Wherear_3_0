using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhereAR
{
	public class WhereARImageListHandler : MonoBehaviour {

		public List<ARObjectImage> images;
		public GameObject imageItemObj;
		public GameObject imageList;
		public GameObject HnextBtn;
		public GameObject HprevBtn;
		public GameObject VnextBtn;
		public GameObject VprevBtn;

		ScreenOrientation ori;

		GameObject _image;
		int currentImage = 0;
		void Start()
		{
			ori = Screen.orientation;
		}
		void Update()
		{
			if(ori != Screen.orientation)
			{
				ori = Screen.orientation;
				ShowButton ();
			}
		}

		// Use this for initialization
		public void LoadImages(bool isCreate = true)
		{
			if(images.Count > 0)
			{
				Vector3 oriPos = imageList.transform.localPosition;
				if (isCreate) {
					_image = Instantiate (imageItemObj, imageList.transform);
					_image.transform.GetChild (0).GetComponent<WhereARImageAction> ().imageUrl = images [currentImage].ImageUrl;
					_image.transform.GetChild (0).GetComponent<WhereARImageAction> ().autoFit = true;
					_image.transform.localPosition = new Vector3 (0, 0, 0);
				} else {
					_image.transform.GetChild (0).GetComponent<WhereARImageAction> ().imageUrl = images [currentImage].ImageUrl;
					_image.transform.GetChild (0).GetComponent<WhereARImageAction> ().autoFit = true;
					_image.transform.GetChild (0).GetComponent<WhereARImageAction> ().StartCoroutine ("loadImageFromUrl");
				}
			}

			ori = Screen.orientation;
			ShowButton ();
		}

		public void ShowButton()
		{
			if (currentImage == 0) {
				HprevBtn.SetActive (false);
				VprevBtn.SetActive (false);
			} else {
				HprevBtn.SetActive (true);
				VprevBtn.SetActive (true);
			}

			if (currentImage == (images.Count - 1)) {
				HnextBtn.SetActive (false);
				VnextBtn.SetActive (false);
			} else {
				HnextBtn.SetActive (true);
				VnextBtn.SetActive (true);
			}

			if (ori == ScreenOrientation.Landscape || ori == ScreenOrientation.LandscapeLeft || ori == ScreenOrientation.LandscapeRight) {
				VnextBtn.SetActive (false);
				VprevBtn.SetActive (false);
			} else {
				HnextBtn.SetActive (false);
				HprevBtn.SetActive (false);
			}
		}


		public void RemoveImages()
		{
			Transform[] ts = imageList.GetComponentsInChildren<Transform> ();

			for(int i = 1; i < ts.Length; i++)
			{
				Destroy (ts[i].gameObject);
			}
		}

		public void NextImage()
		{
			if(currentImage < (images.Count - 1))
			{
				currentImage++;
				LoadImages (false);
			}
		}

		public void PrevImage()
		{
			if(currentImage > 0)
			{
				currentImage--;
				LoadImages (false);
			}
		}

	}
}
