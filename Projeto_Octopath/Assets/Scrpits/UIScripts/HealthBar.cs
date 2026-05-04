using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthbarSprite;
    //private GameObject playerHealth;
    //private Camera cam;
    private GameObject player;
    private Vector3 offset;
    [SerializeField] Vector3 offsetValues;

    private void Awake()
    {
        //cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
        healthbarSprite = Image.FindFirstObjectByType<Image>();
    }
    private void Start()
    {
        offset = new Vector3(0, offsetValues.y, offsetValues.z);
    }

    private void Update()
    {
        if(player != null)
        {
            UpdateHealthBar(player.GetComponent<Health>().maxHealth, player.GetComponent<Health>().currentHealth);
        }
        else
        {
            Destroy(gameObject);
        }
        //transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.transform.position + offset;
        }
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthbarSprite.fillAmount = currentHealth / maxHealth;
    }

}
