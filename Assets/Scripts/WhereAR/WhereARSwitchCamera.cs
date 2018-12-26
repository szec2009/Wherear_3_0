using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

namespace WhereAR
{
	public class WhereARSwitchCamera : MonoBehaviour {

		public static CameraDevice.CameraDirection cameraDirection;
		public List<GameObject> selfieImages = new List<GameObject>();
		public GameObject rescanBtn;
		public GameObject holdAndReleaseObj;
		int holdAndReleaseObjCount;
		string msg = "";

		// Use this for initialization
		void Start () {
			cameraDirection = CameraDevice.Instance.GetCameraDirection ().ToString () == "CAMERA_DEFAULT" ? CameraDevice.CameraDirection.CAMERA_BACK : CameraDevice.Instance.GetCameraDirection ();
		}

		// Update is called once per frame
		void Update () {

		}

		public void SwitchCamera()
		{
			holdAndReleaseObjCount = holdAndReleaseObj.transform.childCount;
			if (cameraDirection == CameraDevice.CameraDirection.CAMERA_BACK) {

				StartCoroutine ("SwitchToFrontCamera");

			}else if(cameraDirection == CameraDevice.CameraDirection.CAMERA_FRONT){
				StartCoroutine ("SwitchToBackCamera");
			}
		}

		private IEnumerator SwitchToFrontCamera()
		{
			foreach(GameObject obj in selfieImages)
			{
				obj.GetComponent<ImageUpSideDown> ().UpSideDown ();
			}
			if(!rescanBtn.active && holdAndReleaseObjCount == 0)
			{
				ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
				ChangeImageBehaviourStatus (false);
				tracker.Stop ();
			}
				CameraDevice.Instance.Deinit ();
				CameraDevice.Instance.Init (CameraDevice.CameraDirection.CAMERA_FRONT);
				CameraDevice.Instance.Start ();
				cameraDirection = CameraDevice.CameraDirection.CAMERA_FRONT;

			if(!rescanBtn.active && holdAndReleaseObjCount == 0)
			{
				ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
				yield return new WaitForSeconds(0.5f);
				tracker.Start ();
				ChangeImageBehaviourStatus (true);
			}
		}

		private IEnumerator SwitchToBackCamera()
		{
			foreach(GameObject obj in selfieImages)
			{
				obj.GetComponent<ImageUpSideDown> ().UpSideDown ();
			}
			if (!rescanBtn.active && holdAndReleaseObjCount == 0) {
				ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
				ChangeImageBehaviourStatus (false);
				tracker.Stop ();
			}
				CameraDevice.Instance.Stop ();
				CameraDevice.Instance.Deinit ();
				CameraDevice.Instance.Init(CameraDevice.CameraDirection.CAMERA_BACK); 
				CameraDevice.Instance.Start ();
				cameraDirection = CameraDevice.CameraDirection.CAMERA_BACK;

			if (!rescanBtn.active && holdAndReleaseObjCount == 0) {
				ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
				yield return new WaitForSeconds(0.5f);
				tracker.Start ();
				ChangeImageBehaviourStatus (true);
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
//			GUI.Label (new Rect(0, 0, Screen.width, Screen.height), "Test: " + holdAndReleaseObjCount.ToString());
		}
	}
}
