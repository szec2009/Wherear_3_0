using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhereAR
{
	public class WhereARObject : MonoBehaviour {

		public ARObject obj;

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		public static ARObject JSONObjectToARObject(JSONObject jsonObj)
		{
			ARObject _obj = new ARObject ();

			_obj.ARID = jsonObj ["ARID"].str.Replace("\\", "");
			_obj.isActive = jsonObj ["isActive"].b;
			_obj.ARType = (ARObejctType)int.Parse(jsonObj ["ARType"].ToString());

			JSONObject enobj = jsonObj["en"];
			JSONObject tcobj = jsonObj["tc"];
			JSONObject scobj = jsonObj["sc"];
			List<ARObjectInfo> en = new List<ARObjectInfo> ();
			List<ARObjectInfo> tc = new List<ARObjectInfo> ();
			List<ARObjectInfo> sc = new List<ARObjectInfo> ();

			//EN Info
			foreach(JSONObject item in enobj.list)
			{
				ARObjectInfo _objInfo = new ARObjectInfo ();
				_objInfo.buttonType = (ARButtonType)int.Parse (item ["ARType"].ToString ());
				_objInfo.Info = item ["Info"].str.Replace("\\", "");
				_objInfo.ButtonImageUrl = item ["ButtonImage"].str.Replace("\\", "");
				JSONObject video = item ["Video"];
				List<string> _video = new List<string> ();
				foreach(JSONObject videoItem in video.list)
				{
					_video.Add (videoItem.str.Replace("\\", ""));
				}
				_objInfo.VideoUrls = _video;
				JSONObject images = item["Images"];
				List<ARObjectImage> _images = new List<ARObjectImage> ();
				foreach(JSONObject imageItem in images.list)
				{
					ARObjectImage _imageItem = new ARObjectImage ();
					_imageItem.ImageUrl = imageItem ["imageurl"].str.Replace("\\", "");
					_imageItem.width = imageItem ["width"].f;
					_imageItem.height = imageItem ["height"].f;
					_images.Add (_imageItem);
				}
				_objInfo.Images = _images;
				en.Add (_objInfo);
			}
			_obj.en = en;

			//TC Info
			foreach(JSONObject item in tcobj.list)
			{
				ARObjectInfo _objInfo = new ARObjectInfo ();
				_objInfo.buttonType = (ARButtonType)int.Parse (item ["ARType"].ToString ());
				_objInfo.Info = item ["Info"].str.Replace("\\", "");
				JSONObject video = item ["Video"];
				List<string> _video = new List<string> ();
				foreach(JSONObject videoItem in video.list)
				{
					_video.Add (videoItem.str.Replace("\\", ""));
				}
				_objInfo.VideoUrls = _video;
				JSONObject images = item["Images"];
				List<ARObjectImage> _images = new List<ARObjectImage> ();
				foreach(JSONObject imageItem in images.list)
				{
					ARObjectImage _imageItem = new ARObjectImage ();
					_imageItem.ImageUrl = imageItem ["imageurl"].str.Replace("\\", "");
					_imageItem.width = imageItem ["width"].f;
					_imageItem.height = imageItem ["height"].f;
					_images.Add (_imageItem);
				}
				_objInfo.Images = _images;
				tc.Add (_objInfo);
			}
			_obj.tc = tc;

			//SC Info
			foreach(JSONObject item in scobj.list)
			{
				ARObjectInfo _objInfo = new ARObjectInfo ();
				_objInfo.buttonType = (ARButtonType)int.Parse (item ["ARType"].ToString ());
				_objInfo.Info = item ["Info"].str.Replace("\\", "");
				JSONObject video = item ["Video"];
				List<string> _video = new List<string> ();
				foreach(JSONObject videoItem in video.list)
				{
					_video.Add (videoItem.str.Replace("\\", ""));
				}
				_objInfo.VideoUrls = _video;
				JSONObject images = item["Images"];
				List<ARObjectImage> _images = new List<ARObjectImage> ();
				foreach(JSONObject imageItem in images.list)
				{
					ARObjectImage _imageItem = new ARObjectImage ();
					_imageItem.ImageUrl = imageItem ["imageurl"].str.Replace("\\", "");
					_imageItem.width = imageItem ["width"].f;
					_imageItem.height = imageItem ["height"].f;
					_images.Add (_imageItem);
				}
				_objInfo.Images = _images;
				sc.Add (_objInfo);
			}
			_obj.sc = sc;

			return _obj;
		}

	}



}
