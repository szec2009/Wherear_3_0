using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Vuforia;

namespace WhereAR
{
	public class WhereARUIPosition : MonoBehaviour {

		public RectTransform mainCanvas;
		public RectTransform rect;
		public ItemPosition itemPosition;

		public enum ItemPosition{
			NONE,
			TOP_RIGHT,
			TOP_MIDDLE,
			TOP_LEFT,
			MIDDLE_RIGHT,
			MIDDLE,
			MIDDLE_LEFT,
			BOTTOM_RIGHT,
			BOTTOM_MIDDLE,
			BOTTOM_LEFT
		}

		public float horizontalPadding;
		public float verticalPadding;
		public bool runOnUpdate = true;

		public ItemPosition VerticalPosition;
		public float horizontalPaddingWhenVertical;
		public float verticalPaddingWhenVertial;

		void Start () {
			if (VerticalPosition != ItemPosition.NONE) {
				if (Screen.height > Screen.width) {
					SetPosition (VerticalPosition, horizontalPaddingWhenVertical, verticalPaddingWhenVertial);
				} else {
					SetPosition (itemPosition, horizontalPadding, verticalPadding);				}
			} else {
				SetPosition (itemPosition, horizontalPadding, verticalPadding);
			}

		}

		// Update is called once per frame
		void Update () {
			if(runOnUpdate)
			{
				if (VerticalPosition != ItemPosition.NONE) {
					if (Screen.height > Screen.width) {
						SetPosition (VerticalPosition, horizontalPaddingWhenVertical, verticalPaddingWhenVertial);
					} else {
						SetPosition (itemPosition, horizontalPadding, verticalPadding);
					}
				} else {
					SetPosition (itemPosition, horizontalPadding, verticalPadding);
				}
			}
		}

		void SetPosition(ItemPosition itemPosition, float _hPadding, float _vPadding)
		{
			switch(itemPosition)
			{
			case ItemPosition.TOP_RIGHT:
				{
					rect.anchoredPosition = new Vector2 (
						mainCanvas.sizeDelta.x / 2 - rect.sizeDelta.x / 2 - _hPadding, 
						mainCanvas.sizeDelta.y / 2 - rect.sizeDelta.y / 2 - _vPadding
					);
				}
				break;
			case ItemPosition.TOP_MIDDLE:
				{
					rect.anchoredPosition = new Vector2 (
						0, 
						mainCanvas.sizeDelta.y / 2 - rect.sizeDelta.y / 2 - _vPadding
					);
				}
				break;
			case ItemPosition.TOP_LEFT:
				{
					rect.anchoredPosition = new Vector2 (
						0 - mainCanvas.sizeDelta.x / 2 + rect.sizeDelta.x / 2 + _hPadding, 
						mainCanvas.sizeDelta.y / 2 - rect.sizeDelta.y / 2 - _vPadding
					);
				}
				break;
			case ItemPosition.MIDDLE_RIGHT:
				{
					rect.anchoredPosition = new Vector2 (
						mainCanvas.sizeDelta.x / 2 - rect.sizeDelta.x / 2 - _hPadding, 
						0
					);
				}
				break;
			case ItemPosition.MIDDLE:
				{
					rect.anchoredPosition = new Vector2 (0, 0);
				}
				break;
			case ItemPosition.MIDDLE_LEFT:
				{
					rect.anchoredPosition = new Vector2 (
						0 - mainCanvas.sizeDelta.x / 2 + rect.sizeDelta.x / 2 + _hPadding, 
						0
					);
				}
				break;
			case ItemPosition.BOTTOM_RIGHT:
				{
					rect.anchoredPosition = new Vector2 (
						mainCanvas.sizeDelta.x / 2 - rect.sizeDelta.x / 2 - _hPadding, 
						0 - mainCanvas.sizeDelta.y / 2 + rect.sizeDelta.y / 2 + _vPadding
					);
				}
				break;
			case ItemPosition.BOTTOM_MIDDLE:
				{
					rect.anchoredPosition = new Vector2 (
						0, 
						0 - mainCanvas.sizeDelta.y / 2 + rect.sizeDelta.y / 2 + _vPadding
					);
				}
				break;
			case ItemPosition.BOTTOM_LEFT:
				{
					rect.anchoredPosition = new Vector2 (
						0 - mainCanvas.sizeDelta.x / 2 + rect.sizeDelta.x / 2 + _hPadding, 
						0 - mainCanvas.sizeDelta.y / 2 + rect.sizeDelta.y / 2 + _vPadding
					);
				}
				break;

			}

		}
	}
}
