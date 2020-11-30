using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaceObject : MonoBehaviour
{
	public float DisplaceSpeed = 2f;
	private Transform Thistransform = null;

	[SerializeField]
	private Vector3 LocalForward;

	[SerializeField]
	private Vector3 TransformForward;

	// Use this for initialization
	void Awake ()
	{
		Thistransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		LocalForward = Vector3.forward;
		//this forward vector is expressed in world space and not local space to the line above
		TransformForward = Thistransform.forward;

		//World space displacement
		//Thistransform.position += Thistransform.forward * DisplaceSpeed * Time.deltaTime;
		
		//local space displacement
		Vector3 LovalSpaceDisplace = Vector3.forward * DisplaceSpeed * Time.deltaTime;
		//Convert local to world space
		//Vector3 WorldSpaceDisplace = Thistransform.TransformDirection(LovalSpaceDisplace);
		Vector3 WorldSpaceDisplace = Thistransform.rotation * LovalSpaceDisplace;

		Thistransform.position += WorldSpaceDisplace;
	}
}
