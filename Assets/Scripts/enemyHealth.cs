using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    // Start is called before the first frame update

    public int enemyMaxHealth = 100;

    Animator enemyAnimation;
    void Start()
    {
        enemyAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyMaxHealth <= 0)
        {
            enemyAnimation.SetBool("isDead", true);
        }
    }

    public void destroyEnemy()
    {
        Debug.Log("DESTROY");
        Destroy(this.gameObject);
    }
}
