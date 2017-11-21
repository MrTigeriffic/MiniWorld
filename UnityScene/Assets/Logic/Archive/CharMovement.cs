using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;//Keyboard input

using UnityEngine;

public class CharMovement : MonoBehaviour {

    private Animator ThisAnimator = null;
    private int VertHash = Animator.StringToHash("Vertical");
    private int HorzHash = Animator.StringToHash("Horizontal");
    // Use this for initialization
    void Awake () {
        ThisAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float horz = CrossPlatformInputManager.GetAxis("Horizontal");
        float vert = CrossPlatformInputManager.GetAxis("Vertical");

        ThisAnimator.SetFloat(HorzHash, horz, 0.1f, Time.deltaTime);
        ThisAnimator.SetFloat(VertHash, vert, 0.1f, Time.deltaTime);

        /*The SetFloat uses String in its first argument and dealing with strings in an update function is poor for performance 
          Unity allows you to convert the strings into hashes. Creating 2 new variables and using the StringToHash.            
         */
    }
}
