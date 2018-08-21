using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexModeManager : MonoBehaviour {

	public GameObject[] ObjectList;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Activate(){
		ObjectList = GameObject.FindGameObjectsWithTag ("MO");
		foreach (GameObject g in ObjectList) {
			if(g.GetComponent<Modify>())
				g.GetComponent<Modify> ().VertexMode ();
			if(g.GetComponent<Modify_2>())
				g.GetComponent<Modify_2> ().VertexMode ();
		}
	}
}
