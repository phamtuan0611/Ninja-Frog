using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public Animator anim;

    private bool isEnding;

    public string nextLevel;

    public float waitToEndLevel = 2f;

    public GameObject blocker;

    public float timeFade = 1f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isEnding == false)
        {
            if (other.CompareTag("Player"))
            {
                isEnding = true;
                anim.SetTrigger("ended");

                AudioManager.instance.completeLevelMusicPlay();

                blocker.SetActive(true);

                StartCoroutine(EndLevelCo());
            }
        }
    }

    public IEnumerator EndLevelCo()
    {
        yield return new WaitForSeconds(waitToEndLevel - timeFade);

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds(timeFade);

        InfoTracker.instance.GetInfo();
        InfoTracker.instance.SaveInfo();

        PlayerPrefs.SetString("currentLevel", nextLevel);

        SceneManager.LoadScene(nextLevel);
    }
}
