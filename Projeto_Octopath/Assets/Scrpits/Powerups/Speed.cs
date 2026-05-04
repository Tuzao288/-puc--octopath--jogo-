using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class Speed : MonoBehaviour
{
    [SerializeField] public float duration;
    private GameObject player;
    private GameObject enemy;
    private GameObject[] speedPU;
    public bool isUsing;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        speedPU = GameObject.FindGameObjectsWithTag("Speed");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    private void Update()
    {
        if(player.GetComponent<PlayerController>().moveSpeed == 40f)
        {
            isUsing = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isUsing == false)
            {
                StartCoroutine(SpeedBoost());
            }
            if (isUsing == true)
            {
                GetComponent<Collider>().enabled = false;
            }
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public IEnumerator SpeedBoost()
    {
        float originalSpeed = player.GetComponent<PlayerController>().moveSpeed;

        isUsing = true;

        player.GetComponent<PlayerController>().moveSpeed *= 2f;

        yield return new WaitForSeconds(duration);

        player.GetComponent<PlayerController>().moveSpeed = originalSpeed;

        isUsing = false;

        Destroy(gameObject);

    }
}
