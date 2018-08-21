using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Face
{
	public string shareAxis;
	public List<GameObject> vList;

	public Face (string x)
	{
		shareAxis = x;
		vList = new List<GameObject> ();
	}
}

public class Modify : MonoBehaviour
{
	public GameObject vertexHandler;
	public GameObject faceHandler;
	public bool vMode = true;
	public bool fMode = false;
	public bool cfMode = false;
	List<GameObject> listOfV;
	List<Face> listOfFaces;
	public Vector3[] verticies;
	int[] myTriangles;
	Mesh mesh;


	bool objectInstantiated = false;
	float shareValueZ;
	float shareValueX;
	float shareValueY;
	bool isSameFace = false;

	// Use this for initialization
	void Start (){
		vertexHandler = Resources.Load ("VertexHandler") as GameObject;
		faceHandler = Resources.Load ("FaceHandler") as GameObject;
		mesh = GetComponent<MeshFilter> ().mesh;
		verticies = mesh.vertices;
		listOfV = new List<GameObject> ();
		listOfFaces = new List<Face> ();
		myTriangles = mesh.triangles;
        Debug.Log(myTriangles[0]);
		detectVertices ();
	}
	
	// Update is called once per frame
	void Update (){
		for (int i = 0; i < verticies.Length; i++) {
			verticies [i] = listOfV [i].transform.position;
		}
		mesh.vertices = verticies;
		mesh.RecalculateBounds ();
	}

	void detectVertices(){
		for (int i = 0; i < verticies.Length; i++) {
			objectInstantiated = false;
			GameObject[] tempL = GameObject.FindGameObjectsWithTag ("V");
			foreach (GameObject go in tempL) {
				if (go.name == (gameObject.name + "V")) {
					if (go.transform.position == verticies [i]) {
						listOfV.Add (Instantiate (vertexHandler, verticies [i], Quaternion.identity));
						listOfV [i].transform.parent = go.transform;
						listOfV [i].name = gameObject.name + "V";
						listOfV [i].SetActive (false);
						objectInstantiated = true;
						break;
					}
				}
			}

			if (!objectInstantiated) {
				listOfV.Add (Instantiate (vertexHandler, verticies [i], Quaternion.identity));
				listOfV [i].name = gameObject.name + "V";
				listOfV [i].AddComponent<mouseDrag> ();
			}
		}
	}

	void detectFaces (){
		//XY Plane (Z is the same)
		shareValueZ = verticies [0].z;
		for (int i = 0; i < myTriangles.Length; i += 3) {
			Debug.Log ("INSIDE DETECT FACES");
			if (verticies [myTriangles [i]].z == verticies [myTriangles [i + 1]].z
			    && verticies [myTriangles [i]].z == verticies [myTriangles [i + 2]].z
			    && verticies [myTriangles [i]].z == shareValueZ) {

				if (!isSameFace) {
					//Create a new Face here and set isSameFace to true
					listOfFaces.Add (new Face ("Share Z Axis"));
					listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i]]);
					listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i + 1]]);
					listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i + 2]]);
					isSameFace = true;
				} else {
					Debug.Log ("COUNT:" + listOfFaces.Count);
					listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i]]);
					listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i + 1]]);
					listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i + 2]]);
				}

			}else if(verticies [myTriangles [i]].z == verticies [myTriangles [i + 1]].z
				&& verticies [myTriangles [i]].z == verticies [myTriangles [i + 2]].z){
				listOfFaces.Add (new Face ("Share Z Axis"));
				listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i]]);
				listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i + 1]]);
				listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i + 2]]);
				shareValueY = verticies [myTriangles [i]].z;
				isSameFace = true;
			} else {
				isSameFace = false;
				if ((i + 3) > myTriangles.Length-1) {
					break;
				}
				shareValueZ = verticies [myTriangles [i + 3]].z;
			}
		}

		//XZ Plane (Y is the same)
		shareValueY = verticies [0].y;
		isSameFace = false;
		for (int i = 0; i < myTriangles.Length; i += 3) {
			if (verticies [myTriangles [i]].y == verticies [myTriangles [i + 1]].y
			    && verticies [myTriangles [i]].y == verticies [myTriangles [i + 2]].y
				&& verticies [myTriangles [i]].y == shareValueY) {
				if (!isSameFace) {
					//Create a new Face here and set isSameFace to true
					listOfFaces.Add (new Face ("Share Y Axis"));
					listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i]]);
					listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i + 1]]);
					listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i + 2]]);
					isSameFace = true;
				} else {
					listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i]]);
					listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i + 1]]);
					listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i + 2]]);
				}

			}else if(verticies [myTriangles [i]].y == verticies [myTriangles [i + 1]].y
				&& verticies [myTriangles [i]].y == verticies [myTriangles [i + 2]].y){
				listOfFaces.Add (new Face ("Share Y Axis"));
				listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i]]);
				listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i + 1]]);
				listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i + 2]]);
				shareValueY = verticies [myTriangles [i]].y;
				isSameFace = true;
			} else {
				isSameFace = false;
				if ((i + 3) > myTriangles.Length-1) {
					break;
				}
				shareValueY = verticies [myTriangles [i+3]].y;
			}
		}

		//YZ Plane (X is the same)
		shareValueX = verticies [0].x;
		isSameFace = false;
		for (int i = 0; i < myTriangles.Length; i += 3) {
			
			if (verticies [myTriangles [i]].x == verticies [myTriangles [i + 1]].x
			   && verticies [myTriangles [i]].x == verticies [myTriangles [i + 2]].x
			   && verticies [myTriangles [i]].x == shareValueX) {
				if (!isSameFace) {
					//Create a new Face here and set isSameFace to true
					listOfFaces.Add (new Face ("Share X Axis"));
					listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i]]);
					listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i + 1]]);
					listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i + 2]]);
					isSameFace = true;
				} else {
					listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i]]);
					listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i + 1]]);
					listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i + 2]]);
				}

			}else if(verticies [myTriangles [i]].x == verticies [myTriangles [i + 1]].x
				&& verticies [myTriangles [i]].x == verticies [myTriangles [i + 2]].x){
				listOfFaces.Add (new Face ("Share X Axis"));
				listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i]]);
				listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i + 1]]);
				listOfFaces [listOfFaces.Count - 1].vList.Add (listOfV [myTriangles [i + 2]]);
				shareValueX = verticies [myTriangles [i]].x;
				isSameFace = true;
			} else {
				isSameFace = false;
				if ((i + 3) > myTriangles.Length-1) {
					break;
				}
				shareValueX = verticies [myTriangles [i + 3]].x;
			}
		}

		foreach (var i in listOfFaces) {
			Debug.Log ("Face : " + i);
		}
	}

	void calcMidPoint(){
		float avgX, avgY, avgZ;
		int i = 0;
		foreach (Face f in listOfFaces) {
			avgX = avgY = avgZ = 0f;
			Vector3 temp1;
			Vector3 temp2;

			switch (f.shareAxis) {
			case "Share X Axis":
				temp1 = f.vList [0].transform.position;
				temp2 = f.vList [2].transform.position;
				avgX = f.vList [0].transform.position.x;
				avgY = Mathf.Min (temp1.y, temp2.y) + Mathf.Abs ((temp1.y - temp2.y) / 2);
				avgZ = Mathf.Min (temp1.z, temp2.z) + Mathf.Abs ((temp1.z - temp2.z) / 2);
				if (fMode) {
					GameObject tempXGO = Instantiate (faceHandler, new Vector3 (avgX, avgY, avgZ), Quaternion.identity);
					tempXGO.name = "Face" + "_" + gameObject.name + "_" + i;
					tempXGO.tag = "Face";
					tempXGO.AddComponent<Extrude> ();
					tempXGO.GetComponent<Extrude> ().shareAxis = 'X';
					tempXGO.GetComponent<Extrude> ().parent = this.gameObject;
					foreach (GameObject g in f.vList) {
						if (g.transform.parent)
							tempXGO.GetComponent<Extrude> ().listOfVerticesObjects.Add (g.transform.parent.gameObject);
						else
							tempXGO.GetComponent<Extrude> ().listOfVerticesObjects.Add (g);
					}
				}

				if (cfMode) {
					GameObject tempXGO = Instantiate (faceHandler, new Vector3 (avgX, avgY, avgZ), Quaternion.identity);
					tempXGO.GetComponent<Renderer> ().material.color = Color.cyan;
					tempXGO.name = "Face" + "_" + gameObject.name + "_" + i;
					tempXGO.tag = "Face";
					tempXGO.AddComponent<CreateFace> ();
					tempXGO.GetComponent<CreateFace> ().shareAxis = 'X';
					tempXGO.GetComponent<CreateFace> ().parent = this.gameObject;
					foreach (GameObject g in f.vList) {
						if (g.transform.parent)
							tempXGO.GetComponent<CreateFace> ().listOfVerticesObjects.Add (g.transform.parent.gameObject);
						else
							tempXGO.GetComponent<CreateFace> ().listOfVerticesObjects.Add (g);
					}
				}
				break;

			case "Share Y Axis":
				temp1 = f.vList [0].transform.position;
				temp2 = f.vList [2].transform.position;
				avgX = Mathf.Min (temp1.x, temp2.x) + Mathf.Abs ((temp1.x - temp2.x) / 2);
				avgY = f.vList [0].transform.position.y;
				avgZ = Mathf.Min (temp1.z, temp2.z) + Mathf.Abs ((temp1.z - temp2.z) / 2);
				if (fMode) {
					GameObject tempYGO = Instantiate (faceHandler, new Vector3 (avgX, avgY, avgZ), Quaternion.identity);
					tempYGO.name = "Face" + "_" + gameObject.name + "_" + i;
					tempYGO.tag = "Face";
					tempYGO.AddComponent<Extrude> ();
					tempYGO.GetComponent<Extrude> ().shareAxis = 'Y';
					tempYGO.GetComponent<Extrude> ().parent = this.gameObject;
					foreach (GameObject g in f.vList) {
						if (g.transform.parent)
							tempYGO.GetComponent<Extrude> ().listOfVerticesObjects.Add (g.transform.parent.gameObject);
						else
							tempYGO.GetComponent<Extrude> ().listOfVerticesObjects.Add (g);
					}
				}

				if (cfMode) {
					GameObject tempYGO = Instantiate (faceHandler, new Vector3 (avgX, avgY, avgZ), Quaternion.identity);
					tempYGO.GetComponent<Renderer> ().material.color = Color.cyan;
					tempYGO.name = "Face" + "_" + gameObject.name + "_" + i;
					tempYGO.tag = "Face";
					tempYGO.AddComponent<CreateFace> ();
					tempYGO.GetComponent<CreateFace> ().shareAxis = 'Y';
					tempYGO.GetComponent<CreateFace> ().parent = this.gameObject;
					foreach (GameObject g in f.vList) {
						if (g.transform.parent)
							tempYGO.GetComponent<CreateFace> ().listOfVerticesObjects.Add (g.transform.parent.gameObject);
						else
							tempYGO.GetComponent<CreateFace> ().listOfVerticesObjects.Add (g);
					}
				}
				break;

			case "Share Z Axis":
				temp1 = f.vList [0].transform.position;
				temp2 = f.vList [2].transform.position;
				avgX = Mathf.Min (temp1.x, temp2.x) + Mathf.Abs ((temp1.x - temp2.x) / 2);
				avgY = Mathf.Min (temp1.y, temp2.y) + Mathf.Abs ((temp1.y - temp2.y) / 2);
				avgZ = f.vList [0].transform.position.z;

				if (fMode) {
					GameObject tempZGO = Instantiate (faceHandler, new Vector3 (avgX, avgY, avgZ), Quaternion.identity);
					tempZGO.name = "Face" + "_" + gameObject.name + "_" + i;
					tempZGO.tag = "Face";
					tempZGO.AddComponent<Extrude> ();
					tempZGO.GetComponent<Extrude> ().shareAxis = 'Z';
					tempZGO.GetComponent<Extrude> ().parent = this.gameObject;
					foreach (GameObject g in f.vList) {
						if (g.transform.parent)
							tempZGO.GetComponent<Extrude> ().listOfVerticesObjects.Add (g.transform.parent.gameObject);
						else
							tempZGO.GetComponent<Extrude> ().listOfVerticesObjects.Add (g);
					}
				}

				if (cfMode) {
					GameObject tempZGO = Instantiate (faceHandler, new Vector3 (avgX, avgY, avgZ), Quaternion.identity);
					tempZGO.GetComponent<Renderer> ().material.color = Color.cyan;
					tempZGO.name = "Face" + "_" + gameObject.name + "_" + i;
					tempZGO.tag = "Face";
					tempZGO.AddComponent<CreateFace> ();
					tempZGO.GetComponent<CreateFace> ().shareAxis = 'Z';
					tempZGO.GetComponent<CreateFace> ().parent = this.gameObject;
					foreach (GameObject g in f.vList) {
						if (g.transform.parent)
							tempZGO.GetComponent<CreateFace> ().listOfVerticesObjects.Add (g.transform.parent.gameObject);
						else
							tempZGO.GetComponent<CreateFace> ().listOfVerticesObjects.Add (g);
					}
				}
				break;
			}
			i++;
		}
	}

	public void reCalcMidPoint(){
		int i = 0;
		float avgX, avgY, avgZ;
		Vector3 temp1;
		Vector3 temp2;
		GameObject tempGO;
		foreach (Face f in listOfFaces) {
			avgX = avgY = avgZ = 0f;

			switch (f.shareAxis) {
			case "Share X Axis":
				temp1 = f.vList [0].transform.position;
				temp2 = f.vList [2].transform.position;
				avgX = f.vList [0].transform.position.x;
				avgY = Mathf.Min (temp1.y, temp2.y) + Mathf.Abs ((temp1.y - temp2.y) / 2);
				avgZ = Mathf.Min (temp1.z, temp2.z) + Mathf.Abs ((temp1.z - temp2.z) / 2);
				tempGO = GameObject.Find ("Face" + "_" + gameObject.name + "_" + i);
				tempGO.transform.position = new Vector3 (avgX, avgY, avgZ);
				break;
			case "Share Y Axis":
				temp1 = f.vList [0].transform.position;
				temp2 = f.vList [2].transform.position;
				avgX = Mathf.Min (temp1.x, temp2.x) + Mathf.Abs ((temp1.x - temp2.x) / 2);
				avgY = f.vList [0].transform.position.y;
				avgZ = Mathf.Min (temp1.z, temp2.z) + Mathf.Abs ((temp1.z - temp2.z) / 2);
				tempGO = GameObject.Find ("Face" + "_" + gameObject.name + "_" + i);
				tempGO.transform.position = new Vector3 (avgX, avgY, avgZ);
				break;
			case "Share Z Axis":
				temp1 = f.vList [0].transform.position;
				temp2 = f.vList [2].transform.position;
				avgX = Mathf.Min (temp1.x, temp2.x) + Mathf.Abs ((temp1.x - temp2.x) / 2);
				avgY = Mathf.Min (temp1.y, temp2.y) + Mathf.Abs ((temp1.y - temp2.y) / 2);
				avgZ = f.vList [0].transform.position.z;
				tempGO = GameObject.Find ("Face" + "_" + gameObject.name + "_" + i);
				tempGO.transform.position = new Vector3 (avgX, avgY, avgZ);
				break;
			}
			i++;
		}
	}

	public void VertexMode(){
		Debug.Log ("Vertex Mode");
		vMode = !vMode;
		if (vMode) {
			foreach (GameObject v in listOfV) {
				if (!v.transform.parent)
					v.SetActive (true);
			}

			GameObject[] tempList = GameObject.FindGameObjectsWithTag ("Face");
			foreach (GameObject f in tempList) {
				Destroy (f);
			}
		} else {
			foreach (GameObject go in listOfV) {
				go.SetActive(false);
			}
		}
	}

	public void FaceMode(){
		Debug.Log ("Face Mode");
		fMode = !fMode;
		foreach (GameObject go in listOfV) {
			go.SetActive(false);
		}

		//Destroy any face objects if there are any on the screen before creating new face objects
		GameObject[] tempList = GameObject.FindGameObjectsWithTag ("Face");
		foreach (GameObject f in tempList) {
			Destroy (f);
		}


		listOfFaces.Clear ();
		isSameFace = false;
		detectFaces ();
		calcMidPoint ();
	}

	public void CreateFace(){
		Debug.Log ("Create Face Mode");
		cfMode = !cfMode;

		foreach (GameObject go in listOfV) {
			go.SetActive(false);
		}

		GameObject[] tempList = GameObject.FindGameObjectsWithTag ("Face");
		foreach (GameObject f in tempList) {
			Destroy (f);
		}

		listOfFaces.Clear ();
		isSameFace = false;
		detectFaces ();
		calcMidPoint ();
	}
}