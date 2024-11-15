using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healthPickup;
    [SerializeField] private GameObject pickEffect;
    [SerializeField] private bool fullHealth;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (PLayerHealthController.instance.currentHealth != PLayerHealthController.instance.maxHealth)
            {
                if (fullHealth == true)
                {
                    PLayerHealthController.instance.AddHealth(PLayerHealthController.instance.maxHealth);
                }
                else
                {
                    PLayerHealthController.instance.AddHealth(healthPickup);
                }

                Destroy(gameObject);
                Instantiate(pickEffect, transform.position, transform.rotation);
                AudioManager.instance.allSFXPlay(10);
            }
        }
    }
}
