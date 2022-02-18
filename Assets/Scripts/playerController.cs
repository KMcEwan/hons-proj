using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class playerController : MonoBehaviour
{
    // Animation
    [SerializeField] private Animator animationController;
    float playerYPos;

    //player movement
    public float movementSpeed = 10f;


    Rigidbody rb;
    Vector3 playersForwardDirection;
    Vector3 playersRightDirection;


    //Players forward raycast hit object holder
   public GameObject lastObjectHit;

    //Layer mask for camera to mouse 
    int ignoreLayerMask = 1 << 7;

    public audioColliderForCollision audioCollider;


    [SerializeField] private gunsController gunController;


    void Start()
    {
        //Player movement
        playersForwardDirection = Camera.main.transform.forward;                                                        // player uses the cameras forward direction rather than the forward vector of the scene
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
        //Debug.Log(playersForwardDirection);
        //Debug.Log(playersRightDirection);


        playerYPos = transform.rotation.eulerAngles.y;
        float playerYNorm = playerYPos / 100f;
       //Debug.Log(playerYNorm);


        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            animationController.SetFloat("WD_movement", playerYNorm);
            animationController.SetFloat("W_movement", -0.1f);
            animationController.SetFloat("D_movement", -0.1f);
            animationController.SetFloat("DS_movement", -0.1f);
            animationController.SetFloat("S_movement", -0.1f);
            animationController.SetFloat("AS_movement", -0.1f);
            animationController.SetFloat("A_movement", -0.1f);
            animationController.SetFloat("WA_movement", -0.1f);
        }
        else
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            animationController.SetFloat("DS_movement", playerYNorm);
            animationController.SetFloat("WD_movement", -0.1f);
            animationController.SetFloat("W_movement", -0.1f);
            animationController.SetFloat("D_movement", -0.1f);
            animationController.SetFloat("S_movement", -0.1f);
            animationController.SetFloat("AS_movement", -0.1f);
            animationController.SetFloat("A_movement", -0.1f);
            animationController.SetFloat("WA_movement", -0.1f);
        }
        else
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            animationController.SetFloat("AS_movement", playerYNorm);
            animationController.SetFloat("WD_movement", -0.1f);
            animationController.SetFloat("W_movement", -0.1f);
            animationController.SetFloat("D_movement", -0.1f);
            animationController.SetFloat("S_movement", -0.1f);
            animationController.SetFloat("DS_movement", -0.1f);
            animationController.SetFloat("A_movement", -0.1f);
            animationController.SetFloat("WA_movement", -0.1f);
        }
        else
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            animationController.SetFloat("WA_movement", playerYNorm);
            animationController.SetFloat("WD_movement", -0.1f);
            animationController.SetFloat("W_movement", -0.1f);
            animationController.SetFloat("D_movement", -0.1f);
            animationController.SetFloat("S_movement", -0.1f);
            animationController.SetFloat("DS_movement", -0.1f);
            animationController.SetFloat("A_movement", -0.1f);
            animationController.SetFloat("AS_movement", -0.1f);
        }
        else
        if (Input.GetKey(KeyCode.W))
        {
            animationController.SetFloat("W_movement", playerYNorm);
            animationController.SetFloat("WD_movement", -0.1f);
            animationController.SetFloat("D_movement", -0.1f);
            animationController.SetFloat("DS_movement", -0.1f);
            animationController.SetFloat("S_movement", -0.1f);
            animationController.SetFloat("AS_movement", -0.1f);
            animationController.SetFloat("A_movement", -0.1f);
            animationController.SetFloat("WA_movement", -0.1f);
        }
        else
        if (Input.GetKey(KeyCode.D))
        {
            animationController.SetFloat("D_movement", playerYNorm);
            animationController.SetFloat("WD_movement", -0.1f);
            animationController.SetFloat("W_movement", -0.1f);
            animationController.SetFloat("DS_movement", -0.1f);
            animationController.SetFloat("S_movement", -0.1f);
            animationController.SetFloat("AS_movement", -0.1f);
            animationController.SetFloat("A_movement", -0.1f);
            animationController.SetFloat("WA_movement", -0.1f);
        }
        else
        if (Input.GetKey(KeyCode.S))
        {
            animationController.SetFloat("S_movement", playerYNorm);
            animationController.SetFloat("WD_movement", -0.1f);
            animationController.SetFloat("W_movement", -0.1f);
            animationController.SetFloat("DS_movement", -0.1f);
            animationController.SetFloat("D_movement", -0.1f);
            animationController.SetFloat("AS_movement", -0.1f);
            animationController.SetFloat("A_movement", -0.1f);
            animationController.SetFloat("WA_movement", -0.1f);
        }
        else
        if (Input.GetKey(KeyCode.A))
        {
            animationController.SetFloat("A_movement", playerYNorm);
            animationController.SetFloat("WD_movement", -0.1f);
            animationController.SetFloat("W_movement", -0.1f);
            animationController.SetFloat("DS_movement", -0.1f);
            animationController.SetFloat("D_movement", -0.1f);
            animationController.SetFloat("AS_movement", -0.1f);
            animationController.SetFloat("S_movement", -0.1f);
            animationController.SetFloat("WA_movement", -0.1f);
        }
        else
        {
            // Debug.Log("no key pressed");
            animationController.SetFloat("WD_movement", -0.2f);
            animationController.SetFloat("W_movement", -0.2f);
            animationController.SetFloat("D_movement", -0.2f);
            animationController.SetFloat("DS_movement", -0.2f);
            animationController.SetFloat("AS_movement", -0.2f);
            animationController.SetFloat("A_movement", -0.2f);
            animationController.SetFloat("S_movement", -0.2f);
            animationController.SetFloat("WA_movement", -0.2f);
        }



        // RAYCAST FOR PLAYER POINTING FORWARD
        Ray playerForwardRay = new Ray(transform.position, transform.forward);
        RaycastHit playerForwardHit;
        Debug.DrawRay(transform.position, transform.forward, Color.magenta);
        if (Physics.Raycast(playerForwardRay, out playerForwardHit, Mathf.Infinity))
        {
            lastObjectHit = playerForwardHit.transform.gameObject;         
        }

        // RAYCAST FOR PLAYER LOOK AT MOUSE POS
        Ray camToMouse;
        RaycastHit camToMouseHitData;
        Vector3 worldPosition;
        camToMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(camToMouse, out camToMouseHitData, 1000, ignoreLayerMask))
        {
            worldPosition = camToMouseHitData.point;
            transform.LookAt(new Vector3(worldPosition.x, transform.position.y, worldPosition.z));
            //Debug.Log(camToMouseHitData);
        }
      
    }
    void characterMovement()
    {
        if (audioCollider.isCrouched)
        {
            movementSpeed = 5.0f;
        }


        Vector3 movementDirection = playersRightDirection * Input.GetAxis("Horizontal") + playersForwardDirection * Input.GetAxis("Vertical");
        movementDirection = Vector3.ClampMagnitude(movementDirection, 1);
        rb.AddForce(movementDirection * movementSpeed, ForceMode.Force);
    }


}
