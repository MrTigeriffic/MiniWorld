using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAnimControl : MonoBehaviour
{
	
	private Animator ThisAnimator = null;

	//static function to get axis and remove string variable in the update function
	private int VertHash = Animator.StringToHash("Vertical");
	private int HorzHash = Animator.StringToHash("Horizontal");

	// Use this for initialization
	void Awake ()
	{
		ThisAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		float horz = CrossPlatformInputManager.GetAxis("Horizontal");
		float vert = CrossPlatformInputManager.GetAxis("Vertical");

		ThisAnimator.SetFloat(HorzHash, horz, 0.1f, Time.deltaTime);
		ThisAnimator.SetFloat(VertHash, vert, 0.1f, Time.deltaTime);
	}
}
