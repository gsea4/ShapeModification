using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFace : MonoBehaviour {
	bool isSelected = false;
	GameObject temp;
	// Use this for initialization
	void Start () {
		temp = GameObject.Find ("FaceCreationController") ;
	}
	
	// Update is called once per frame
	void Update () {

		if (!temp.GetComponent<FaceController> ().isProcessing) {
			gameObject.GetComponent<Renderer> ().material.color = Color.red;
			isSelected = false;
			if(!gameObject.GetComponent<mouseDrag>())
				gameObject.AddComponent<mouseDrag> ();
		} else {
			Destroy (gameObject.GetComponent<mouseDrag> ());
		}
	}

	void OnMouseUp(){
		//GameObject temp = GameObject.Find ("NewFaceController") ;
		if (temp.GetComponent<FaceController> ().isProcessing) {
			isSelected = !isSelected;
			if (isSelected) {
				gameObject.GetComponent<Renderer> ().material.color = Color.blue;
				temp.GetComponent<FaceController> ().vList.Add (this.gameObject);				
			} else {
				gameObject.GetComponent<Renderer> ().material.color = Color.red;
			}

		} else {
			isSelected = false;
			gameObject.GetComponent<Renderer> ().material.color = Color.red;
		}
	}
}
