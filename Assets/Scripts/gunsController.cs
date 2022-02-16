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
    bool hasPistol;
    bool hasSMG;
    bool pistolActive;
    bool SMGActive;
    int pistolBulletCount = 0;
    int SMGBulletCount = 0;
    int pistolMagSize = 8;
    int SMGMagSize = 30;

    int PistolBulletsLeftInMag;
    int SMGBulletsLeftInMag;

    //player bullet fire
    float pistolCooldown = 0.5f;
    float pistolFireNext = 0;


    float SMGCooldown = 0.5f;
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

    // Update is called once per frame

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

        if(Input.GetKey(KeyCode.R))
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
                pistolActive = true;
                SMGActive = false;
                pistolObject.SetActive(true);
                SMGObject.SetActive(false);
                Debug.Log(pistolActive.ToString());
                Debug.Log(SMGActive.ToString());
                animationController.SetLayerWeight(0, 0);
                animationController.SetLayerWeight(1, 1);
            }

        }
        else if (gunNum == 2)
        {
            if (hasSMG)
            {
                pistolActive = false;
                SMGActive = true;
                pistolObject.SetActive(false);
                SMGObject.SetActive(true);
                Debug.Log(pistolActive.ToString());
                Debug.Log(SMGActive.ToString());
                animationController.SetLayerWeight(0, 0);
                animationController.SetLayerWeight(1, 1);
            }

        }
    }

    void firePistol()
    {
        if (Time.time > pistolFireNext)
        {
            if(PistolBulletsLeftInMag > 0)
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
                if(pistolBulletCount > 0)
                {
                    pistolBulletCount--;
                }
                Debug.Log("current pistol : " + PistolBulletsLeftInMag);
                pistolAmmoCountTMP.text = PistolBulletsLeftInMag.ToString() + "/" + pistolBulletCount;
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
                if(SMGBulletCount > 0)
                {
                    SMGBulletCount--;
                }
                SMGAmmoCountTMP.text = SMGBulletsLeftInMag.ToString() + "/" + SMGBulletCount;
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
        }
        if (other.gameObject.tag == "gunTwo")
        {
            Debug.Log("gun one picked up");
            hasSMG = true;
            SMGImage.SetActive(true);
            SMGAmmoCountTMP.gameObject.SetActive(true);
            SMGAmmoCountTMP.text = SMGBulletsLeftInMag.ToString() + "/" + SMGBulletCount;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "pistolAmmo")
        {
            pistolAmmoSpawnCount = Random.Range(1, 9);
            Debug.Log("pistol ammo collicion");
            pistolBulletCount += pistolAmmoSpawnCount;
            pistolAmmoCountTMP.text = PistolBulletsLeftInMag.ToString() + "/" + pistolBulletCount;
        }

        if (other.gameObject.tag == "SMGAmmo")
        {
            SMGAmmoSpawnCount = Random.Range(6, 30);
            Debug.Log("smg ammo collicion");
            SMGBulletCount += SMGAmmoSpawnCount;
            SMGAmmoCountTMP.text = SMGBulletsLeftInMag.ToString() + "/" + SMGBulletCount;
        }
    }
}
