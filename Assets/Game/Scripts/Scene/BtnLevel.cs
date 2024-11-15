using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BtnLevel : MonoBehaviour
{
    private Button btn;
    // Start is called before the first frame update
    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(LoadLevel);
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(btn.GetComponentInChildren<TextMeshProUGUI>().text);
    }
}
