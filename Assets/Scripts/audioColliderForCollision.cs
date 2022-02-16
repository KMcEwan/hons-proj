using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioColliderForCollision : MonoBehaviour
{
    [SerializeField] private Animator animationController;
    public bool isCrouched;
    public bool isRunning;

    SphereCollider colliderForAudio;

    private void Start()
    {
        colliderForAudio = GetComponent<SphereCollider>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("Control pressed");
            isCrouched = !isCrouched;
        }

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && Input.GetKey(KeyCode.LeftShift))
        {
            colliderForAudio.radius = 5.0f;
        }
        else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            colliderForAudio.radius = 4.0f;
        }
        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && isCrouched)
        {
            colliderForAudio.radius = 3.0f;
        }
        else if (!isCrouched)
        {
            colliderForAudio.radius = 2.0f;
        }
        else if(isCrouched)
        {
            colliderForAudio.radius = 1.0f;
        }
        
        //audioRadiusColliders = Physics.OverlapSphere(this.transform.position, radiusForAudio);
        //Debug.Log(radiusForAudio);
    }
    
   

}

