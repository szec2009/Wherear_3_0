using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

	private UniWebView webView;

	// The Next Scene
	public string SceneName;

	// Use this for initialization
	void Start () {

		// Creating WebView
		var webObject = new GameObject("UniWebView");
		webView = webObject.AddComponent<UniWebView>();

		// Start WebView
		webView.Frame = new Rect(0, 0, Screen.width, Screen.height);
		var url = UniWebViewHelper.StreamingAssetURLForPath("www/index.html");
		webView.Load(url);
		webView.Show();

		// The Interaction between WebView & Unity
		webView.OnMessageReceived += (view, message) => {
			if (message.Path.Equals("navigate")) {
				SceneManager.LoadScene(0);
			}
		};
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
