using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootAudio : MonoBehaviour
{
    public void PlayStep(string audio)
    {
        MusicManager.Instance.playRun(audio);
    }
}
