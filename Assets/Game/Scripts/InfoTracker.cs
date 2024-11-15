using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTracker : MonoBehaviour
{
    public static InfoTracker instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            transform.SetParent(null); //Vi dang de trong Level Pack, neu khong set null thi khong lay bo vo DontDestroyOnLoad duoc
            DontDestroyOnLoad(gameObject);

            if (PlayerPrefs.HasKey("lives"))
            {
                currentLives = PlayerPrefs.GetInt("lives");
                currentFruits = PlayerPrefs.GetInt("fruits");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int currentLives, currentFruits;

    public void GetInfo()
    {
        if (LifeController.instance != null)
        {
            currentLives = LifeController.instance.currentLive;
        }

        if (CollectiblesManager.instance != null)
        {
            currentFruits = CollectiblesManager.instance.collectibleCount;
        }
    }

    public void SaveInfo()
    {
        PlayerPrefs.SetInt("lives", currentLives);
        PlayerPrefs.SetInt("fruits", currentFruits);
    }
}
