using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIController : MonoBehaviour
{
    //Singleton
    public static UIController instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private Image[] heartIcons;
    [SerializeField] private Sprite healthFull, healthEmpty;
    public TMP_Text liveText, collectiblesText;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private string mainMenuScreen;
    [SerializeField] private Image fadeScreen;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private bool fadingToBlack, fadingFromBlack;
    [SerializeField] private GameObject waitScreen;
 
    // Start is called before the first frame update
    void Start()
    {
        FadeFromBlack();
        StartCoroutine(WaitScreen());   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }

        if (fadingFromBlack)
        { 
            //Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime) co nghia la Do mo tu 1 -> 0 dan dan theo tung Frame voi fadeSpeed
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
        }

        if (fadingToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
        }
    }

    public void UpdateHealthDisplay(int health, int maxHealth)
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].enabled = true;
            /* if (health <= i)
            {
                heartIcons[i].enabled = false;
            } */

            //Neu ma luong mau lon hon i thi se co ngan ay mau
            if (health > i)
            {
                heartIcons[i].sprite = healthFull;
            }
            else
            {
                //Cac tym con lai thi la Empty (Bao gom ca tym thua)
                heartIcons[i].sprite = healthEmpty;

                //Cac tym thua duoc an di
                if (maxHealth <= i)
                {
                    heartIcons[i].enabled = false;
                }
            }
        }
    }

    public void UpdateLiveDisplay(int currentLives)
    {
        liveText.text = currentLives.ToString();
    }

    public void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void Resetart()
    {
        /*
        SceneManager: Đây là một lớp trong Unity giúp quản lý các màn chơi.
        GetActiveScene(): Hàm này trả về màn chơi đang hoạt động hiện tại.
        .name: Lấy tên của màn chơi đang hoạt động.
        LoadScene(): Hàm này được sử dụng để tải một màn chơi có tên nhất định.
        */
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void UpdateCollectibleDisplay(int currentCollectibles)
    {
        collectiblesText.text = currentCollectibles.ToString();
    }

    public void PauseUnpause()
    {
        //Xem GameObject do co dang Active khong
        if (pauseScreen.activeSelf == false)
        {
            pauseScreen.SetActive(true);

            //Dung chuong trinh
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);

            Time.timeScale = 1f;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScreen);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game 02");
    }

    public void FadeFromBlack()
    {
        fadingToBlack = false;
        fadingFromBlack = true;
    }

    public void FadeToBlack()
    {
        fadingToBlack=true;
        fadingFromBlack=false;
    }

    private IEnumerator WaitScreen()
    {
        yield return new WaitForSeconds(0.2f);
        waitScreen.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        waitScreen.SetActive(false);
    }
}
