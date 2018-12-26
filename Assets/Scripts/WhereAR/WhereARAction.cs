using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WhereAR
{
	public class WhereARAction : MonoBehaviour {

		public void OnEmailClick(string email)
		{			
			Application.OpenURL ("mailto:" + email + "?subject=WhereAR");
		}

		public void OnPhoneClick(string phone)
		{			
			Application.OpenURL ("tel://" + phone);
		}

		public void OnWhatsappClick(string whatsapp)
		{
			LoadWebUrl ("https://api.whatsapp.com/send?phone=" + whatsapp);
		}

		public void OnVideoClick()
		{
		}

		public void OnGreenVideoClick()
		{
		}

		public void OnSelfieClick()
		{
		}

		public void OnImageClick()
		{
		}

		public void OnFacebookClick(string facebook)
		{
			LoadWebUrl (facebook);
		}

		public void OnWebUrlClick(string weburl)
		{
			LoadWebUrl (weburl);
		}

		public void OnLocationClick()
		{
		}

		public void OnInfoClick()
		{
		}
			
		public void OnFormClick()
		{
		}

		public void LoadWebUrl(string url)
		{
			Application.OpenURL (url);
		}

	}
}
