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
    float crouchMovementSpeed = 100;
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


        //hotSpot for cursor placement
        hotSpot = new Vector2(inactiveCursor.width / 2, inactiveCursor.height / 2);

    }

    void Update()
    {
        if (Input.anyKey)
        {
            characterMovement();
        }

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

        //test code

        Vector3 playerPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

        // test code end


        //RAYCAST OF CAMERA TO MOUSE POSITION
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    
        RaycastHit hitData;
        RaycastHit hitDataToMouse;
        if (Physics.Raycast(ray, out hitData, 1000))
        {
            Debug.Log(hitData.transform.gameObject.name);
            lastObjectHit = hitData.transform.gameObject;
            worldPosition = hitData.point;


            if (Physics.Raycast(playerPosition, (worldPosition - playerPosition), out hitDataToMouse, 100))
            {
                Debug.Log(hitDataToMouse.transform.gameObject.tag);
                if (gunController.pistolActive || gunController.SMGActive)
                {
                    if (hitData.transform.gameObject.tag == "enemy" && hitDataToMouse.transform.gameObject.tag == "enemy")
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
            //Debug.Log(worldPosition);
            Debug.DrawLine(playerPosition, worldPosition, Color.red);
        }
    }



    private void FixedUpdate()
    {
        Vector3 movementDirection = playersRightDirection * Input.GetAxis("Horizontal") + playersForwardDirection * Input.GetAxis("Vertical");
        movementDirection = Vector3.ClampMagnitude(movementDirection, 1);
        rb.AddForce(movementDirection * movementSpeed, ForceMode.Force);

        playerYPos = transform.rotation.eulerAngles.y;
        float playerYNorm = playerYPos / 100f;

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            animationController.SetFloat("WD_movement", playerYNorm/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("W_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("D_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("DS_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("S_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("AS_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("A_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("WA_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
        }
        else
     if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            animationController.SetFloat("DS_movement", playerYNorm/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("WD_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("W_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("D_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("S_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("AS_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("A_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("WA_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
        }
        else
     if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            animationController.SetFloat("AS_movement", playerYNorm/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("WD_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("W_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("D_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("S_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("DS_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("A_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("WA_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
        }
        else
     if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            animationController.SetFloat("WA_movement", playerYNorm/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("WD_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("W_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("D_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("S_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("DS_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("A_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("AS_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
        }
        else
     if (Input.GetKey(KeyCode.W))
        {
            animationController.SetFloat("W_movement", playerYNorm/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("WD_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("D_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("DS_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("S_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("AS_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("A_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("WA_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
        }
        else
     if (Input.GetKey(KeyCode.D))
        {
            animationController.SetFloat("D_movement", playerYNorm/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("WD_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("W_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("DS_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("S_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("AS_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("A_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("WA_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
        }
        else
     if (Input.GetKey(KeyCode.S))
        {
            animationController.SetFloat("S_movement", playerYNorm/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("WD_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("W_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("DS_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("D_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("AS_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("A_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("WA_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
        }
        else
     if (Input.GetKey(KeyCode.A))
        {
            animationController.SetFloat("A_movement", playerYNorm/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("WD_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("W_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("DS_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("D_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("AS_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("S_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
            animationController.SetFloat("WA_movement", -0.1f/*, 1f, Time.deltaTime * 10f*/);
        }
        else
        {
            // Debug.Log("no key pressed");
            animationController.SetFloat("WD_movement", -0.2f, 1f, Time.deltaTime * 10f);
            animationController.SetFloat("W_movement", -0.2f, 1f, Time.deltaTime * 10f);
            animationController.SetFloat("D_movement", -0.2f, 1f, Time.deltaTime * 10f);
            animationController.SetFloat("DS_movement", -0.2f, 1f, Time.deltaTime * 10f);
            animationController.SetFloat("AS_movement", -0.2f, 1f, Time.deltaTime * 10f);
            animationController.SetFloat("A_movement", -0.2f, 1f, Time.deltaTime * 10f);
            animationController.SetFloat("S_movement", -0.2f, 1f, Time.deltaTime * 10f);
            animationController.SetFloat("WA_movement", -0.2f, 1f, Time.deltaTime * 10f);
        }


    }
    void characterMovement()
    {
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
