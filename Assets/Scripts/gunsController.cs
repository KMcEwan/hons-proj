using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gunsController : MonoBehaviour
{

    [SerializeField]
    private playerController playerScript;
    [SerializeField]
    private enemyHealth enemyScript;

    //Gun objects
    [SerializeField]
    GameObject pistolObject;
    [SerializeField]
    GameObject SMGObject;


    //Player & gun properties
    internal bool hasPistol;
    internal bool hasSMG;
    internal bool pistolActive;
    internal bool SMGActive;
    int pistolBulletCount = 0;
    int SMGBulletCount = 0;
    int pistolMagSize = 8;
    int SMGMagSize = 30;

    int PistolBulletsLeftInMag;
    int SMGBulletsLeftInMag;

    //player bullet fire
    float pistolCooldown = 0.5f;
    float pistolFireNext = 0;


    float SMGCooldown = 0.2f;
    float SMGFireNext = 0;


    // player ammo spawn
    int pistolAmmoSpawnCount;
    int SMGAmmoSpawnCount;

    //Gun UI
    [SerializeField]
    GameObject pistolImage;
    [SerializeField]
    GameObject SMGImage;
    [SerializeField]
    TextMeshProUGUI pistolAmmoCountTMP;
    [SerializeField]
    TextMeshProUGUI SMGAmmoCountTMP;

    //Animations

    [SerializeField] private Animator animationController;

    // Gun for animations
    [SerializeField] private GameObject noGunTorch;
    [SerializeField] private GameObject uziGunTorch;
    [SerializeField] private GameObject pistolGunTorch;

    // bool for cursor check
    internal bool hasWeapon = false;


    // audio for guns
    [SerializeField] AudioClip pistolFire;
    [SerializeField] AudioClip smgFire;
    [SerializeField] AudioClip pistolReload;
    [SerializeField] AudioClip smgReload;
    [SerializeField] AudioClip emptyClip;
    [SerializeField] AudioClip drawWeapon;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Debug.Log("key 1 pressed");
            playerWeaponSelection(1);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            Debug.Log("key 2 pressed");
            playerWeaponSelection(2);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            reloadGuns();
        }
        
        if (pistolActive)
        {
            if(Input.GetMouseButtonDown(0))
            {
                firePistol();
            }
            
        }
        else if (SMGActive)
        {
            if(Input.GetMouseButton(0))
            {
                fireSMG();
            }
           
        }

       
    }

    void playerWeaponSelection(int gunNum)
    {
        if (gunNum == 1)
        {
            if (hasPistol)
            {
                if (!pistolActive)
                {
                    audioSource.PlayOneShot(drawWeapon);
                }
                noGunTorch.SetActive(false);
                uziGunTorch.SetActive(false);
                pistolGunTorch.SetActive(true);
                animationController.SetLayerWeight(2, 0);
                animationController.SetBool("hasSMG", false);
                animationController.SetBool("hasSMG", false);
                pistolActive = true;
                SMGActive = false;
                pistolObject.SetActive(true);
                SMGObject.SetActive(false);
                animationController.SetLayerWeight(0, 0);                   // standard movement layer
                animationController.SetLayerWeight(1, 1);                   // arms with guns
                animationController.SetBool("hasPistol", true);
                animationController.SetBool("hasSMG", false);
                pistolImage.GetComponent<Image>().color = Color.red;
                SMGImage.GetComponent<Image>().color = Color.white;                

            }

        }
        else if (gunNum == 2)
        {
            if (hasSMG)
            {
                if (!SMGActive)
                {
                    audioSource.PlayOneShot(drawWeapon);
                }
                noGunTorch.SetActive(false);
                uziGunTorch.SetActive(true);
                pistolGunTorch.SetActive(false);
                animationController.SetLayerWeight(2, 0);
                animationController.SetBool("hasSMG", false);
                animationController.SetBool("hasSMG", false);
                pistolActive = false;
                SMGActive = true;
                pistolObject.SetActive(false);
                SMGObject.SetActive(true);
                animationController.SetLayerWeight(0, 0);
                animationController.SetLayerWeight(1, 1);
                animationController.SetBool("hasSMG", true);
                animationController.SetBool("hasPistol", false);
                pistolImage.GetComponent<Image>().color = Color.white;
                SMGImage.GetComponent<Image>().color = Color.red;
                
            }

        }
    }

    void firePistol()
    {
        if (Time.time > pistolFireNext)
        {
            if (PistolBulletsLeftInMag > 0)
            {
                Debug.Log("pistol fire");
                pistolFireNext = Time.time + pistolCooldown;
                if (playerScript.lastObjectHit.tag.ToString() == "enemy")
                {

                    GameObject objectHit = playerScript.lastObjectHit.gameObject;
                    enemyScript = objectHit.GetComponent<enemyHealth>();
                    enemyScript.enemyMaxHealth -= 10;
                    Debug.Log("enemy health : " + enemyScript.enemyMaxHealth);
                }
                PistolBulletsLeftInMag--;
                audioSource.PlayOneShot(pistolFire);
                if (pistolBulletCount > 0)
                {
                    pistolBulletCount--;
                }
                Debug.Log("current pistol : " + PistolBulletsLeftInMag);
                pistolAmmoCountTMP.text = PistolBulletsLeftInMag.ToString() + "/" + pistolBulletCount;
            }
            else
            {
                audioSource.PlayOneShot(emptyClip);
            }
           
        }

    }

    void fireSMG()
    {
        if (Time.time > SMGFireNext)
        {
            if(SMGBulletsLeftInMag > 0)
            {
                SMGFireNext = Time.deltaTime + SMGCooldown;
                if(playerScript.lastObjectHit.tag.ToString() == "enemy")
                {
                    GameObject objectHit = playerScript.lastObjectHit.gameObject;
                    enemyScript = objectHit.GetComponent<enemyHealth>();
                    enemyScript.enemyMaxHealth -= 50;
                    Debug.Log("enemy health : " + enemyScript.enemyMaxHealth);
                }

                SMGBulletsLeftInMag--;
                audioSource.PlayOneShot(smgFire);
                if (SMGBulletCount > 0)
                {
                    SMGBulletCount--;
                }
                SMGAmmoCountTMP.text = SMGBulletsLeftInMag.ToString() + "/" + SMGBulletCount;
            }
            else
            {
                audioSource.PlayOneShot(emptyClip);
            }


            Debug.Log("SMG fire");
            SMGFireNext = Time.time + SMGCooldown;
        }

    }

    void reloadGuns()
    {
        if (pistolActive)
        {
            pistolBulletCount += PistolBulletsLeftInMag;
            if (pistolBulletCount >= pistolMagSize)
            {
                PistolBulletsLeftInMag = pistolMagSize;
                pistolBulletCount -= pistolMagSize;
            }
            else
            {
                PistolBulletsLeftInMag = pistolBulletCount;
                pistolBulletCount -= pistolBulletCount;
            }
            audioSource.PlayOneShot(pistolReload);
            pistolAmmoCountTMP.text = PistolBulletsLeftInMag.ToString() + "/" + pistolBulletCount;
        }
        else if (SMGActive)
        { 
            SMGBulletCount += SMGBulletsLeftInMag;
            if(SMGBulletCount >= SMGMagSize)
            {
                SMGBulletsLeftInMag = SMGMagSize;
                SMGBulletCount -= SMGMagSize;
            }
            else
            {
                SMGBulletsLeftInMag = SMGBulletCount;
                SMGBulletCount -= SMGBulletCount;
            }
            audioSource.PlayOneShot(smgReload);
            SMGAmmoCountTMP.text = SMGBulletsLeftInMag.ToString() + "/" + SMGBulletCount;
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(other.gameObject.name);
    //}
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        //Debug.Log("parent");

        // foreach(ContactPoint contact in other.con)

        if (other.gameObject.tag == "gunOne")
        {
            Debug.Log("gun one picked up");
            hasPistol = true;
            pistolImage.SetActive(true);
            pistolAmmoCountTMP.gameObject.SetActive(true);
            pistolAmmoCountTMP.text = PistolBulletsLeftInMag.ToString() + "/" + pistolBulletCount;
            Destroy(other.gameObject);


            if(hasWeapon == false)
            {
                hasWeapon = true;
            }
        }
        if (other.gameObject.tag == "gunTwo")
        {
            Debug.Log("gun one picked up");
            hasSMG = true;
            SMGImage.SetActive(true);
            SMGAmmoCountTMP.gameObject.SetActive(true);
            SMGAmmoCountTMP.text = SMGBulletsLeftInMag.ToString() + "/" + SMGBulletCount;
            Destroy(other.gameObject);
            if (hasWeapon == false)
            {
                hasWeapon = true;
            }
        }

        if (other.gameObject.tag == "pistolAmmo")
        {
            pistolAmmoSpawnCount = Random.Range(1, 9);
            Debug.Log("pistol ammo collicion");
            pistolBulletCount += pistolAmmoSpawnCount;
            pistolAmmoCountTMP.text = PistolBulletsLeftInMag.ToString() + "/" + pistolBulletCount;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "SMGAmmo")
        {
            SMGAmmoSpawnCount = Random.Range(6, 30);
            Debug.Log("smg ammo collicion");
            SMGBulletCount += SMGAmmoSpawnCount;
            SMGAmmoCountTMP.text = SMGBulletsLeftInMag.ToString() + "/" + SMGBulletCount;
            Destroy(other.gameObject);
        }
    }

    private void OnMouseDown()
    {
        Debug.Log(gameObject.name);
    }
}
