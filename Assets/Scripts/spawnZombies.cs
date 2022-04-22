using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnZombies : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject spawnZoneOne;



    private void OnTriggerEnter(Collider other)
    {
       
        if(other.tag == "zoneOne" || other.tag == "zoneTwo" || other.tag == "zoneThree" || other.tag == "zoneFour" || other.tag == "zoneFive" || other.tag == "zoneSix")
        {

            foreach (Transform child in other.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "zoneOne" || other.tag == "zoneTwo" || other.tag == "zoneThree" || other.tag == "zoneFour" || other.tag == "zoneFive" || other.tag == "zoneSix")
        {
     
            foreach (Transform child in other.transform)
            {
                if (Vector3.Distance(player.transform.position, child.transform.position) < 40)
                {
                    continue;
                }
                else
                {
                    child.gameObject.SetActive(false);
                }

            }
        }
    }


}
