using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distributeKey : MonoBehaviour
{
    [SerializeField] GameObject[] deadArmy;

    private searchDead assignKey;


    void Start()
    {
       assignKey = deadArmy[1].GetComponent<searchDead>();


        int deadArmyNumber = Random.Range(0, deadArmy.Length);

        deadArmy[deadArmyNumber].GetComponent<searchDead>().hasKey = true;

    }

}
