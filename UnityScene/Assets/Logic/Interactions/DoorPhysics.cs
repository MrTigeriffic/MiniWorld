using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPhysics : MonoBehaviour {

    //public Vector3 velocity;
    public float pushPower = 20.0f;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            
        return;
        if (hit.moveDirection.y < -3f)
            //Debug.Log("Moving");
        return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
    }

    ////Use this for initialization
    //void OnCollisionEnter(Collision col)
    //{
    //    Debug.Log(gameObject.name + "has collided with" + col.gameObject.name);
    //    col = gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * 500, ForceMode.Force);
    //}

    ////Update is called once per frame
    //void OnTriggerEnter(Collider other)
    //{

    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.name == "Door")
    //    {
    //        Debug.Log("Hitting the Cube");
    //    }
    //}

}
