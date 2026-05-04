using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cure : MonoBehaviour
{
    public float regeneration;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(player.GetComponent<Health>().currentHealth < player.GetComponent<Health>().maxHealth)
        {
            if (collision.CompareTag("Player"))
            {
                player.GetComponent<Health>().currentHealth += regeneration;
                Destroy(gameObject);

                if (player.GetComponent<Health>().currentHealth + regeneration > player.GetComponent<Health>().maxHealth)
                {
                    player.GetComponent<Health>().currentHealth = player.GetComponent<Health>().maxHealth;
                }
            }
        }
           
    }


}
