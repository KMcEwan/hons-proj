using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class playerHealth : MonoBehaviour
{
    // health and medpacks
    [SerializeField] private int health = 100;
    [SerializeField] private int medpackCount = 0;

    // Canvas elements
    [SerializeField] private GameObject medpackUI;
    [SerializeField] private TextMeshProUGUI medpackCountUI;
    [SerializeField] private TextMeshProUGUI healthCountUI;

    // text animation
    [SerializeField] private Animator healthAnimationController;
    
    [SerializeField] float hitCooldown = 0.0f;
    [SerializeField] float hitNext;


    // audio
    [SerializeField] AudioClip playerDamaged;
    AudioSource audioSource;

    //player controller script for animation
    [SerializeField] private playerController playerScript;


    // medpack pick up
    [SerializeField] AudioClip medpackPickUp;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerHealth();
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            addHealth();
        }

        //for debuggin purposes only
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("minus health");
            health -= 10;
            healthCountUI.text = health.ToString();

        }
    }

    private void addHealth()
    {
        Debug.Log("add health");
        Debug.Log(medpackCount);

        if (medpackCount > 0 && health < 100)
        {
            Debug.Log("in if");
            medpackCount--;
            health += 10;
        }
        if(health > 50)
        {
            healthCountUI.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        healthCountUI.text = health.ToString();
        medpackCountUI.text = medpackCount.ToString();
    }
    private void checkPlayerHealth()
    {
        Debug.Log("players health");
        if (health <= 0)
        {
            health = 0;
            healthCountUI.text = health.ToString();
            playerScript.playerDead();

        }


        if (health <= 20)
        {
            healthAnimationController.SetBool("healthLessThan", true);
        }
        else
        {
            healthAnimationController.SetBool("healthLessThan", false);
        }

        if(health <= 50)
        {
            healthCountUI.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        if(health > 100)
        {
            health = 100;
            healthCountUI.text = health.ToString();

        }

    }

    void playerDamage()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "medkit")
        {
            Debug.Log("medkit hit");
            if(medpackUI.activeInHierarchy == false)
            {
                medpackUI.SetActive(true);
                medpackCountUI.gameObject.SetActive(true);                
            }
            medpackCount++;
            medpackCountUI.text = medpackCount.ToString();
            healthCountUI.text = health.ToString();
            Destroy(other.gameObject);
            audioSource.PlayOneShot(medpackPickUp);
        }
        else if(other.tag == "enemyHands")
        {
            //Debug.Log("enemy hit");
            if (Time.time > hitNext)
            {
                hitNext = Time.time + hitCooldown;
      
                Debug.Log(other.name);
                health -= 10;
                healthCountUI.text = health.ToString();
                if(!audioSource.isPlaying && !playerScript.isDead)
                {
                    audioSource.PlayOneShot(playerDamaged);
                }
            }

        }


    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit");
        if(other.tag == "enemyHands")
        {
            Debug.Log("exiting enemy hands");
        }
    }
}
