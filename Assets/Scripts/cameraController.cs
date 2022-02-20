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
    public enum SurfaceType
    {
        Opaque,
        Transparent
    }

    public enum BlendMode
    {
        Alpha,
        Premultiply,
        Additive,
        Multiply
    }

    public Material transparentMaterial;



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
           // Debug.Log("objects  =  player");
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
                    // targetObject.material.SetFloat("_Mode", 3f);


                     objectHitMaterial = hits[i].transform.gameObject.GetComponent<Renderer>().material;
                    //MeshRenderer renderer = hits[i].transform.gameObject.GetComponent<MeshRenderer>();

                    //Material originalMaterial = renderer.sharedMaterial; //sharedMaterial

                    //Material transMaterial = new Material(transparentMaterial);
                    //transMaterial.CopyPropertiesFromMaterial(originalMaterial);
                    //oldColour = transMaterial.color;
                    //newColour = new Color(oldColour.r, oldColour.g, oldColour.b, 0.5f);
                    //transMaterial.SetColor("_Color", newColour);

                    //renderer.sharedMaterial = transMaterial;

               
                    objectHitMaterial.SetFloat("_Surface", 1.0f);
                    objectHitMaterial.SetFloat("_Blend", 0f);
                    //objectHitMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    //objectHitMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    //objectHitMaterial.EnableKeyword("_ALPHABLEND_ON");
                    //objectHitMaterial.SetOverrideTag("RenderType", "Transparent");
                    //oldColour = objectHitMaterial.color;
                    //newColour = new Color(oldColour.r, oldColour.g, oldColour.b, 0.1f);
                    //objectHitMaterial.SetColor("_BaseColor", newColour);
                    objectHitMaterial.SetOverrideTag("RenderType", "Transparent");
                    objectHitMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    objectHitMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    //objectHitMaterial.SetInt("_ZWrite", 0);
                    //objectHitMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    objectHitMaterial.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    objectHitMaterial.SetShaderPassEnabled("ShadowCaster", false);
                    oldColour = objectHitMaterial.color;
                    newColour = new Color(oldColour.r, oldColour.g, oldColour.b, 0.5f);
                    objectHitMaterial.SetColor("_BaseColor", newColour);


                    // renderer.sharedMaterial.SetColor("_Colour", new Color(1.0f,1.0f,1.0f,0.5f));
                    //Color oldColour2 = renderer.sharedMaterial.color;
                    //Color newColour2 = new Color(oldColour2.r, oldColour2.g, oldColour2.b, 0.5f);
                    //objectHitMaterial.SetColor("_Color", newColour2);

                    //objectHitMaterial.SetFloat("_Mode", 3);
                    //objectHitMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    //objectHitMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    //objectHitMaterial.EnableKeyword("_ALPHABLEND_ON");
                    //objectHitMaterial.renderQueue = 3000;


                    //objectHitMaterial.SetFloat("_Surface", (float)SurfaceType.Transparent);
                    //objectHitMaterial.SetFloat("_Blend", (float)BlendMode.Alpha);




                    //oldColour = objectHitMaterial.color;
                    //newColour = new Color(oldColour.r, oldColour.g, oldColour.b, 0.5f);
                    //objectHitMaterial.SetColor("_Color", newColour);
                }


            }
        }


        for (int i = 0; i < transparentObjects.Count; i++)
        {
            if (hitObjects.Contains(transparentObjects[i]))
            {
                Debug.Log("OBJECTS IN LIST");
                //return;
                continue;
            }
            else
            {
                Material transparentMat = transparentObjects[i].GetComponent<Renderer>().material;




                Color transparentMatOldColour = transparentMat.color;
                Color transparentMatnewColour = new Color(transparentMatOldColour.r, transparentMatOldColour.g, transparentMatOldColour.b, 1f);
                transparentMat.SetColor("_Color", transparentMatnewColour);
                transparentObjects.Remove(transparentObjects[i]);
                Debug.Log("Not in list");

                transparentMat.SetFloat("_Surface", 0);


                transparentMat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                transparentMat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                transparentMat.SetInt("_ZWrite", 1);
                transparentMat.DisableKeyword("_ALPHATEST_ON");
                transparentMat.DisableKeyword("_ALPHABLEND_ON");
                transparentMat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                transparentMat.renderQueue = -1;


            }

        }
    }
    
}
