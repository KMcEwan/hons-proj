using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{
    // enemy movement
    [SerializeField] private int needToMove;
    [SerializeField] private Vector3 moveTo;
    private NavMeshAgent navMeshAgent;
    private NavMeshPath navigationPath ;
    [SerializeField] bool pathPossible;

    LineRenderer line;

   // [SerializeField] private Transform currentPosition;
    [SerializeField] private Transform moveToPos;
    [SerializeField] private Vector3 initalPosOfMoveTo = new Vector3(0, 0, 0);
    [SerializeField] Vector3 lastPos;
    [SerializeField] Vector3 currentPos;
    [SerializeField] Vector3 playersPosition;
    private bool atMoveTo = false;
    Vector3 moveToDec = new Vector3(0, 0, 0);

    // player object to assign location when detected
    [SerializeField] GameObject playerObject;


    // enemy movement vars
    [SerializeField] private float speed = 2;
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
    [SerializeField] AudioClip[] screechDetection;
    AudioSource audioSource;
    bool audioForDetection = false;


    // to determine whether to move or not
    [SerializeField] private float movementCooldown = 0f;
    private float moveNext = 0;

    // test position bool
    [SerializeField] bool CurrentEqualToLast;


    // to check if enemy is dead
    [SerializeField] enemyHealth enemyHealthScipt;

    void Start()
    {
        atPostition = true;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        audioSource = GetComponent<AudioSource>();
        movementCooldown = Random.Range(10f, 50f);
        playerObject = GameObject.FindWithTag("player");
        navigationPath = new NavMeshPath();
    }

    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (enemyHealthScipt.isDead)
        {
            moveTo = transform.position;
            navMeshAgent.isStopped = true;
        }
        else
        if (!this.enemyHealthScipt.isDead)
        {
            if (moveTo != initalPosOfMoveTo)
            {

                moveToDec.x = Mathf.Round(moveTo.x * 100.0f) * 0.1f;
                moveToDec.z = Mathf.Round(moveTo.z * 100.0f) * 0.1f;

                currentPos.x = Mathf.Round(transform.position.x * 100.0f) * 0.1f;
                currentPos.z = Mathf.Round(transform.position.z * 100.0f) * 0.1f;
            }

            int timeToScreech = Random.Range(0, 1000);
            if (timeToScreech > 998)
            {

                audioSource.PlayOneShot(screechDetection[Random.Range(0, screechDetection.Length)]);
            }


            if (currentPos == moveToDec)
            {
                atMoveTo = true;
            }

            if (playerDetected)
            {
                moveTo = new Vector3(playerObject.transform.position.x, transform.position.y, playerObject.transform.position.z);
                moveToPlayer();
            }
            else
            if (!atMoveTo && moveTo != initalPosOfMoveTo)
            {
                disableRightHandCollider();
                disableBothHands();
                navMeshAgent.destination = moveTo;
            }
            else if (!playerDetected && Time.time > moveNext)
            {
                disableRightHandCollider();
                disableBothHands();
                moveNext = Time.time + movementCooldown;
                moveToRandomPosition();
            }
            else if (navMeshAgent.velocity.sqrMagnitude! > 1f)
            {
                enemyAnim.SetBool("playWalking", true);
                disableRightHandCollider();
                disableBothHands();
            }
            else
            {
                enemyAnim.SetBool("playWalking", false);
                disableRightHandCollider();
                disableBothHands();
            }
        }
    }


    void moveToPlayer()
    {
        enemyAnim.SetBool("playWalking", true);
        transform.LookAt(moveTo);
        navMeshAgent.destination = moveTo - capsuleRadius;

        if(!audioForDetection && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(screechDetection[Random.Range(0, screechDetection.Length)]);
            audioForDetection = true;
        }
        if (Vector3.Distance(transform.position, playerObject.transform.position) < 1)
        {
            enemyAnim.SetBool("playWalking", false);
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
        currentX = transform.position.x;
        currentZ = transform.position.z;

        newX = Random.Range(currentX + 20, currentX - 20);
        newZ = Random.Range(currentZ + 20, currentZ - 20);
        moveTo = new Vector3(newX, transform.position.y, newZ);
        if (navMeshAgent.CalculatePath(moveTo, navigationPath) && navigationPath.status == NavMeshPathStatus.PathComplete)
        {
            navMeshAgent.SetPath(navigationPath);
            pathPossible = true;
        }
        else
        {
            pathPossible = false;
            enemyAnim.SetBool("playWalking", false); 
        }       
    }

    void checkDistanceToPlayer()
    {
        if (Vector3.Distance(playerObject.transform.position, transform.position) > 150)
        {
            transform.gameObject.SetActive(false);
        }
        else
        {
            if (transform.gameObject.activeInHierarchy == false)
            {
                transform.gameObject.SetActive(true);
            }

        }
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

    public void disableRightHandCollider()
    {
        rightHandCollider.enabled = false;
    }

    void enableBothHands()
    {
        rightHandCollider.enabled = true;
        lefttHandCollider.enabled = true;
    }

    void disableBothHands()
    {
        rightHandCollider.enabled = false;
        lefttHandCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "playerAudioCollider")
        {
            playersPosition = playerObject.transform.position;
            playerDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "playerAudioCollider")
        {
            playerDetected = false;
            audioForDetection = false;
        }

    }
}
