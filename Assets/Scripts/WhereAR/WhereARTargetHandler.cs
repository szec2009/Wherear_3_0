using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Vuforia;

namespace WhereAR
{
	public class WhereARTargetHandler : MonoBehaviour {

        //Please go to below link add ARID
        //http://devwherear.com/arms/v2/ARMS/artarget/
        public string ARID = "";

		public GameObject rootObject;
		public GameObject imageTarget;
		public WhereARObject infoObject;

		//Target Child Object
		public GameObject videoPlayer;
		public GameObject greenVideoPlayer;
		public GameObject images;
		public GameObject playFullScreen;

		//Buttons
		public GameObject buttonEle;
		public List<GameObject> buttonObjs = new List<GameObject>();
		List<GameObject> readyButtons = new List<GameObject>();

		//Other Object
		public List<GameObject> scanWhenDisableObj = new List<GameObject>();
		public List<GameObject> scanWhenEnableObj = new List<GameObject> ();
		public List<GameObject> lostWhenDisableObj = new List<GameObject> ();
		public List<GameObject> lostWhenEnableObj = new List<GameObject> ();

		//Selfie
		public GameObject selfieImage;
		public GameObject reScanButotn;

		//Hold and Release
		public GameObject holdAndReleaseObj;
		public bool isLock = false;

		//Language
		public List<UnityEngine.UI.Button> languageBtns;
		public GameObject changeLanguageEle;

		private bool isLoad = false;
		ScreenOrientation ori;
		string currentLang;

		void Start()
		{
			currentLang = WhereARLanguage.GetLang ();
			ori = Screen.orientation;
		}

		void Update()
		{
			if(currentLang != WhereARLanguage.GetLang())
			{
				currentLang = WhereARLanguage.GetLang ();
				if(imageTarget.GetComponent<ImageTargetBehaviour>().enabled || isLock)
				{
					SetARChildObject(infoObject.obj);
				}
			}
			if(ori != Screen.orientation)
			{
				ori = Screen.orientation;
				SetTargetLang (infoObject.obj);
			}
		}

		void OnBecameVisible()
		{

			ori = Screen.orientation;
			WhereARCameraFocus.isSetTapFoucs = false;
			foreach(GameObject obj in scanWhenDisableObj)
			{
				obj.SetActive (false);
			}

			foreach(GameObject obj in scanWhenEnableObj)
			{
				obj.SetActive (true);
			}

			infoObject = new WhereARObject ();
			infoObject.obj = new ARObject ();
			SetARObjectInfo ();	
		}

		void OnBecameInvisible()
		{

			WhereARCameraFocus.isSetTapFoucs = true;
			foreach(GameObject obj in lostWhenDisableObj)
			{
				obj.SetActive (false);
			}

			foreach(GameObject obj in lostWhenEnableObj)
			{
				obj.SetActive (true);
			}
			InactiveGreenVideo ();
			InactiveImage ();
			InactiveVideo ();
			RemoveAllChildInParent (buttonEle);
			RemoveAllChildInParent (changeLanguageEle.gameObject);
		}

		void SetARObjectInfo()
		{
			WhereAR2ARMS toARMS = new WhereAR2ARMS ();
			StartCoroutine (toARMS.MakeRecoRequest(
				ARID, //ARID
				"", //VuforiaID
				"RECO", //Action
				callback =>
				{
					infoObject.obj = callback;
					isLoad = true;
					SetARChildObject(infoObject.obj);
				}
			));
		}

		void SetARChildObject(ARObject obj)
		{
			videoPlayer = rootObject.transform.Find ("VideoPlayer").gameObject;
			greenVideoPlayer = rootObject.transform.Find ("GreenVideoPlayer").gameObject;
			images = rootObject.transform.Find ("Images").gameObject;
			buttonEle = rootObject.transform.Find ("Buttons").gameObject;

			InactiveGreenVideo ();
			InactiveImage ();
			InactiveVideo ();

			RemoveAllChildInParent (buttonEle);
			readyButtons.Clear ();

			SetTargetLang (obj);

			List<ARObjectInfo> objInfoes = new List<ARObjectInfo> ();
			switch(WhereARLanguage.GetLang())
			{
			case "en":
				objInfoes = infoObject.obj.en;
				break;
			case "tc":
				objInfoes = infoObject.obj.tc;
				break;
			case "sc":
				objInfoes = infoObject.obj.sc;
				break;
			}


			foreach(ARObjectInfo objInfo in objInfoes)
			{
				LoadButton (objInfo.buttonType);
			}
			int count = 0;
			foreach (GameObject button in readyButtons) {
				float _x = count * 0.15f;
				Vector3 newPos = new Vector3 (
					_x,
					0.0f,
					0.01f
				);
				GameObject _button = Instantiate (button, buttonEle.transform);
				_button.transform.localPosition = newPos;
				_button.GetComponent<WhereARButtonHandler> ().reScanBtn = reScanButotn;
				_button.GetComponent<WhereARButtonHandler> ().holdAndReleaseObj = holdAndReleaseObj;
				_button.GetComponent<WhereARButtonHandler> ().targetHandler = this;
				_button.GetComponent<WhereARButtonHandler> ().buttonID = count.ToString();
				count++;
			}

			switch (infoObject.obj.ARType) {
				case ARObejctType.WHEREAR_NONE:
				default:
					break;
				case ARObejctType.WHEREAR_IMAGE_SHOW:
					{
					ActiveImage (infoObject.obj.GetFirstImages(currentLang));
					}
					break;
				case ARObejctType.WHEREAR_VIDEO:
					{
					ActiveVideo (infoObject.obj.GetFirstVideo(currentLang), infoObject.obj.GetFirstVideoObjIndex(currentLang));
					}
					break;
				case ARObejctType.WHEREAR_GREENVIDEO:
					{
					ActiveGreenVideo (infoObject.obj.GetFirstGreenVideo(currentLang));
					}
					break;
			}
		}

		void SetTargetLang(ARObject obj)
		{

			RemoveAllChildInParent (changeLanguageEle.gameObject);
			int langBtnCount = 0;
			if(obj.en.Count > 0)
			{
				UnityEngine.UI.Button _button =  Instantiate (languageBtns [0], changeLanguageEle.transform);
				if (Screen.orientation == ScreenOrientation.Landscape || Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight) 
				{
					float posX = -120 * langBtnCount;
					RectTransform transform = _button.gameObject.transform as RectTransform;
					Vector2 position = transform.anchoredPosition;
					transform.anchoredPosition = new Vector2(posX, 0);
				}
				else
				{
					float posY = -120 * langBtnCount;
					RectTransform transform = _button.gameObject.transform as RectTransform;
					Vector2 position = transform.anchoredPosition;
					transform.anchoredPosition = new Vector2(0, posY);
				}
				langBtnCount++;
			}

			if(obj.tc.Count > 0)
			{
				UnityEngine.UI.Button _button =  Instantiate (languageBtns [1], changeLanguageEle.transform);
				if (Screen.orientation == ScreenOrientation.Landscape || Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight) 
				{
					float posX = -120 * langBtnCount;
					RectTransform transform = _button.gameObject.transform as RectTransform;
					Vector2 position = transform.anchoredPosition;
					transform.anchoredPosition = new Vector2(posX, 0);
				}
				else
				{
					float posY = -120 * langBtnCount;
					RectTransform transform = _button.gameObject.transform as RectTransform;
					Vector2 position = transform.anchoredPosition;
					transform.anchoredPosition = new Vector2(0, posY);
				}
				langBtnCount++;
			}

			if(obj.sc.Count > 0)
			{
				UnityEngine.UI.Button _button =  Instantiate (languageBtns [2], changeLanguageEle.transform);
				if (Screen.orientation == ScreenOrientation.Landscape || Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight) 
				{
					float posX = -120 * langBtnCount;
					RectTransform transform = _button.gameObject.transform as RectTransform;
					Vector2 position = transform.anchoredPosition;
					transform.anchoredPosition = new Vector2(posX, 0);
				}
				else
				{
					float posY = -120 * langBtnCount;
					RectTransform transform = _button.gameObject.transform as RectTransform;
					Vector2 position = transform.anchoredPosition;
					transform.anchoredPosition = new Vector2(0, posY);
				}
				langBtnCount++;
			}
		}

		void LoadButton(ARButtonType buttonType)
		{
			GameObject _button = null;
			switch(buttonType)
			{
			case ARButtonType.WHEREAR_VIDEO:
				{
					_button = buttonObjs[5];
				}
				break;
			case ARButtonType.WHEREAR_SELFIE:
				{
					_button = buttonObjs[10];
				}
				break;
			case ARButtonType.WHEREAR_TAKEPHOTO:
				{
					_button = buttonObjs[7];
				}
				break;
			case ARButtonType.WHEREAR_GREENMOVIE:
				{
					_button = buttonObjs[11];
				}
				break;
			case ARButtonType.WHEREAR_FACEBOOK:
				{
					_button = buttonObjs[1];
				}
				break;
			case ARButtonType.WHEREAR_IG:
				{
					_button = buttonObjs[8];
				}
				break;
			case ARButtonType.WHEREAR_IMAGES:
				{
					_button = buttonObjs[2];
				}
				break;
			case ARButtonType.WHEREAR_WEBLINK:
				{
					_button = buttonObjs[8];
				}
				break;
			case ARButtonType.WHEREAR_EMAIL:
				{
					_button = buttonObjs[0];
				}
				break;
			case ARButtonType.WHEREAR_PHONE:
				{
					_button = buttonObjs[6];
				}
				break;
			case ARButtonType.WHEREAR_WHATSAPP:
				{
					_button = buttonObjs[9];
				}
				break;
			case ARButtonType.WHEREAR_LOCATION:
				{
					_button = buttonObjs[4];
				}
				break;
			}
			readyButtons.Add (_button);
		}

		public void ActiveVideo(string url, int buttonID)
		{
			playFullScreen.GetComponent<WhereARButtonHandler> ().buttonID = buttonID.ToString();
			InactiveGreenVideo ();
			InactiveImage ();
			InactiveSelfie ();
			InactiveVideo ();
			videoPlayer.SetActive (true);
			videoPlayer.GetComponent<HighQualityPlayback> ().videoId = url;
			videoPlayer.GetComponent<HighQualityPlayback> ().PlayYoutubeVideo (url);
		}

		public void ActiveGreenVideo(string url)
		{
			InactiveGreenVideo ();
			InactiveImage ();
			InactiveSelfie ();
			InactiveVideo ();
			greenVideoPlayer.SetActive (true);
			greenVideoPlayer.GetComponent<HighQualityPlayback> ().videoId = url;
			greenVideoPlayer.GetComponent<HighQualityPlayback> ().PlayYoutubeVideo (url);
		}

		public void ActiveImage(List<ARObjectImage> imageObjs)
		{
			InactiveGreenVideo ();
			InactiveImage ();
			InactiveSelfie ();
			InactiveVideo ();
			images.SetActive (true);
			images.GetComponent<WhereARImageListHandler> ().images = imageObjs;
			images.GetComponent<WhereARImageListHandler> ().LoadImages ();
		}

		public void ActiveSelfie(List<ARObjectImage> imageObjs)
		{
			InactiveVideo ();
			InactiveGreenVideo ();
			InactiveImage ();
			selfieImage.SetActive (true);
			selfieImage.GetComponent<WhereARRandomImage> ().images = imageObjs;
		}


		public void InactiveVideo()
		{
			videoPlayer.GetComponent<VideoPlayer> ().enabled = true;
			videoPlayer.GetComponent<VideoPlayer> ().Stop ();
			videoPlayer.SetActive (false);
		}

		public void InactiveGreenVideo()
		{
			greenVideoPlayer.GetComponent<VideoPlayer> ().enabled = true;
			greenVideoPlayer.GetComponent<VideoPlayer> ().Stop ();
			greenVideoPlayer.SetActive (false);
		}

		public void InactiveImage()
		{
			images.GetComponent<WhereARImageListHandler> ().RemoveImages ();
			images.SetActive (false);
		}

		public void InactiveSelfie()
		{
			selfieImage.SetActive (false);
		}

		public void SetHoldAndReleaseObject()
		{
			GameObject copyObj = Instantiate (rootObject, holdAndReleaseObj.transform);
			copyObj.transform.Find ("Lock").gameObject.SetActive (false);
			copyObj.transform.Find ("UnLock").gameObject.SetActive (true);
			copyObj.transform.Find ("TargetManager").gameObject.GetComponent<WhereARTargetHandler> ().isLock = true;
			holdAndReleaseObj.SetActive (true);
			ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
			ChangeImageBehaviourStatus (false);
			tracker.TargetFinder.Stop ();
			tracker.Stop ();
		}

		void ChangeImageBehaviourStatus(bool status)
		{
			ImageTargetBehaviour[] imageTargets = Object.FindObjectsOfType<ImageTargetBehaviour> ();
			foreach(ImageTargetBehaviour imageTarget in imageTargets)
			{
				imageTarget.enabled = status;
			}
		}

		public void CleaseHoldAndReleaseObject()
		{			
			RemoveAllChildInParent (holdAndReleaseObj);
			holdAndReleaseObj.SetActive (false);
			ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
			tracker.Start ();
			ChangeImageBehaviourStatus (true);
		}

		void RemoveAllChildInParent(GameObject obj)
		{
			Transform[] ts = obj.GetComponentsInChildren<Transform> ();

			for(int i = 1; i < ts.Length; i++)
			{
				Destroy (ts[i].gameObject);
			}
		}

	}

}

