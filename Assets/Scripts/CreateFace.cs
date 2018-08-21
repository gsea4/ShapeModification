using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFace : MonoBehaviour {
	
	public List<GameObject> listOfVerticesObjects = new List<GameObject>();
	public char shareAxis;
	public GameObject parent;

	public Material mat;
	public static int counter = 0;
	public GameObject prefab;
	public GameObject vPrefab;
	public List<GameObject> vList = new List<GameObject>();
	public Vector3[] tempList;
	public Vector3[] vertices;

	GameObject vertexHandler;

	// Use this for initialization
	void Start () {
		vPrefab = Resources.Load ("VertexHandler") as GameObject;
		prefab = Resources.Load ("Empty") as GameObject;
		mat = Resources.Load ("New Material") as Material;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseUp(){
		vList.Clear ();
		vList.Add (listOfVerticesObjects [0]);
		vList.Add (listOfVerticesObjects [1]);
		vList.Add (listOfVerticesObjects [2]);
		vList.Add (listOfVerticesObjects [listOfVerticesObjects.Count - 1]);

		createFace ();
	}

	public void createFace(){
		GameObject temp = Instantiate (prefab, vList [0].transform.position, Quaternion.identity);
		temp.transform.localPosition = Vector3.zero;
		counter++;
		temp.tag = "MO";
		temp.name = "NewObject_" + counter + "_";


		Mesh mesh;
		temp.GetComponent<MeshFilter> ().mesh = mesh = new Mesh ();
		mesh.Clear ();

		bool shareX = true;
		bool shareY = true;
		bool shareZ = true;

		for (int i = 1; i < vList.Count; i++) {
			if (vList [i].transform.position.x != vList [i - 1].transform.position.x)
				shareX = false;

			if (vList [i].transform.position.y != vList [i - 1].transform.position.y)
				shareY = false;

			if (vList [i].transform.position.z != vList [i - 1].transform.position.z)
				shareZ = false;
		}

		int c = vList.Count;
		for (int i = 0; i < c; i++) {
			if (shareX) {
				GameObject temp1 = Instantiate (vPrefab, vList [i].transform.position + Vector3.right, Quaternion.identity);
				vList.Add (temp1);
				temp1.gameObject.name = "ToBeDestroyed";
				temp1.gameObject.tag = "Destroyed";
			}

			if (shareY) {
				GameObject temp1 = Instantiate (vPrefab, vList [i].transform.position + Vector3.up, Quaternion.identity);
				vList.Add (temp1);
				temp1.gameObject.name = "ToBeDestroyed";
				temp1.gameObject.tag = "Destroyed";
			}

			if (shareZ) {
				GameObject temp1 = Instantiate (vPrefab, vList [i].transform.position + Vector3.back, Quaternion.identity);
				vList.Add (temp1);
				temp1.gameObject.name = "ToBeDestroyed";
				temp1.gameObject.tag = "Destroyed";
			}
		}


		vertices = new Vector3[] {
			// Bottom
			vList [0].transform.position, vList [1].transform.position, vList [2].transform.position, vList [3].transform.position,

			// Left
			vList [7].transform.position, vList [4].transform.position, vList [0].transform.position, vList [3].transform.position,

			// Front
			vList [4].transform.position, vList [5].transform.position, vList [1].transform.position, vList [0].transform.position,

			// Back
			vList [6].transform.position, vList [7].transform.position, vList [3].transform.position, vList [2].transform.position,

			// Right
			vList [5].transform.position, vList [6].transform.position, vList [2].transform.position, vList [1].transform.position,

			// Top
			vList [7].transform.position, vList [6].transform.position, vList [5].transform.position, vList [4].transform.position,
		};

		int[] triangles = new int[] {
			// Bottom
			3, 1, 0,
			3, 2, 1,			

			// Left
			3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
			3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,

			// Front
			3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
			3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,

			// Back
			3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
			3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,

			// Right
			3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
			3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,

			// Top
			3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,
			3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,

		};

		mesh.vertices = vertices;
		mesh.triangles = triangles;

		mesh.RecalculateBounds ();
		mesh.RecalculateNormals ();

		Vector2 _00 = new Vector2 (0f, 0f);
		Vector2 _10 = new Vector2 (1f, 0f);
		Vector2 _01 = new Vector2 (0f, 1f);
		Vector2 _11 = new Vector2 (1f, 1f);

		Vector2[] uvs = new Vector2[] {
			// Bottom
			_11, _01, _00, _10,

			// Left
			_11, _01, _00, _10,

			// Front
			_11, _01, _00, _10,

			// Back
			_11, _01, _00, _10,

			// Right
			_11, _01, _00, _10,

			// Top
			_11, _01, _00, _10,
		};

		temp.GetComponent<MeshRenderer> ().material = mat;
		mesh.uv = uvs;

		//mesh.uv = GameObject.Find ("test").GetComponent<MeshFilter> ().mesh.uv;
		//mesh.uv2 = GameObject.Find ("test").GetComponent<MeshFilter> ().mesh.uv2;



		//Add modify script and remove unncessary components
		GameObject[] tempList = GameObject.FindGameObjectsWithTag ("Destroyed");
		foreach (GameObject g in tempList) {
			Destroy (g);
		}
		vList.Clear ();

		temp.AddComponent<Modify_2> ();

		//GameObject faceManagerTemp = GameObject.Find ("FaceManager");
		//faceManagerTemp.GetComponent<FaceModeManager> ().TestActivate ();
		//faceManagerTemp.GetComponent<FaceModeManager> ().TestActivate ();
		//Destroy (faceManagerTemp);
	}
}