using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerController : MonoBehaviour
{

    public CharacterController characterController;


    //player movement
    [SerializeField]
    private float movementSpeed = 5f;
    Rigidbody rb;
    Vector3 playersForwardDirection;
    Vector3 playersRightDirection;


    //Players forward raycast hit object holder
   public GameObject lastObjectHit;

    //Layer mask for camera to mouse 
    int ignoreLayerMask = 1 << 7;



    void Start()
    {
        //Player movement
        playersForwardDirection = Camera.main.transform.forward;            // player uses the cameras forward direction rather than the forward vector of the scene
        playersForwardDirection.y = 0;
        playersForwardDirection = Vector3.Normalize(playersForwardDirection);
        playersRightDirection = Quaternion.Euler(new Vector3(0, 90, 0)) * playersForwardDirection;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.anyKey)
        {
            characterMovement();
        }

   

        // RAYCAST FOR PLAYER POINTING FORWARD
        Ray playerForwardRay = new Ray(transform.position, transform.forward);
        RaycastHit playerForwardHit;
        Debug.DrawRay(transform.position, transform.forward, Color.magenta);
        if (Physics.Raycast(playerForwardRay, out playerForwardHit, Mathf.Infinity))
        {
            lastObjectHit = playerForwardHit.transform.gameObject;
          
            //Debug.Log(lastObjectHit.name);
        }

       // Debug.Log("Ray hit : " + lastObjectHit);

        // RAYCAST FOR PLAYER LOOK AT MOUSE POS
        Ray camToMouse;
        RaycastHit camToMouseHitData;
        Vector3 worldPosition;
        camToMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
        //ignoreLayerMask = ~ignoreLayerMask;
        if (Physics.Raycast(camToMouse, out camToMouseHitData, 1000, ignoreLayerMask))
        {
            worldPosition = camToMouseHitData.point;
            transform.LookAt(new Vector3(worldPosition.x, transform.position.y, worldPosition.z));
            Debug.Log(camToMouseHitData);
        }


        if(Input.GetKey(KeyCode.E))
        {
            GetComponent<SphereCollider>().radius = 5;
        }
        else
        {
            GetComponent<SphereCollider>().radius = 2;
        }

    }
    void characterMovement()
    {
        Vector3 ActualSpeed = playersRightDirection * Input.GetAxis("Horizontal") + playersForwardDirection * Input.GetAxis("Vertical");
        ActualSpeed = Vector3.ClampMagnitude(ActualSpeed, 1);
        rb.AddForce(ActualSpeed * movementSpeed, ForceMode.Force);
    }

   
}
