using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WhereAR
{
	public class ARObject
	{
		public string ARID;
		public bool isActive;
		public ARObejctType ARType;
		public List<ARObjectInfo> en;
		public List<ARObjectInfo> tc;
		public List<ARObjectInfo> sc;

		public string GetFirstVideo(string lang = "")
		{
			string returnResult = "";

			List<ARObjectInfo> objs = new List<ARObjectInfo> ();
			switch(lang)
			{
			case "en":
			default:
				objs = en;
				break;
			case "tc":
				objs = tc;
				break;
			case "sc":
				objs = sc;
				break;
			}
			ARObjectInfo firstVideoItem = null;
			foreach(ARObjectInfo objInfo in objs)
			{
				if(objInfo.buttonType == ARButtonType.WHEREAR_VIDEO)
				{
					firstVideoItem = objInfo;
					break;
				}
			}
			if(firstVideoItem != null)
			{
				if(firstVideoItem.VideoUrls.Count > 0)
				{
					returnResult = firstVideoItem.VideoUrls [0];
				}
			}
			return returnResult;
		}

		public int GetFirstVideoObjIndex(string lang = "")
		{
			int returnResult = 0;

			List<ARObjectInfo> objs = new List<ARObjectInfo> ();
			switch(lang)
			{
			case "en":
			default:
				objs = en;
				break;
			case "tc":
				objs = tc;
				break;
			case "sc":
				objs = sc;
				break;
			}
			foreach(ARObjectInfo objInfo in objs)
			{
				if(objInfo.buttonType == ARButtonType.WHEREAR_VIDEO)
				{
					break;
				}
				returnResult++;
			}
			return returnResult;
		}

		public string GetFirstGreenVideo(string lang = "")
		{
			string returnResult = "";

			List<ARObjectInfo> objs = new List<ARObjectInfo> ();
			switch(lang)
			{
			case "en":
			default:
				objs = en;
				break;
			case "tc":
				objs = tc;
				break;
			case "sc":
				objs = sc;
				break;
			}
			ARObjectInfo firstVideoItem = null;
			foreach(ARObjectInfo objInfo in objs)
			{
				if(objInfo.buttonType == ARButtonType.WHEREAR_GREENMOVIE)
				{
					firstVideoItem = objInfo;
					break;
				}
			}
			if(firstVideoItem != null)
			{
				if(firstVideoItem.VideoUrls.Count > 0)
				{
					returnResult = firstVideoItem.VideoUrls [0];
				}
			}
			return returnResult;
		}

		public List<ARObjectImage> GetFirstImages(string lang = "")
		{
			List<ARObjectImage> returnResult = new List<ARObjectImage> ();
			List<ARObjectInfo> objs = new List<ARObjectInfo> ();
			switch(lang)
			{
			case "en":
			default:
				objs = en;
				break;
			case "tc":
				objs = tc;
				break;
			case "sc":
				objs = sc;
				break;
			}
			ARObjectInfo firstImageItem = null;
			foreach(ARObjectInfo objInfo in objs)
			{
				if(objInfo.buttonType == ARButtonType.WHEREAR_IMAGES)
				{
					firstImageItem = objInfo;
					break;
				}
			}

			if(firstImageItem != null)
			{
				if(firstImageItem.Images.Count > 0)
				{
					returnResult = firstImageItem.Images;
				}
			}
			return returnResult;
		}


		public List<ARObjectImage> GetFirstSelfie(string lang = "")
		{
			List<ARObjectImage> returnResult = new List<ARObjectImage> ();
			List<ARObjectInfo> objs = new List<ARObjectInfo> ();
			switch(lang)
			{
			case "en":
			default:
				objs = en;
				break;
			case "tc":
				objs = tc;
				break;
			case "sc":
				objs = sc;
				break;
			}
			ARObjectInfo firstImageItem = null;
			foreach(ARObjectInfo objInfo in objs)
			{
				if(objInfo.buttonType == ARButtonType.WHEREAR_SELFIE)
				{
					firstImageItem = objInfo;
					break;
				}
			}

			if(firstImageItem != null)
			{
				if(firstImageItem.Images.Count > 0)
				{
					returnResult = firstImageItem.Images;
				}
			}
			return returnResult;
		}

		public ARObjectInfo GetARObjectInfoByIndex(int index, string lang = "")
		{
			ARObjectInfo returnResult = new ARObjectInfo ();
			List<ARObjectInfo> objs = new List<ARObjectInfo> ();
			switch(lang)
			{
			case "en":
			default:
				objs = en;
				break;
			case "tc":
				objs = tc;
				break;
			case "sc":
				objs = sc;
				break;
			}
			returnResult = objs [index];
			return returnResult;
		}

	}

	public enum ARObejctType
	{
		WHEREAR_NONE = 0,
		WHEREAR_IMAGE_SHOW = 1,
		WHEREAR_VIDEO = 2,
		WHEREAR_GREENVIDEO = 3
	}

	public enum ARButtonType
	{
		WHEREAR_VIDEO = 0,
		WHEREAR_SELFIE = 1,
		WHEREAR_TAKEPHOTO = 2,
		WHEREAR_GREENMOVIE = 3,
		WHEREAR_FACEBOOK = 4,
		WHEREAR_IG = 5,
		WHEREAR_IMAGES = 6,
		WHEREAR_WEBLINK = 7,
		WHEREAR_EMAIL = 8,
		WHEREAR_PHONE = 9,
		WHEREAR_WHATSAPP = 10,
		WHEREAR_LOCATION = 11
	}

	public class ARObjectInfo
	{
		public ARButtonType buttonType;
		public string ButtonImageUrl;
		public string Info;
		public List<ARObjectImage> Images;
		public List<string> VideoUrls;
	}

	public class ARObjectImage
	{
		public string ImageUrl;
		public float height;
		public float width;
	}


}
