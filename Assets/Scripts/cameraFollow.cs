using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform playerTarget;
    public float smoothSpeed = 0.05f;
    private Vector3 offset = new Vector3(-8, 12, -7);
    void Start()
    {
        
    }


    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, (playerTarget.position + offset), smoothSpeed);
    }
}
