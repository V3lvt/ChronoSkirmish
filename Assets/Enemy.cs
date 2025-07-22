using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour
{

    public float health = 100f;
    public CapsuleCollider collider;

    public TMPro.TMP_Text healthText;


    private void Awake()
    {
        collider = GetComponent<CapsuleCollider>();
        healthText = GetComponentInChildren<TMP_Text>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 100f;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            
            Debug.Log("Enemy Killed)");
            Destroy(this.gameObject);
        }

        healthText.text = health.ToString();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
        {
            Debug.Log("Enemy Hit");
            health = health - 20f;
        }
           

    }
}
