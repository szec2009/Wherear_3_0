using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhereARDoNotDestroyOnLoad : MonoBehaviour {

	public GameObject obj;
	// Use this for initialization
	void Start () {
		Object.DontDestroyOnLoad (obj);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
