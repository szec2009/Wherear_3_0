using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class WhereARDisableWhenHorV : MonoBehaviour {

	public ScreenOrientation orientation;
	ScreenOrientation currentOri;

	// Update is called once per frame
	void Update () {
		currentOri = Screen.orientation;
		if(currentOri == ScreenOrientation.LandscapeLeft || 
			currentOri == ScreenOrientation.LandscapeRight ||
			currentOri == ScreenOrientation.Landscape)
		{
			currentOri = ScreenOrientation.Landscape;
		}
		if(currentOri == ScreenOrientation.Portrait || currentOri == ScreenOrientation.PortraitUpsideDown)
		{
			currentOri = ScreenOrientation.Portrait;
		}

		if (orientation == currentOri) {
			gameObject.SetActive (true);	
		} else {
			gameObject.SetActive (false);
		}
		
	}
}
