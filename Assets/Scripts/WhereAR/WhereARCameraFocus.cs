using UnityEngine;
using System.Collections;
using Vuforia;

namespace WhereAR
{

	public class WhereARCameraFocus : MonoBehaviour {

		private bool mVuforiaStarted = false;

		public static bool isSetTapFoucs = false;
		private float touchduration;
		private Touch touch;

		int focusCount = 0;

		void Start () 
		{
			VuforiaARController vuforia = VuforiaARController.Instance;

			if (vuforia != null)
				vuforia.RegisterVuforiaStartedCallback(StartAfterVuforia);
		}

		void Update()
		{
			if(isSetTapFoucs)
			{
				Vuforia.CameraDevice.Instance.SetFocusMode(Vuforia.CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);

				if (Input.touchCount > 0) {
					touchduration += Time.deltaTime;
					touch = Input.GetTouch (0);
					if (touch.phase == TouchPhase.Ended && touchduration < 0.2f) {
						StartCoroutine ("singleOrDouble");
					}
				}  else {
					touchduration = 0;     
				}
			}
		}

		IEnumerator singleOrDouble ()
		{
			yield return new WaitForSeconds (0.3f);
			if (touch.tapCount == 1)
				//Debug.Log ("Single");
				OnSingleTapped ();
			else if (touch.tapCount == 2) {
				//this coroutine has been called twice. We should stop the next one here otherwise we get two double tap
				StopCoroutine ("singleOrDouble");
				//Debug.Log ("Double");
				OnDoubleTapped();
			}
		}
		//        
		private void OnSingleTapped ()
		{
			TriggerAutoFocus ();
			//label = "Tap the Screen!";
		}

		private void OnDoubleTapped ()
		{
			//label = "Double Tap the Screen!";
		}

		public void TriggerAutoFocus ()
		{
			StartCoroutine (TriggerAutoFocusAndEnableContinuousFocusIfSet ());
		}

		/// <summary>
		/// Activating trigger autofocus mode unsets continuous focus mode (if was previously enabled from the UI Options Menu)
		/// So, we wait for a second and turn continuous focus back on (if options menu shows as enabled)
		/// </returns>
		private IEnumerator TriggerAutoFocusAndEnableContinuousFocusIfSet ()
		{
			focusCount++;
			CameraDevice.Instance.SetFocusMode (CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);
			TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
			yield return new WaitForSeconds (1.0f);
			TrackerManager.Instance.GetTracker<ObjectTracker>().Start();
			CameraDevice.Instance.SetFocusMode (CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		}

		private void StartAfterVuforia()
		{
			mVuforiaStarted = true;
			SetAutofocus();
		}

		void OnApplicationPause(bool pause)
		{
			if (!pause)
			{
				// App resumed
				if (mVuforiaStarted)
				{
					// App resumed and vuforia already started
					// but lets start it again...
					SetAutofocus(); // This is done because some android devices lose the auto focus after resume
					// this was a bug in vuforia 4 and 5. I haven't checked 6, but the code is harmless anyway
				}
			}
		}

		private void SetAutofocus()
		{
			#if UNITY_IOS
			#else

			if (CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO))
			{
			isSetTapFoucs = true;
			//Debug.Log("Autofocus set");
			}
			else
			{
			CameraDevice.Instance.SetFocusMode (CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);
			isSetTapFoucs = true;
			// never actually seen a device that doesn't support this, but just in case
			//Debug.Log("this device doesn't support auto focus");
			}
			#endif

		}

//		void OnGUI()
//		{
//			GUI.Label (new Rect(0, 0, Screen.width, Screen.height), focusCount.ToString());
//		}
	}
}
