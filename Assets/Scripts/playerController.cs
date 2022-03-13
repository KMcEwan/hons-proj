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
    float movementSpeed = 1000f;
    float NormalMovementSpeed = 200;
    float crouchMovementSpeed = 35;
    float runMovementSpeed = 300;
    Vector3 playersForwardDirection;
    Vector3 playersRightDirection;

    Rigidbody rb;

    //Players forward raycast hit object holder
    public GameObject lastObjectHit;

    //Layer mask for camera to mouse 
    int ignoreLayerMask = 1 << 7;

    public audioColliderForCollision audioCollider;


    [SerializeField] private gunsController gunController;

    // rotation speed
    public float lookAtSpeed = 3600f;

    // cursors
    public Texture2D inactiveCursor;
    public Texture2D activeCursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot;

    void Start()
    {
        //Player movement
        playersForwardDirection = Camera.main.transform.forward;                                                        // player uses the cameras forward direction rather than the forward vector of the scene
        playersForwardDirection.y = 0;
        playersForwardDirection = Vector3.Normalize(playersForwardDirection);
        playersRightDirection = Quaternion.Euler(new Vector3(0, 90, 0)) * playersForwardDirection;
        rb = GetComponent<Rigidbody>();

        hotSpot = new Vector2(inactiveCursor.width / 2, inactiveCursor.height / 2);

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



       // //RAYCAST FOR PLAYER POINTING FORWARD
       //Vector3 originRaycast = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
       // Ray playerForwardRay = new Ray(originRaycast, transform.forward);
       // RaycastHit playerForwardHit;

       // Debug.DrawRay(originRaycast, transform.forward, Color.yellow);

       // if (Physics.Raycast(playerForwardRay, out playerForwardHit, 10))
       // {
       //     lastObjectHit = playerForwardHit.transform.gameObject;
       //     Debug.Log(lastObjectHit);
       // }









        // RAYCAST FOR PLAYER LOOK AT MOUSE POS
        Ray camToMouse;
        RaycastHit camToMouseHitData;
        Vector3 worldPosition;
        camToMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(camToMouse, out camToMouseHitData, 1000, ignoreLayerMask))
        {
            worldPosition = camToMouseHitData.point;
            Vector3 playerToMouseDirection = worldPosition - transform.position;
            Quaternion lookRotationToMouse = Quaternion.LookRotation(playerToMouseDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotationToMouse, Time.deltaTime * (lookAtSpeed / 360f));

        }


        Vector3 worldPosition2;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData, 1000))
        {
            worldPosition2 = hitData.point;
            Debug.Log(hitData.transform.gameObject.name);
            lastObjectHit = hitData.transform.gameObject;

            if (hitData.transform.gameObject.tag == "enemy")
            {
                Debug.Log("enemy hit in cursor change");
                Cursor.SetCursor(activeCursor, hotSpot, cursorMode);
            }
            else
            {
                Cursor.SetCursor(inactiveCursor, hotSpot, cursorMode);
            }
        }
    }



    private void FixedUpdate()
    {
        Vector3 movementDirection = playersRightDirection * Input.GetAxis("Horizontal") + playersForwardDirection * Input.GetAxis("Vertical");
        movementDirection = Vector3.ClampMagnitude(movementDirection, 1);
        rb.AddForce(movementDirection * movementSpeed, ForceMode.Force);
    }
    void characterMovement()
    {
        //Debug.Log("character movement");
        if (audioCollider.isCrouched)
        {            
            movementSpeed = crouchMovementSpeed;
        }
        else if (Input.GetKey((KeyCode.LeftShift)))
        {
            audioCollider.isCrouched = false;
            movementSpeed = runMovementSpeed;
            animationController.SetFloat("runMultiplier", 2.0f);
        }
        else
        {
            animationController.SetFloat("runMultiplier", 1.0f);
            movementSpeed = NormalMovementSpeed;
        }

    }
}
