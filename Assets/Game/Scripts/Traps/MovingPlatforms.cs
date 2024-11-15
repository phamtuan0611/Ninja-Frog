using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float speed, waitTime, timeAtPoint;
    [SerializeField] private int currentPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform patrolPoint in patrolPoints)
        {
            patrolPoint.SetParent(null);
        }
        transform.position = patrolPoints[currentPoint].position;
        currentPoint++;
        waitTime = timeAtPoint;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, speed * Time.deltaTime);
        if (transform.position == patrolPoints[currentPoint].position)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                currentPoint++;
                waitTime = timeAtPoint;
            }
        }
        if (currentPoint >= patrolPoints.Length)
        {
            currentPoint = 0;
        }
    }
}
