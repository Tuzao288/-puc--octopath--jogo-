using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    private GameObject targetedScript;
    private GameObject player;
    private Renderer newColor;
    private Color deafultColor;
    [SerializeField] private TMP_Text loseText;

    void Awake()
    {
        targetedScript = GameObject.FindGameObjectWithTag("Spawn");
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
    }

    private void Start()
    {
        newColor = GetComponent<Renderer>();
        deafultColor = newColor.material.color;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        StartCoroutine(ChangeColor());
        if (currentHealth <= 0f)
        {
            
            Die();
        }
    }

    void Die()
    {
        var counter = targetedScript.GetComponent<EnemySpawn>();
        counter.CounterSubtractor();
        if (player.GetComponent<Health>().currentHealth <= 0)
        {
            loseText.text = "You Died!";
        }
        Destroy(gameObject);
    }

    IEnumerator ChangeColor()
    {
        newColor.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        newColor.material.color = deafultColor;
    }

}