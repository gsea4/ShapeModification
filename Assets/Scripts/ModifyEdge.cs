using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyEdge : MonoBehaviour {
	private Vector3 screenPoint;
	private Vector3 offset;
	Vector3 temp1;
	Vector3 temp2;
	public List<GameObject> listOfVerticesObjects = new List<GameObject>();
	// Use this for initialization
	void Start () {
		listOfVerticesObjects = GameObject.Find ("EdgeManager").GetComponent<EdgeController> ().Edge;
		temp1 = listOfVerticesObjects [0].transform.position;
		temp2 = listOfVerticesObjects [1].transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Yolo");
		if (temp1.x - temp2.x != 0) {
			foreach (GameObject g in listOfVerticesObjects) {
				Vector3 temp = g.transform.position;
				temp.z = this.transform.position.z;
				temp.y = this.transform.position.y;
				g.transform.position = temp;
			}
		}

		if (temp1.y - temp2.y != 0) {
			foreach (GameObject g in listOfVerticesObjects) {
				Vector3 temp = g.transform.position;
				temp.x = this.transform.position.x;
				temp.z = this.transform.position.z;
				g.transform.position = temp;
			}
		}

		if (temp1.z - temp2.z != 0) {
			foreach (GameObject g in listOfVerticesObjects) {
				Vector3 temp = g.transform.position;
				temp.x = this.transform.position.x;
				temp.y = this.transform.position.y;
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

		if (temp1.x - temp2.x != 0) {
			Vector3 tempPos = transform.position;
			tempPos.z = cursorPosition.z;
			tempPos.y = cursorPosition.y;
			transform.position = tempPos;
		}

		if (temp1.y - temp2.y != 0) {
			Vector3 tempPos = transform.position;
			tempPos.z = cursorPosition.z;
			tempPos.x = cursorPosition.x;
			transform.position = tempPos;
		}

		if (temp1.z - temp2.z != 0) {
			Vector3 tempPos = transform.position;
			tempPos.x = cursorPosition.x;
			tempPos.y = cursorPosition.y;
			transform.position = tempPos;
		}
	}
}
