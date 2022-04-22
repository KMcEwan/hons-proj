using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deactivateColliderOfEnemyHand : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] SphereCollider rightHandCollider;

    void Start()
    {
        //rightHandCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            Debug.Log("player hit");
            rightHandCollider.enabled = false;
        }
    }


}
