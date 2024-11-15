using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PLayerHealthController : MonoBehaviour
{
    //Singleton
    public static PLayerHealthController instance;

    private void Awake()
    {
        instance = this;
    }

    public int currentHealth, maxHealth;

    //Tao tym bat tu tam thoi
    [SerializeField] private float invincibilityLength = 1f;
    private float invincibilityCounter;

    [SerializeField] private SpriteRenderer theSR;
    [SerializeField] private Color normalColor, fadeColor;

    private PlayerController thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);

        thePlayer = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            if (invincibilityCounter <= 0)
            {
                theSR.color = normalColor;
            }
        }

//Chi khi dang dung Unity thi moi thuc hien. Con khi phat hanh Game thi khong
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AddHealth(1);
        }
    }
#endif

    public void DamagePLayer()
    {
        if (invincibilityCounter <= 0)
        {
            //invincibilityCounter = invincibilityLength;

            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //gameObject.SetActive(false);
                LifeController.instance.Respawn();
            }
            else
            {
                invincibilityCounter = invincibilityLength;

                theSR.color = fadeColor;

                thePlayer.isKnock();
                AudioManager.instance.allSFXPlay(13);
            }

            UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
        }

    }

    public void AddHealth(int amountToAdd)
    {
        currentHealth += amountToAdd;
        
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
    }
}
