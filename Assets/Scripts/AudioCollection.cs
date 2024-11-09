using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioCollection : MonoBehaviour
{
    [Header("========== Output ==========")]
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource SFX;

    [Header("========== Background Music ==========")]
    public AudioClip mainMenu;
    public AudioClip inRoad;
    public AudioClip inCasino;
    public AudioClip inRepairShop;
    public AudioClip inRoom;
    
    [Header("========== SFX ==========")]
    public AudioClip EnterButtonClick;
    public AudioClip walk;
    public AudioClip doorSound;

    public void ButtonPress()
    {
        SFX.PlayOneShot(EnterButtonClick);
    }
    
    public void PlayBGM(AudioClip clip)
    {
        BGM.clip = clip;
        BGM.loop = true;
        BGM.Play();
    }

    public void StopPlayBGM()
    {
        BGM.Stop();
    }
    public void StopPlaySFX()
    {
        SFX.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }
    public void PlaySFXLoop(AudioClip clip)
    {
        SFX.clip = clip;
        SFX.loop = true;
        SFX.Play();
    }

}
