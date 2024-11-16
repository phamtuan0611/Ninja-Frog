using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleController : MonoBehaviour
{
    private bool bossActive;
    public GameObject blockers;

    public Transform camPoint;
    private CameraController camController;
    public float cameraMoveSpeed;

    public Transform theBoss;
    public float bossGrowSpeed;

    public Transform projectileLauncher;
    public float launcherSpeed = 2f;

    public float laucncherRotateSpeed = 90f;
    private float launcherRotation;

    public GameObject projectileToFire;
    public Transform[] projectilePoints;

    public float waitToStartShooting, timeBetweenShots;
    private float shootStartCounter, shotCounter;
    private int currentShoot;

    public Animator bossAnim;
    private bool isWeak;

    public Transform[] bossMovePoints;
    private int currentMovePoint;
    public float bossMoveSpeed;

    private int currentPhase;

    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        camController = FindFirstObjectByType<CameraController>();

        shootStartCounter = waitToStartShooting;

        blockers.transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        if (bossActive == true)
        {
            camController.transform.position = Vector3.MoveTowards(camController.transform.position, camPoint.position, cameraMoveSpeed * Time.deltaTime);

            if (theBoss.localScale != Vector3.one)
            {
                theBoss.localScale = Vector3.MoveTowards(theBoss.localScale, Vector3.one, bossGrowSpeed * Time.deltaTime);
            }

            if (theBoss.localScale == Vector3.one && projectileLauncher.localScale != Vector3.one)
            {
                projectileLauncher.localScale = Vector3.MoveTowards(projectileLauncher.localScale, Vector3.one, bossGrowSpeed * Time.deltaTime);
            }

            launcherRotation += laucncherRotateSpeed * Time.deltaTime;
            if (launcherRotation > 360) launcherRotation -= 360f;
            projectileLauncher.localRotation = Quaternion.Euler(0f, 0f, launcherRotation);

            //start shooting
            if (shootStartCounter > 0f)
            {
                shootStartCounter -= Time.deltaTime;
                if (shootStartCounter <= 0f)
                {
                    shotCounter = timeBetweenShots;

                    FireShoot();
                }
            }

            if (shotCounter > 0f)
            {
                shotCounter -= Time.deltaTime;
                if(shotCounter <= 0f)
                {
                    shotCounter = timeBetweenShots;

                    FireShoot();
                }
            }

            if (isWeak == false)
            {
                theBoss.transform.position = Vector3.MoveTowards(theBoss.transform.position, bossMovePoints[currentMovePoint].position, bossMoveSpeed * Time.deltaTime);

                if (theBoss.transform.position == bossMovePoints[currentMovePoint].position)
                {
                    currentMovePoint++;

                    if (currentMovePoint >= bossMovePoints.Length)
                    {
                        currentMovePoint = 0;
                    }
                }
            }
        }
    }
    public void AcitvateBattle()
    {
        bossActive = true;
        blockers.SetActive(true);
        camController.enabled = false;

        AudioManager.instance.bossMusicPlay();
    }

    void FireShoot()
    {
        Instantiate(projectileToFire, projectilePoints[currentShoot].position, projectilePoints[currentShoot].rotation);

        projectilePoints[currentShoot].gameObject.SetActive(false);
        currentShoot++;

        if (currentShoot >= projectilePoints.Length)
        {
            shotCounter = 0f;

            MakeWeak();
        }

        AudioManager.instance.allSFXPlay(2);
    }

    void MakeWeak()
    {
        bossAnim.SetTrigger("isWeak");
        isWeak = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isWeak == false)
            {
                PLayerHealthController.instance.DamagePLayer();
            }
            else
            {
                if (other.transform.position.y > theBoss.position.y)
                {
                    bossAnim.SetTrigger("Hit");
                    FindAnyObjectByType<PlayerController>().Jump();
                    MoveToNextPhase();
                }
            }
        }
    }

    void MoveToNextPhase()
    {
        currentPhase++;

        if (currentPhase < 3)
        {
            isWeak = false;

            waitToStartShooting *= 0.5f;
            timeBetweenShots *= 0.75f;
            bossMoveSpeed *= 1.5f;

            shootStartCounter = waitToStartShooting;

            projectileLauncher.localScale = Vector3.zero;

            foreach (Transform point in projectilePoints)
            {
                point.gameObject.SetActive(true);
            }

            currentShoot = 0;

            AudioManager.instance.allSFXPlay(1);
        }
        else
        {
            gameObject.SetActive(false);
            blockers.SetActive(false);

            camController.enabled = true;

            Instantiate(deathEffect, theBoss.position, Quaternion.identity);

            AudioManager.instance.allSFXPlay(0);

            AudioManager.instance.levelTracksPlay(FindFirstObjectByType<LevelMusicPlayer>().trackToPlay);
        }
    }
}
