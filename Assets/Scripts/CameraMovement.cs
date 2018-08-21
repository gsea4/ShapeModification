using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public float lookSpeedH = 2f;
	public float lookSpeedV = 2f;
	public float zoomSpeed = 10f;
	public float dragSpeed = 6f;
	public float movementSpeed = 1f;

	private float yaw = 0f;
	private float pitch = 0f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (1)) {
			yaw += lookSpeedH * Input.GetAxis ("Mouse X");
			pitch -= lookSpeedV * Input.GetAxis ("Mouse Y");

			transform.eulerAngles = new Vector3 (pitch, yaw, 0f);
		}

		if (Input.GetMouseButton (2)) {
			transform.Translate (-Input.GetAxisRaw ("Mouse X") * Time.deltaTime * dragSpeed, -Input.GetAxisRaw ("Mouse Y") * Time.deltaTime * dragSpeed, 0);
		}

		transform.Translate (0, 0, Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed, Space.Self);

		if (Input.GetKey (KeyCode.UpArrow)) {
			transform.Translate (Vector3.forward * movementSpeed);
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			transform.Translate (Vector3.back * movementSpeed);
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate (Vector3.left * movementSpeed);
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate (Vector3.right * movementSpeed);
		}
	}
}
