using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

namespace WhereAR
{
	public class WhereAR2ARMS : MonoBehaviour {

		public string ARMSUrl = "http://devwherear.com/arms/v2/ARMS/libs/api/artarget/get/";

		public IEnumerator MakeRecoRequest(
			string ARItemID,
			string VuforiaID,
			string Action,
			System.Action<ARObject> CallBack
		)
		{
			WWWForm formData = new WWWForm ();
			formData.AddField ("id", ARItemID);
			formData.AddField ("vu", VuforiaID);

			string deviceID = "";

			#if UNITY_IOS
			deviceID = UnityEngine.iOS.Device.vendorIdentifier;
			#else
			deviceID = SystemInfo.deviceUniqueIdentifier;
			#endif

			if(!string.IsNullOrEmpty(deviceID))
			{
				formData.AddField ("d", deviceID);
			}
			formData.AddField("a", Action);
			formData.AddField("pl", Application.platform.ToString());
			formData.AddField("os", SystemInfo.operatingSystem);
			formData.AddField("l", "");
			formData.AddField("ct", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

			UnityWebRequest www = UnityWebRequest.Post (ARMSUrl + "", formData);
			www.chunkedTransfer = false;
			yield return www.SendWebRequest ();

			if (www.isNetworkError || www.isHttpError) {
				yield return www.error;
			} else {
				string returnResult = www.downloadHandler.text;

				JSONObject jsonObj = new JSONObject (returnResult);

				ARObject obj = WhereARObject.JSONObjectToARObject (jsonObj);

				CallBack (obj);

				yield return obj;
			}

		}

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}
	}

}
