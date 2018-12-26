using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WhereAR
{
	public class WhereARFullScreen : MonoBehaviour {

		public RectTransform rectTransform;
		public bool isFullScreen;
		// Use this for initialization
		void Start () {
		}

		// Update is called once per frame
		void Update () {
			if(isFullScreen)
			{
				if(rectTransform.sizeDelta.x != Screen.width)
				{
					rectTransform.sizeDelta = new Vector2 (Screen.width, Screen.height);
				}
			}
		}
	}
}

