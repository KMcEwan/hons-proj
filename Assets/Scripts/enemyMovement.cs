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
    private int speed = 2;
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

    void Start()
    {
        atPostition = true;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
       // Debug.Log(atPostition);
    }

    // Update is called once per frame
    void Update()
    {
        //needToMove = (Random.Range(0, 2) == 0 );
        //Debug.Log(currentPosition.name);
        currentPos = transform.position;                                                          // works using this
        currentPos = (currentPos * 100);
        currentPos.x = Mathf.Floor(currentPos.x);
        currentPos.z = Mathf.Floor(currentPos.z);
        currentPos = new Vector3(currentPos.x, currentPos.y, currentPos.z);
        currentPos = (currentPos / 100);
        Debug.Log("2 last pos : " + lastPos + "current pos : " + currentPos);
        //currentPos = currentPosition.transform.position;
        //Debug.Log("current position : " + currentPos);
        // Debug.Log(new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, playerObject.transform.position.z));
        if (playerDetected)
        {
            moveTo = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, playerObject.transform.position.z);
            moveToPlayer();
        }
        if (lastPos == currentPos)
        {
            atPostition = true;
            Debug.Log(atPostition);
            Debug.Log("last pos equal to current");
            Debug.Log("currents : " + currentPos + "lastPos : " + lastPos);
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


        //lastPos = (lastPos * 100.0f)/* * 0.1f*/;
        //currentPos = (currentPos * 100.0f) /** 0.1f*/;
        //Debug.Log("last pos 1 " + lastPos);
        //Debug.Log("current pos 1 " + currentPos);
        // System.Math.Round(lastPos.x, 2);

        //Debug.Log("1 last pos : " + lastPos + "current pos : " + currentPos);
        //lastPos = (lastPos * 100);               // changed from 100
        //lastPos.x = Mathf.Floor(lastPos.x);
        //lastPos.z = Mathf.Floor(lastPos.z);
        //lastPos = new Vector3(lastPos.x, lastPos.y, lastPos.z);
        //lastPos = (lastPos / 100);







        //lastPos = new Vector3Int((int)lastPos.x, (int)lastPos.y, (int)lastPos.z);
        //currentPos = new Vector3Int((int)currentPos.x, (int)currentPos.y, (int)currentPos.z);
        //Debug.Log("last pos 2 " + lastPos);
        //Debug.Log("current pos 2 " + currentPos);



    }


    void moveToPlayer()
    {
        enemyAnim.SetBool("playWalking", true);
        Vector3 targetPostition = new Vector3(playerObject.transform.position.x, transform.position.y, playerObject.transform.position.z);
        transform.LookAt(targetPostition);
        navMeshAgent.destination = moveTo - capsuleRadius;

        Debug.Log(rightHandCollider.enabled);


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
        //Debug.Log(moveTo);
        navMeshAgent.destination = moveTo;

       
    }

    void playHitAnimations()
    {
        rightHandCollider.enabled = true;
        enemyAnim.SetLayerWeight(1, 1);
        enemyAnim.SetBool("playHit1", true);
        //Debug.Log("close to player");
    }

    void stopHitAnimation()
    {
        rightHandCollider.enabled = false;
        enemyAnim.SetLayerWeight(1, 0);
        enemyAnim.SetBool("playHit1", false);
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
    }
}
