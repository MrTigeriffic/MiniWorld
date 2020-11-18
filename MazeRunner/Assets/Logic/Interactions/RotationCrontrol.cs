using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCrontrol : MonoBehaviour {

	private Transform GoTransform = null;
	private Transform PivotTransform;

	public float velocity = 2.0f;
	// Use this for initialization

	void Awake () {
		GoTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		GoTransform.rotation = Quaternion.Euler(0f, 90f, 0f);
	}
}
