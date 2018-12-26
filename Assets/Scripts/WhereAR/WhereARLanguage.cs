using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhereARLanguage : MonoBehaviour {

	public static string lang = "en";

	public static string GetLang()
	{
		return lang;
	}

	public void SetLang(string _lang)
	{
		lang = _lang;
	}
}