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

        if(isCrouched)
        {
            colliderForAudio.radius = 0.5f;
        }
        else if(!isCrouched)
        {
            colliderForAudio.radius = 1.5f;
        }

        if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && isCrouched)
        {
            colliderForAudio.radius = 1.5f;
        }
        else 
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && Input.GetKey(KeyCode.LeftShift))
        {
            colliderForAudio.radius = 5.0f;
        }
        else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            colliderForAudio.radius = 3.0f;
        }


        
        //audioRadiusColliders = Physics.OverlapSphere(this.transform.position, radiusForAudio);
        //Debug.Log(radiusForAudio);
    }
    
   

}

