using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayerScript : MonoBehaviour
{
    [SerializeField] GameObject player;


    private void FixedUpdate()
    {

        transform.position = new Vector3(player.transform.position.x, .34f, player.transform.position.z);
    }


}
