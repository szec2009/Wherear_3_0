using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using WhereAR;

public class ImageUpSideDown : MonoBehaviour {

	// Use this for initialization

	ScreenOrientation orientation;
	string msg = "";

	void Start () {
		orientation = Screen.orientation;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(orientation != Screen.orientation)
		{
			orientation = Screen.orientation;
			ImageRotate ();
		}
	}

	public void UpSideDown()
	{

		if(WhereARSwitchCamera.cameraDirection == CameraDevice.CameraDirection.CAMERA_BACK)
			{

			#if UNITY_ANDROID
			if(orientation == ScreenOrientation.Portrait)
			{
				transform.rotation = new Quaternion(0.7f, 0.0f,0.0f, -0.7f);
				//				transform.rotation = new Quaternion(0.0f, 0.7f, -0.7f, 0.0f);
			}
			if(orientation == ScreenOrientation.LandscapeLeft)
			{
				transform.rotation = new Quaternion(0.0f, 0.7f, -0.7f, 0.0f);
				//				transform.rotation = new Quaternion(0.7f, 0.0f,0.0f, -0.7f);
			}
			if(orientation == ScreenOrientation.LandscapeRight)
			{
				transform.rotation = new Quaternion(0.0f, 0.7f, -0.7f, 0.0f);
				//				transform.rotation = new Quaternion(-0.7f, 0.0f,0.0f, 0.7f);
			}
			#endif


			#if UNITY_IOS
			if(orientation == ScreenOrientation.Portrait)
			{
			transform.rotation = new Quaternion(0.0f, 0.7f, -0.7f, 0.0f);
			}
			if(orientation == ScreenOrientation.LandscapeLeft)
			{
			transform.rotation = new Quaternion(0.7f, 0.0f,0.0f, -0.7f);
			}
			if(orientation == ScreenOrientation.LandscapeRight)
			{
			transform.rotation = new Quaternion(-0.7f, 0.0f,0.0f, 0.7f);
			}
			#endif
			}
			transform.localScale = new Vector3(
				transform.localScale.x * -1,
				transform.localScale.y,
				transform.localScale.z
			);
	}

	public void ImageRotate()
	{
		if(WhereARSwitchCamera.cameraDirection == CameraDevice.CameraDirection.CAMERA_FRONT)
		{
			#if UNITY_ANDROID
			if(orientation == ScreenOrientation.Portrait)
			{
				transform.rotation = new Quaternion(0.7f, 0.0f,0.0f, -0.7f);
//				transform.rotation = new Quaternion(0.0f, 0.7f, -0.7f, 0.0f);
			}
			if(orientation == ScreenOrientation.LandscapeLeft)
			{
				transform.rotation = new Quaternion(0.0f, 0.7f, -0.7f, 0.0f);
//				transform.rotation = new Quaternion(0.7f, 0.0f,0.0f, -0.7f);
			}
			if(orientation == ScreenOrientation.LandscapeRight)
			{
				transform.rotation = new Quaternion(0.0f, 0.7f, -0.7f, 0.0f);
//				transform.rotation = new Quaternion(-0.7f, 0.0f,0.0f, 0.7f);
			}
			#endif


			#if UNITY_IOS
			if(orientation == ScreenOrientation.Portrait)
			{
			transform.rotation = new Quaternion(0.0f, 0.7f, -0.7f, 0.0f);
			}
			if(orientation == ScreenOrientation.LandscapeLeft)
			{
			transform.rotation = new Quaternion(0.7f, 0.0f,0.0f, -0.7f);
			}
			if(orientation == ScreenOrientation.LandscapeRight)
			{
			transform.rotation = new Quaternion(-0.7f, 0.0f,0.0f, 0.7f);
			}
			#endif

		}
	}

	public void Rotate(float der)
	{
		Quaternion r = Quaternion.AngleAxis(der, transform.up);
		transform.rotation = r * transform.rotation;
	}
		
//	void OnGUI()
//	{
//		GUI.Label (new Rect(0, 0, Screen.width, Screen.height), transform.rotation.ToString() + " - " + ScreenOrientation.LandscapeRight.ToString());
//	}
}
