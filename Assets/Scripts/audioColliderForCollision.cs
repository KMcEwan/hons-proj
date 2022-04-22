using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioColliderForCollision : MonoBehaviour
{
    [SerializeField] private Animator animationController;
    public bool isCrouched;
    public bool isRunning;

    SphereCollider colliderForAudio;


    private float colliderSizeForCrouched = 0.5f;
    private float colliderSizeForNotCrouched = 1.5f;
    private float colliderSizeForCrouchedAndWalking = 2.0f;
    private float colliderSizeForWalking = 5.0f;
    private float colliderSizeForShiftRunning = 10.0f;
    private float colliderSizeForShooting = 30.0f;


    //FOR AUDIO COLLIDER SIZE FOR WHEN GUNS FIRED
    [SerializeField] gunsController gunControllerScript;
    private float resetColliderSize = .5f;
    private float timeToReset;





    private void Start()
    {
        colliderForAudio = GetComponent<SphereCollider>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouched = !isCrouched;
        }
        
        if (isCrouched && Time.time > timeToReset)
        {
            colliderForAudio.radius = colliderSizeForCrouched;
        }
        else if (!isCrouched && Time.time > timeToReset)
        {
            colliderForAudio.radius = colliderSizeForNotCrouched;
        }



        if ((Input.GetMouseButtonDown(0) && gunControllerScript.pistolActive && gunControllerScript.PistolBulletsLeftInMag > 0) || (Input.GetMouseButton(0) && gunControllerScript.SMGActive && gunControllerScript.SMGBulletsLeftInMag > 0))
        {
            timeToReset = Time.time + resetColliderSize;
            colliderForAudio.radius = colliderSizeForShooting;
        }

        if (!(Input.GetMouseButtonDown(0) && gunControllerScript.pistolActive && gunControllerScript.PistolBulletsLeftInMag > 0) &&
                !(Input.GetMouseButton(0) && gunControllerScript.SMGActive && gunControllerScript.SMGBulletsLeftInMag > 0) &&
                Time.time > timeToReset)
        {
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && isCrouched)
            {

                colliderForAudio.radius = colliderSizeForCrouchedAndWalking;
            }
            else
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && Input.GetKey(KeyCode.LeftShift))
            {

                colliderForAudio.radius = colliderSizeForShiftRunning;
            }
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {

                colliderForAudio.radius = colliderSizeForWalking;
            }

        }          

    } 


}

