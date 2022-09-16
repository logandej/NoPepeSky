using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private AudioSource music, effect, step;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
      

    }
    private void Start()
    {
    }


    public void playMusic(string audio)
    {
        music.clip = Resources.Load<AudioClip>(audio);
        music.Play();
    }

    public void playEffect(string audio)
    {
        effect.clip = Resources.Load<AudioClip>(audio);
        effect.Play();
    }
    public void playRun(string audio)
    {
        step.volume = Random.Range(0.8f, 1);
        step.pitch = Random.Range(0.8f, 1.2f);
        step.clip = Resources.Load<AudioClip>(audio);
        step.Play(); 
    }
    public bool getStepPlaying()
    {
        return step.isPlaying;
    }

    public void setMusicVolume(float value)
    {
        Instance.music.volume = value;
    }

    public void setEffectVolume(float value)
    {
        Instance.effect.volume = value;
    }

    public void setStepVolume(float value)
    {
        Instance.step.volume = value;
    }
}
