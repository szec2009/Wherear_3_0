using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

using UnityEngine.UI;
using System.Linq;

namespace WhereAR
{
	public class WhereARTakePhoto : MonoBehaviour {


		public GameObject mainPlane;

		public NativeShare nativeShare;

		public List<GameObject> actionWhenDisableObjs = new List<GameObject> ();
		public List<GameObject> actionWhenEnableObjs = new List<GameObject> ();
		public List<GameObject> enableWhenBackObj = new List<GameObject> ();

		//Preview Dialog
		public GameObject previewDialog;
		public UnityEngine.UI.Image photo;
//		public GameObject phoneEle;
//		public GameObject waitingDialog;

		bool saved = false;
		bool saving = false;
		string photoPath = "";
		string url;
		string fileName = "";

		public static string msg = "";

		// Use this for initialization
		void Start () {
			WhereARScreenshotManager.reset ();
			WhereARScreenshotManager.ScreenshotFinishedSaving += ScreenshotSaved;             
			WhereARScreenshotManager.ImageFinishedSaving += ImageSaved;
			WhereARScreenshotManager.ImageNotSave += ImageFail;
		}

		IEnumerator SaveAssetImage ()
		{           
			Renderer renderer = mainPlane.GetComponent<Renderer> ();
			Texture2D _tex = renderer.material.mainTexture as Texture2D;
			DateTime now = DateTime.Now;
			byte[] bytes = _tex.EncodeToPNG ();           
			string path = Application.persistentDataPath + "/WhereAR_" + now.ToString("yyyyMMddhhmmssfff") + ".png";         
			File.WriteAllBytes (path, bytes);                      
			yield return new WaitForEndOfFrame ();          
			StartCoroutine (WhereARScreenshotManager.SaveExisting (path, true));
		}

		IEnumerator ShowDialog ()
		{        
//			waitingDialog.SetActive (true);
			yield return new WaitForSeconds(1.0f);
//			waitingDialog.SetActive (false);
			photo.sprite = null;
			StartCoroutine (ShowTakenPhoto());
		}

		public void TakePhoto()
		{
			foreach(GameObject obj in actionWhenDisableObjs)
			{
				obj.SetActive (false);
			}
			DateTime now = DateTime.Now;
			fileName = "WhereAR_" + now.ToString("yyyyMMddhhmmssfff");
			StartCoroutine (WhereARScreenshotManager.Save((returnValue) => { photoPath = returnValue; },fileName, "WhereAR", true));
		}

		IEnumerator ShowTakenPhoto()
		{
			//tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
			previewDialog.SetActive (true);
			//url = photoPath;

			#if UNITY_ANDROID

			url = "file://" + "/sdcard/DCIM/WhereAR/"+photoPath.Split('/').Last();
			#endif

			#if UNITY_IOS
			url = "file://" + Application.persistentDataPath + "/" +photoPath.Split('/').Last();
			#endif

			WWW www = new WWW(url);
			yield return www;
//			phoneEle.SetActive (false);
			photo.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));

		}

		void ScreenshotSaved()
		{
			saving = true;
			StartCoroutine (ShowDialog ());
		}

		void ImageSaved()
		{
			saved = true;
		}

		void ImageFail()
		{
		}

		public void ClosePhotoPanel(){

			foreach(GameObject obj in enableWhenBackObj)
			{
				obj.SetActive (true);
			}
			previewDialog.SetActive (false);
			photo.sprite = null;

		}

		public void PhotoShare(){
			msg += "\nStart Photo Share";
			#if UNITY_ANDROID
			nativeShare.ScreenshotName = url.Substring(7);
			#endif
			#if UNITY_IOS
			nativeShare.ScreenshotName = photoPath.Split('/').Last();
			#endif
			msg += "\nShare Name: " + nativeShare.ScreenshotName;
			nativeShare.Share ();
			msg += "\nEnd Photo Share";
		}

//		void OnGUI()
//		{
//			GUI.Label (new Rect(0, 600, Screen.width, Screen.height), "This is testing: " + msg + " - " + photoPath);
//		}
	}
}
