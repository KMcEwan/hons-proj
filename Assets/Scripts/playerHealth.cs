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


    [SerializeField] float hitCooldown = 1.5f;
    [SerializeField] float hitNext;


    // audio
    [SerializeField] AudioClip playerDamaged;
    AudioSource audioSource;
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
        if(health <= 20)
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

        if(health <= 0)
        {
            health = 0;
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
        }
        else if(other.tag == "enemyHands")
        {
            if(Time.time > hitNext)
            {
                hitNext = Time.time + hitCooldown;
                Debug.Log("enemy hit");
                health -= 10;
                healthCountUI.text = health.ToString();
                if(!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(playerDamaged);
                }
            }

        }


    }
}
