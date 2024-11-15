using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string firstLevel;
    public int startLive = 3, startFruit = 0;
    public GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.menuMusicPlay();

        if (PlayerPrefs.HasKey("currentLevel"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.M))
        {
            AudioManager.instance.levelTracksPlay(1);
        }

#if UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.S))
        {
            PlayerPrefs.DeleteAll();
        }
#endif
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void NewGame()
    {
        //PlayerPrefs.DeleteAll();
        InfoTracker.instance.currentLives = startLive;
        InfoTracker.instance.currentFruits = startFruit;

        InfoTracker.instance.SaveInfo();

        SceneManager.LoadScene(firstLevel);
    }

    public void Continue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("currentLevel"));
    }
}
