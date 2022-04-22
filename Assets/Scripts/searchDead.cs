using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class searchDead : MonoBehaviour
{


   [SerializeField] private playerController playerScript;


    // Search UI
    [SerializeField] Image searchUI;

    // turn key on UI
    [SerializeField] Image keyUI;

    //do they have the key
    public bool hasKey = false;

    // collision with player
    public bool playerInRange;


    // layer mask for dead objects
    int layer = 13;
    [SerializeField] int deadLayerMask;

    // mouse position
    private Vector3 mousePosition;
    void Start()
    {
        deadLayerMask = 1 << layer;
    }

    //to pass last object hit to player script for cursor change
    public GameObject objectHit;
    public bool hittingDeadArmy;


    // to play searching audio
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip searchingForKey;
    [SerializeField] AudioClip foundKey;

    //animator to search the dead
    [SerializeField] Animator deadAnim;
    // Update is called once per frame
    void Update()
    {
       
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 20, Color.white);
        RaycastHit hitData;


            if (Physics.Raycast(ray, out hitData, 1000, deadLayerMask))
            {
                mousePosition = Input.mousePosition;
                objectHit = hitData.transform.gameObject;
                if (hitData.transform.gameObject.tag.ToString() == "armyDead")
                {
                    Vector3 deadPos = hitData.transform.position;

                    deadPos.z -= 1f;
                    deadPos.x -= 1f;
                    deadPos.y += 1f;
                    searchUI.gameObject.SetActive(true);
 
                    searchUI.transform.position = deadPos;


                    if (Input.GetMouseButtonDown(1) && playerInRange)
                    {

                        audioSource.PlayOneShot(searchingForKey);
                        deadAnim.SetBool("searchingDead", true);
                    }
                }
                else
                {
                    searchUI.gameObject.SetActive(false);
                }
            }
    }


    public void playSearchingAudio()
    {
        audioSource.PlayOneShot(searchingForKey);
    }

    public void checkForKey()
    {
        Debug.Log("checking for key");
        audioSource.Stop();
        if(hasKey && playerInRange)
        {
            audioSource.PlayOneShot(foundKey);
            keyUI.gameObject.SetActive(true);
            playerScript.updateKeyInformation();
        }
        deadAnim.SetBool("searchingDead", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            playerInRange = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerInRange = false;
    }
}
