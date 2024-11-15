using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    //private PLayerHealthController healthPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        //healthPlayer = FindFirstObjectByType<PLayerHealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //other.gameObject.SetActive(false);
            //FindFirstObjectByType<PLayerHealthController>().DamagePlayer();
            //healthPlayer.DamagePlayer();

            PLayerHealthController.instance.DamagePLayer();
        }
    }
}
