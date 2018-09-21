using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVertex : MonoBehaviour {
    public GameObject[] listOfV;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyUp(KeyCode.A)) {
            listOfV = GameObject.FindGameObjectsWithTag("V");
            foreach(var v in listOfV) {
                v.transform.position = new Vector3(1f, 2f, 3f);
            }
        }
	}
}
