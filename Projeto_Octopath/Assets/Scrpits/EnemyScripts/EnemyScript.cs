using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class EnemyScript : MonoBehaviour 
{
    Vector3 dir;
    public float minSpeed;
    public float maxSpeed;
    private GameObject target;
    public float damage;
    public int enemyPoints;
    private GameObject score;

    [Header("Drop System")]
    public List<GameObject> dropItems;
    public float dropChance;
    public int itensQnt;

    private void Awake()
    {
        score = GameObject.FindGameObjectWithTag("Canvas");
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float t = Time.deltaTime;
        
        if(target != null)
        {
            dir = target.transform.position - transform.position;
            transform.position += dir.normalized * t * Random.Range(minSpeed, maxSpeed);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target)
        {
            var health = target.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }

    }
    private void OnDestroy()
    {
        if(score != null)
        {
        score.GetComponent<ScoreScript>().ScoreUp(enemyPoints);

            if (UnityEngine.Random.Range(0f, 100f) <= dropChance)
            {

                DropRandomItem();
                
            }
        }

    }

    void DropRandomItem()
    {
        if (dropItems == null || dropItems.Count == 0) return;

        int index = Random.Range(0, dropItems.Count);
        GameObject item = dropItems[index];

        Instantiate(item, transform.position, Quaternion.identity);
        itensQnt++;
    }

}
