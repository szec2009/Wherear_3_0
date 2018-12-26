using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhereAR
{
	public class WhereARImageButton : MonoBehaviour {

		public ButtonType buttonType;
		public WhereARImageListHandler imageListHandler;

		public enum ButtonType
		{
			NEXT,
			PREV
		}

		void OnMouseDown()
		{
			switch(buttonType)
			{
			case ButtonType.NEXT:
				imageListHandler.NextImage ();
				break;
			case ButtonType.PREV:
				imageListHandler.PrevImage ();
				break;
			}
		}
	}
}
