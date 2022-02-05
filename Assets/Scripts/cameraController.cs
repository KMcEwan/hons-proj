using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;


public class cameraController : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;
    public GameObject hitObject;
    GameObject minDistanceObject;

    // Change transparancy
    private Material objectHitMaterial;
    Color oldColour;
    Color newColour;
    List<GameObject> transparentObjects = new List<GameObject>();
    List<GameObject> hitObjects = new List<GameObject>();

    Ray cameraToPlayerRay;

    void Start()
    {
       
    }


    void Update()
    {
        cameraToPlayerRay = new Ray(camera.transform.position, (player.transform.position - camera.transform.position).normalized);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(cameraToPlayerRay, 1000);
        Debug.DrawRay(camera.transform.position, (player.transform.position - camera.transform.position));
        float minDistance = 100f;
        hitObjects.Clear();


        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].distance < minDistance)
            {
                minDistance = hits[i].distance;
                minDistanceObject = hits[i].transform.gameObject;
            }
            hitObjects.Add(hits[i].transform.gameObject);
        }

        if (minDistanceObject.gameObject.tag == "player")
        {
            Debug.Log("objects  =  player");
        }
        else
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.gameObject.tag == "stationary")
                {
                    if (!transparentObjects.Contains(hits[i].transform.gameObject))
                    {
                        transparentObjects.Add(hits[i].transform.gameObject);
                    }

                    objectHitMaterial = hits[i].transform.gameObject.GetComponent<Renderer>().material;
                    oldColour = objectHitMaterial.color;
                    newColour = new Color(oldColour.r, oldColour.g, oldColour.b, 0.5f);
                    objectHitMaterial.SetColor("_Color", newColour);
                }


            }
        }


        for (int i = 0; i < transparentObjects.Count; i++)
        {
            if (hitObjects.Contains(transparentObjects[i]))
            {
                Debug.Log("OBJECTS IN LIST");
                return;               
            }
            else
            {
                Material transparentMat = transparentObjects[i].GetComponent<Renderer>().material;
                Color transparentMatOldColour = transparentMat.color;
                Color transparentMatnewColour = new Color(transparentMatOldColour.r, transparentMatOldColour.g, transparentMatOldColour.b, 1f);
                transparentMat.SetColor("_Color", transparentMatnewColour);
                transparentObjects.Remove(transparentObjects[i]);
                Debug.Log("Not in list");

            }

        }
    }
    
}
