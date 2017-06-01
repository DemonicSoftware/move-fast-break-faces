using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

  	public Transform target;
	public float cam_speed;
	Camera mycam;

	// Use this for initialization
	void Start () {

		mycam = GetComponent<Camera> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		mycam.orthographicSize = (Screen.height / 100f) / 0.6f;
        print(mycam.orthographicSize);

		if (target) 
		{
			transform.position = Vector3.Lerp (transform.position, target.position, cam_speed) + new Vector3(0, 0, -10);
		}

	}
}