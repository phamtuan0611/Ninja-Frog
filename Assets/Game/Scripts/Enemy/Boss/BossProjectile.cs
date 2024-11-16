using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 8f;
    private Vector3 direction;
    private float lifeTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        //Dung PlayerHealthController vi no co instance, lay duoc vi tri cua Player de hon la PlayerController
        //Dung .normalized de dam bao o khoang cach nao, toc do di chuyen cung nhu nhau, cung nhat quan - Chuan hoa Vector 
        //Tranh truong hop nhu Player o xa thi se di chuyen dan nhanh hon va gan thi cham hon
        if (PLayerHealthController.instance != null)
        {
            direction = (PLayerHealthController.instance.transform.position - transform.position).normalized;
        }

        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * (Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PLayerHealthController.instance.DamagePLayer();
            Destroy(gameObject);
        }
    }
}
