using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBirdController : MonoBehaviour
{
    [SerializeField] private bool isAttack;
    [SerializeField] private GameObject blueBrid, thePlayer, patrolPoint;
    [SerializeField] private float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        patrolPoint.transform.SetParent(null);
        isAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack == true)
        {
            Debug.Log("Blue Bird is coming");
            blueBrid.transform.position = Vector3.MoveTowards(blueBrid.transform.position, thePlayer.transform.position, speed * Time.deltaTime);
            if (blueBrid.transform.position.x > patrolPoint.transform.position.x)
            {
                blueBrid.transform.localScale = new Vector3(-1f, 1f, 1f);
            }else if (blueBrid.transform.position.x < patrolPoint.transform.position.x)
            {
                blueBrid.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        if (isAttack == false)
        {
            Debug.Log("Blue Bird is leaving");
            blueBrid.transform.position = Vector3.MoveTowards(blueBrid.transform.position, patrolPoint.transform.position, speed * Time.deltaTime);
            if (blueBrid.transform.position.x > patrolPoint.transform.position.x)
            {
                blueBrid.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (blueBrid.transform.position.x < patrolPoint.transform.position.x)
            {
                blueBrid.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isAttack = true;
            Debug.Log("Player in");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isAttack = false;
            Debug.Log("Player out");
        }
    }
}
