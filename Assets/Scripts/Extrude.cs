using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extrude : MonoBehaviour {
	public List<GameObject> listOfVerticesObjects = new List<GameObject>();
	public char shareAxis;
	private Vector3 screenPoint;
	private Vector3 offset;
	public GameObject parent;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (shareAxis == 'Z') {
			foreach (GameObject g in listOfVerticesObjects) {
				Vector3 temp = g.transform.position;
				temp.z = this.transform.position.z;
				g.transform.position = temp;
			}
		}

		if (shareAxis == 'Y') {
			foreach (GameObject g in listOfVerticesObjects) {
				Vector3 temp = g.transform.position;
				temp.y = this.transform.position.y;
				g.transform.position = temp;
			}
		}

		if (shareAxis == 'X') {
			foreach (GameObject g in listOfVerticesObjects) {
				Vector3 temp = g.transform.position;
				temp.x = this.transform.position.x;
				g.transform.position = temp;
			}
		}

	}


	void OnMouseDown(){
		screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	void OnMouseDrag(){
		Vector3 cursorPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint (cursorPoint) + offset;
		if (shareAxis == 'Z') {
			Vector3 tempPos = transform.position;
			tempPos.z = cursorPosition.z;
			transform.position = tempPos;
		}
		if (shareAxis == 'Y') {
			Vector3 tempPos = transform.position;
			tempPos.y = cursorPosition.y;
			transform.position = tempPos;
		}
		if (shareAxis == 'X') {
			Vector3 tempPos = transform.position;
			tempPos.x = cursorPosition.x;
			transform.position = tempPos;
		}
	}

	void OnMouseUp(){
		Debug.Log ("Hello There: " + parent.gameObject.name);
		if(parent.GetComponent<Modify> ())
			parent.GetComponentInChildren<Modify> ().reCalcMidPoint ();
		else if(parent.GetComponent<Modify_2> ())
			parent.GetComponentInChildren<Modify_2> ().reCalcMidPoint ();
	}
}
