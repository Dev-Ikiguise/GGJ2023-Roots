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
    public Pesticide.PesticideColor pesticideWeakness;

    private void Start()
    {
        currentHealth = maxHealth;
        healthUI.transform.localScale = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Potato>())
        {
            healthUI.transform.localScale = Vector3.one;

            currentHealth -= collision.gameObject.GetComponent<Potato>().damageToGive;

            if (currentHealth <= 0)
            {
                HandleDeath();
            }

            UpdateUI();

            collision.gameObject.GetComponent<Potato>().PlayImpactSound();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        float damageBonus = 0;

        if (other.GetComponent<Pesticide>() && other.GetComponent<Pesticide>().pesticideColor == pesticideWeakness)
        {
            damageBonus = 10;
        }

        print(other.name);

        healthUI.transform.localScale = Vector3.one;

        currentHealth -= 1 + damageBonus;
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
