using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    float currentHealth;
    public GameObject healthUI;
    public Image healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Potato>())
        {
            currentHealth -= collision.gameObject.GetComponent<Potato>().damageToGive;

            if (currentHealth <= 0)
            {
                HandleDeath();
            }

            UpdateUI();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        currentHealth -= 1;
        print(currentHealth);

        if (currentHealth <= 0)
        {
            HandleDeath();
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        healthBar.fillAmount = (currentHealth / maxHealth) / 1;
    }

    public void HandleDeath()
    {
        Instantiate(Resources.Load("VegieSplode"), transform.position, transform.rotation);
        Destroy(healthUI);
        Destroy(gameObject);
    }
}
