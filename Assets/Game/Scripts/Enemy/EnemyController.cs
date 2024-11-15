using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    //[HideInInspector]
    public bool isDefeated;

    public float waitToDestroy;
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDefeated == true)
        {
            waitToDestroy -= Time.deltaTime;
            
            if (waitToDestroy <= 0)
            {
                
                Destroy(gameObject);
                AudioManager.instance.allSFXPlay(5);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isDefeated == false)
            {
                PLayerHealthController.instance.DamagePLayer();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Destroy(gameObject);

            //Tim GameObject dau tien trong Scene chua Component (truong hop nay Component la Scrpit) PlayerController
            FindFirstObjectByType<PlayerController>().Jump();
            
            anim.SetTrigger("defeated");

            isDefeated = true;
            AudioManager.instance.allSFXPlay(6);
        }
    }
}
