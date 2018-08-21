using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceModeManager : MonoBehaviour {
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
			Debug.Log ("Activating Face Mode for " + g.name);
			if(g.GetComponent<Modify>())
				g.GetComponent<Modify> ().FaceMode ();
			else if(g.GetComponent<Modify_2>())
				g.GetComponent<Modify_2> ().FaceMode ();
		}
	}

	public void TestActivate(){
		ObjectList = GameObject.FindGameObjectsWithTag ("MO");
		foreach (GameObject g in ObjectList) {
			if(g.GetComponent<Modify>())
				g.GetComponent<Modify> ().CreateFace ();
			else if(g.GetComponent<Modify_2>())
				g.GetComponent<Modify_2> ().CreateFace ();
		}
	}
}
