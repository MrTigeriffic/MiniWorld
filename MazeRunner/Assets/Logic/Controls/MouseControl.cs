using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    private const float YAngleMin = -10.0f;
    private const float YAngleMax = 50.0f;


    public Transform lootAt;
    public Transform camTransform;
    private Camera cam;

    private float distance = 5.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    public float sensitivityX = 8.0f;
    public float sensitivityY = 3.0f;

    private void Start()
    {
        camTransform = transform;
        cam = Camera.main;
    }

    private void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");
        currentY = Mathf.Clamp(currentY, YAngleMin, YAngleMax);
    }
    //move the player then calculate the position of camera, as camera and player are co depentant of eachother
    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0,2,-distance);
        Quaternion rotation = Quaternion.Euler(currentY,currentX,0);
        camTransform.position = lootAt.position + rotation * dir;
        camTransform.LookAt(lootAt.position);
    }
}
