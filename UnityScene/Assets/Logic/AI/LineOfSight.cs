using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour {

    public enum SightSensitivity {SRTICT, LOOSE};

    public SightSensitivity Sensitiv = SightSensitivity.SRTICT;

    private Transform ThisTransform = null;
    private SphereCollider ThisCollider = null;

    public Vector3 LastknownSighting = Vector3.zero;

    void Awake()
    {
        ThisTransform = GetComponent<Transform>();
        ThisCollider = GetComponent<SphereCollider>();
        LastknownSighting = ThisTransform.position;
    }

    //------------------------------------------------------
    //FOV
    public float FieldOfView = 45f;
    
    //ref to target
    public Transform Target = null;

    //ref to eyes
    public Transform EyeLevel = null;

    //Can see the target
    public bool CanSeeTarget = false;
    //-------------------------------------------------------

    bool InFOV()
    {
        //get direction to target
        Vector3 DirToTarget = Target.position - EyeLevel.position;

        //get the angle between forward and look direction
        float Angle = Vector3.Angle(EyeLevel.forward, DirToTarget);

        //check if we are in the field of view
        if (Angle <= FieldOfView)
            return true;

        //not within view
        return false;
    }

    bool ClearlineOfSight()
    {
        RaycastHit Info;
        //using a raycast to the player and checking if object in the way
        if(Physics.Raycast(EyeLevel.position, (Target.position - EyeLevel.position).normalized, out Info, ThisCollider.radius))
        {
            //if npc raycast detects tag Player 
            if (Info.transform.CompareTag("Player"))
                return true;
        }
        return false;
    }
    void UpdateSight()
    {
        switch (Sensitiv)
        {
            case SightSensitivity.SRTICT:
                CanSeeTarget = InFOV() && ClearlineOfSight();
                break;

            case SightSensitivity.LOOSE:
                CanSeeTarget = InFOV() || ClearlineOfSight();
                break;
        }
    }
    private void OnTriggerStay(Collider other)//reference to the collider that enters the trigger
    {
        //Debug.Log(other.name);
        UpdateSight();
        if (CanSeeTarget)
            LastknownSighting = Target.position;
    }
}
