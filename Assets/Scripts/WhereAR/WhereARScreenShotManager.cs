#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.

using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
using WhereAR;

public class WhereARScreenshotManager : MonoBehaviour {

	public static event Action ScreenshotFinishedSaving;
	public static event Action ImageFinishedSaving;
	public static event Action ImageNotSave;

	public static string errormess="";


	#if UNITY_IPHONE

	[DllImport("__Internal")]
	private static extern bool saveToGallery( string path );

	#endif

	public static void reset ()
	{   
		WhereARScreenshotManager.ScreenshotFinishedSaving =null;             
		WhereARScreenshotManager.ImageFinishedSaving =null;
		WhereARScreenshotManager.ImageNotSave = null;
	}

	public static IEnumerator Save(System.Action<string> _imagePath, string fileName, string albumName = "MyScreenshots", bool callback = false)
	{
		WhereARTakePhoto.msg += "\nStart Save";
		string returnPath = "";
			bool photoSaved = false;

			string date = System.DateTime.Now.ToString("dd-MM-yy");

			WhereARScreenshotManager.ScreenShotNumber++;

			string screenshotFilename = fileName+".png";// + "_" + WhereARScreenshotManager.ScreenShotNumber + "_" + date + ".png";

			WhereARTakePhoto.msg += "\n" + screenshotFilename;

			//Debug.Log("Save screenshot " + screenshotFilename); 

			#if UNITY_IPHONE

			if(Application.platform == RuntimePlatform.IPhonePlayer) 
			{
			Debug.Log("iOS platform detected");

			string iosPath = Application.persistentDataPath + "/" + screenshotFilename;

			ScreenCapture.CaptureScreenshot(screenshotFilename);

			//yield return new WaitForSeconds(2f);

			while(!photoSaved) 
			{
			photoSaved = saveToGallery( iosPath );

			yield return new WaitForSeconds(.5f);
			_imagePath(iosPath);
			yield return iosPath;
			}

			UnityEngine.iOS.Device.SetNoBackupFlag( iosPath );

			} else {

			ScreenCapture.CaptureScreenshot(screenshotFilename);

			}

			#elif UNITY_ANDROID	

			if(Application.platform == RuntimePlatform.Android) 
			{
				//Debug.Log("Android platform detected");
				//string androidPath = "/../../../../DCIM/" + albumName + "/" + screenshotFilename;
				string androidPath = "/../../../../DCIM/" + albumName + "/" + screenshotFilename;
				string path = Application.persistentDataPath + androidPath;
				WhereARTakePhoto.msg += "\nPath: " + path;
				string pathonly = Path.GetDirectoryName(path);
				Directory.CreateDirectory(pathonly);
				WhereARTakePhoto.msg += "\npathonly: " + pathonly ;
				ScreenCapture.CaptureScreenshot(androidPath);
//
				AndroidJavaClass obj = new AndroidJavaClass("com.ryanwebb.androidscreenshot.MainActivity");

				while(!photoSaved) 
				{
					photoSaved = obj.CallStatic<bool>("scanMedia", path);

					//yield return new WaitForSeconds(.5f);
					_imagePath(path);
				returnPath = path;
				yield return returnPath;
				}

			} else {

				ScreenCapture.CaptureScreenshot(screenshotFilename);

			}
			#else

			while(!photoSaved) 
			{
			yield return new WaitForSeconds(.5f);

			Debug.Log("Screenshots only available in iOS/Android mode!");

			photoSaved = true;
			}

			#endif

			if (callback) {
				ScreenshotFinishedSaving ();
			} else {
				ImageNotSave ();
			}

		WhereARTakePhoto.msg += "\n" + returnPath;

		WhereARTakePhoto.msg += "\nEnd Save";
	}


	public static IEnumerator SaveExisting(string filePath, bool callback = false)
	{
		bool photoSaved = false;

		Debug.Log("Save existing file to gallery " + filePath);

		#if UNITY_IPHONE

		if(Application.platform == RuntimePlatform.IPhonePlayer) 
		{
		Debug.Log("iOS platform detected");

		while(!photoSaved) 
		{
		photoSaved = saveToGallery( filePath );

		yield return new WaitForSeconds(.5f);
		}

		UnityEngine.iOS.Device.SetNoBackupFlag( filePath );
		}

		#elif UNITY_ANDROID	

		if(Application.platform == RuntimePlatform.Android) 
		{
			Debug.Log("Android platform detected");

			AndroidJavaClass obj = new AndroidJavaClass("com.ryanwebb.androidscreenshot.MainActivity");

			while(!photoSaved) 
			{
				photoSaved = obj.CallStatic<bool>("scanMedia", filePath);

				yield return new WaitForSeconds(.5f);
			}

		}

		#else

		while(!photoSaved) 
		{
		yield return new WaitForSeconds(.5f);

		Debug.Log("Save existing file only available in iOS/Android mode!");

		photoSaved = true;
		}

		#endif

		if (callback)
			ImageFinishedSaving ();
		else
			ImageNotSave ();
	}


	public static int ScreenShotNumber 
	{
		set { PlayerPrefs.SetInt("screenShotNumber", value); }

		get { return PlayerPrefs.GetInt("screenShotNumber"); }
	}

}
