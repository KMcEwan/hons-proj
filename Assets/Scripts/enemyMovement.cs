using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{
    // enemy movement
    [SerializeField] private bool needToMove;
    [SerializeField] private Vector3 moveTo;
    private NavMeshAgent navMeshAgent;

   // [SerializeField] private Transform currentPosition;
    [SerializeField] private Transform moveToPos;
    [SerializeField] Vector3 lastPos;
    [SerializeField] Vector3 currentPos;
    [SerializeField] Vector3 playersPosition;


    // player object to assign location when detected
    [SerializeField] GameObject playerObject;


    // enemy movement vars
    private float speed = 2;
    [SerializeField]  public bool atPostition;
    [SerializeField] public bool playerDetected;
    Vector3 capsuleRadius = new Vector3(0.5f, 0, 0.5f);

    float currentX;
    float currentZ;
    float newX;
    float newZ;


    // enemy ability to hit
     public SphereCollider rightHandCollider;
     public SphereCollider lefttHandCollider;

    // for animations
    [SerializeField] public Animator enemyAnim;

    // audio
    [SerializeField] AudioClip screechDetection;
    AudioSource audioSource;
    bool audioForDetection = false;




    // test position bool
    [SerializeField] bool CurrentEqualToLast;

    void Start()
    {
        atPostition = true;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        currentPos = transform.position;                                                         
        currentPos = (currentPos * 100);
        currentPos.x = Mathf.Floor(currentPos.x);
        currentPos.z = Mathf.Floor(currentPos.z);
        currentPos = new Vector3(currentPos.x, currentPos.y, currentPos.z);
        currentPos = (currentPos / 100);

        if (playerDetected)
        {
            moveTo = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, playerObject.transform.position.z);
            moveToPlayer();
        }
        if (lastPos == currentPos)
        {
            atPostition = true;
            enemyAnim.SetBool("playWalking", false);
            CurrentEqualToLast = true;
        }
        else
        {
            CurrentEqualToLast = false; 
        }
        lastPos = currentPos;
        if (atPostition && !playerDetected)
        {
            Debug.Log("AT POSITION");
            moveToRandomPosition();
        }



        if (atPostition && playerDetected)
        {
            enemyAnim.SetBool("playWalking", false);
        }
    }


    void moveToPlayer()
    {
        enemyAnim.SetBool("playWalking", true);
        Vector3 targetPostition = new Vector3(playerObject.transform.position.x, transform.position.y, playerObject.transform.position.z);
        transform.LookAt(targetPostition);
        navMeshAgent.destination = moveTo - capsuleRadius;

        if(!audioForDetection && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(screechDetection);
            audioForDetection = true;
        }


        if (Vector3.Distance(transform.position, playerObject.transform.position) < 1)
        {
            Debug.Log("position to player less than 1");
            playHitAnimations();
        }
        else
        {
            stopHitAnimation();
            atPostition = false;
        }
    }

    void moveToRandomPosition()
    {
        enemyAnim.SetBool("playWalking", true);
        atPostition = false;
        Debug.Log(atPostition);
        currentX = transform.position.x;
        currentZ = transform.position.z;

        newX = Random.Range(currentX + 50, currentX - 50);
        newZ = Random.Range(currentZ + 50, currentZ - 50);

        moveTo = new Vector3(newX, transform.position.y, newZ);
        navMeshAgent.destination = moveTo;

       
    }

    void playHitAnimations()
    {
        enemyAnim.SetLayerWeight(1, 1);
        enemyAnim.SetBool("playHitSwipe", true);
    }

    void stopHitAnimation()
    {
        enemyAnim.SetLayerWeight(1, 0);
        enemyAnim.SetBool("playHitSwipe", false);
    }

    void enableRightHandCollider()
    {
        rightHandCollider.enabled = true;
    }

    void disableRightHandCollider()
    {
        rightHandCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("player sound detection");
        playersPosition = playerObject.transform.position;
        playerDetected = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerDetected = false;
        audioForDetection = false;
    }
}
