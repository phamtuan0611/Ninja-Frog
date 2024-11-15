using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMLoader : MonoBehaviour
{
    public AudioManager theAM;

    private void Awake()
    {
        if (AudioManager.instance == null)
        {
            //Tao ra phien ban moi bang Instantiate(theAM) roi goi no bang SetupAudioManager() de su dung
            Instantiate(theAM).SetupAudioManager();
        }
    }
}
