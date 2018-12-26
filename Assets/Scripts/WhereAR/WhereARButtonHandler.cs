using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using YoutubeLight;

namespace WhereAR
{
	public class WhereARButtonHandler : MonoBehaviour {

		public string buttonID = "";
		public ButtonAction buttonAction;
		public WhereARTargetHandler targetHandler;
		public GameObject reScanBtn;
		public GameObject holdAndReleaseObj;

		public bool isClickable = true;
		public List<GameObject> DisableWhenScans = new List<GameObject>();
		public List<GameObject> EnableWhenScans = new List<GameObject>();
		public List<GameObject> DisableWhenLose = new List<GameObject>();
		public List<GameObject> EnableWhenLose = new List<GameObject>();

		string msg = "";

		public enum ButtonAction
		{
			WHEREAR_NONE,
			WHEREAR_VIDEO,
			WHEREAR_SELFIE,
			WHEREAR_TAKEPHOTO,
			WHEREAR_GREENMOVIE,
			WHEREAR_FACEBOOK,
			WHEREAR_IG,
			WHEREAR_IMAGES,
			WHEREAR_WEBLINK,
			WHEREAR_EMAIL,
			WHEREAR_PHONE,
			WHEREAR_WHATSAPP,
			WHEREAR_LOCATION,
			WHEREAR_HOLD_AND_RELEASE_LOCK,
			WHEREAR_HOLD_AND_RELEASE_UNLOCK,
			WHEREAR_PLAY_FULLSCREEN
		}

		public void BackToIonic()
		{
			msg += "\nBack";
			//IonicComms.FinishActivity ();
		}

		public void ReScan()
		{
			reScanBtn.SetActive (false);
			TrackerManager.Instance.GetTracker<ObjectTracker> ().Start ();
			ChangeImageBehaviourStatus (true);

			targetHandler.InactiveSelfie ();

			foreach(GameObject obj in EnableWhenLose)
			{
				obj.SetActive (true);
			}
		}

		public void holdAndRelease()
		{
			ChangeImageBehaviourStatus (false);
			TrackerManager.Instance.GetTracker<ObjectTracker> ().Stop ();
		}

		void OnMouseDown()
		{
			WhereARAction wherearAction = new WhereARAction ();

			ARObjectInfo currentObj = new ARObjectInfo ();
			if(!string.IsNullOrEmpty(buttonID))
			{
				currentObj = targetHandler.infoObject.obj.GetARObjectInfoByIndex (
					int.Parse(buttonID), WhereARLanguage.GetLang()
				);
			}

			if(isClickable)
			{
				switch(buttonAction)
				{
				case ButtonAction.WHEREAR_NONE:
					{
					}
					break;
				case ButtonAction.WHEREAR_VIDEO:
					{
						targetHandler.ActiveVideo (currentObj.VideoUrls[0], int.Parse(buttonID));
					}
					break;
				case ButtonAction.WHEREAR_SELFIE:
					{
						reScanBtn.SetActive (true);
						reScanBtn.GetComponent<WhereARButtonHandler> ().targetHandler = targetHandler;
						targetHandler.ActiveSelfie (currentObj.Images);
						ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
						ChangeImageBehaviourStatus (false);
						tracker.Stop ();
						holdAndReleaseObj.SetActive (false);
					}
					break;
				case ButtonAction.WHEREAR_GREENMOVIE:
					{
						targetHandler.ActiveGreenVideo (currentObj.VideoUrls [0]);
					}
					break;
				case ButtonAction.WHEREAR_FACEBOOK:
					{
						string facebook = currentObj.Info;
						wherearAction.OnFacebookClick (facebook);
					}
					break;
				case ButtonAction.WHEREAR_IG:
					{
						string ig = currentObj.Info;
						wherearAction.OnWebUrlClick (ig);
					}
					break;
				case ButtonAction.WHEREAR_IMAGES:
					{
						targetHandler.ActiveImage (currentObj.Images);
//						wherearAction.OnImageClick ();
					}
					break;
				case ButtonAction.WHEREAR_WEBLINK:
					{
						string weburl = currentObj.Info;
						wherearAction.OnWebUrlClick (weburl);
					}
					break;
				case ButtonAction.WHEREAR_EMAIL:
					{
						string email = currentObj.Info;
						wherearAction.OnEmailClick (email);
					}
					break;
				case ButtonAction.WHEREAR_PHONE:
					{
						string phone = currentObj.Info;
						wherearAction.OnPhoneClick (phone);
					}
					break;
				case ButtonAction.WHEREAR_WHATSAPP:
					{
						string whatsapp = currentObj.Info;
						wherearAction.OnWhatsappClick (whatsapp);
					}
					break;
				case ButtonAction.WHEREAR_LOCATION:
					{
						wherearAction.OnLocationClick ();
					}
					break;
				case ButtonAction.WHEREAR_HOLD_AND_RELEASE_LOCK:
					{
						targetHandler.SetHoldAndReleaseObject ();
					}
					break;
				case ButtonAction.WHEREAR_HOLD_AND_RELEASE_UNLOCK:
					{
						targetHandler.CleaseHoldAndReleaseObject ();
					}
					break;
				case ButtonAction.WHEREAR_PLAY_FULLSCREEN:
					{
//						ChangeImageBehaviourStatus (false);
						targetHandler.videoPlayer.GetComponent<HandheldPlayback> ().PlayVideo (currentObj.VideoUrls [0],
						targetHandler.videoPlayer.GetComponent<HighQualityPlayback> ().OnVideoFinished);
					}
					break;
				}
			}
		}

		void ChangeImageBehaviourStatus(bool status)
		{
			ImageTargetBehaviour[] imageTargets = Object.FindObjectsOfType<ImageTargetBehaviour> ();
			foreach(ImageTargetBehaviour imageTarget in imageTargets)
			{
				imageTarget.enabled = status;
			}
		}

		void OnGUI()
		{
//			GUI.Label (new Rect(0, 0, Screen.width, Screen.height), "Test: " + msg);
		}
	}
}

