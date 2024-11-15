using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePickup : MonoBehaviour
{
    [SerializeField] private int amount = 1;
    [SerializeField] private GameObject pickupEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (CollectiblesManager.instance != null)
            {
                CollectiblesManager.instance.GetCollectible(amount);

                Destroy(gameObject);
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
                AudioManager.instance.allSFXPlayPitched(9);
            }
        }
    }
}
