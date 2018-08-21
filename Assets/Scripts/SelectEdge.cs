using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectEdge : MonoBehaviour {
	public GameObject EdgeManager;
	// Use this for initialization
	void Awake () {
		EdgeManager = GameObject.Find("EdgeManager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseUp(){
		if (EdgeManager.GetComponent<EdgeController> ().EdgeMode) {
			EdgeManager.GetComponent<EdgeController> ().Edge.Add (gameObject);
			gameObject.GetComponent<Renderer> ().material.color = Color.blue;
		}
	}
}
