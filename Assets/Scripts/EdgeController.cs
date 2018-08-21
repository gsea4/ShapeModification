using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeController : MonoBehaviour {

	public List<GameObject> Edge = new List<GameObject>();
	public GameObject prefab;
	public bool EdgeMode = false;
	public bool EdgeCreated = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!EdgeCreated && Edge.Count == 2) {
			Vector3 temp1 = Edge [0].transform.position;
			Vector3 temp2 = Edge [1].transform.position;
			if (temp1.x - temp2.x != 0) {
				float avgX = Mathf.Min (temp1.x, temp2.x) + Mathf.Abs ((temp1.x - temp2.x) / 2);
				GameObject tempEdge = Instantiate (prefab, new Vector3 (avgX, temp1.y, temp1.z), Quaternion.identity);
				tempEdge.AddComponent<ModifyEdge> ();
				EdgeCreated = true;
			}

			if (temp1.y - temp2.y != 0) {
				float avgY = Mathf.Min (temp1.y, temp2.y) + Mathf.Abs ((temp1.y - temp2.y) / 2);
				GameObject tempEdge = Instantiate (prefab, new Vector3 (temp1.x, avgY, temp1.z), Quaternion.identity);
				tempEdge.AddComponent<ModifyEdge> ();
				EdgeCreated = true;
			}

			if (temp1.z - temp2.z != 0) {
				float avgZ = Mathf.Min (temp1.z, temp2.z) + Mathf.Abs ((temp1.z - temp2.z) / 2);
				GameObject tempEdge = Instantiate (prefab, new Vector3 (temp1.x, temp1.y, avgZ), Quaternion.identity);
				tempEdge.AddComponent<ModifyEdge> ();
				EdgeCreated = true;
			}
		}
	}

	public void Activate(){
		EdgeMode = !EdgeMode;

		if (!EdgeMode) {
			Debug.Log ("Hello");
			Edge.Clear ();
			GameObject[] ObjectList = GameObject.FindGameObjectsWithTag ("EdgeHandler");
			foreach (GameObject o in ObjectList) {
				Destroy (o);
			}
			EdgeCreated = false;
		}
	}
}
