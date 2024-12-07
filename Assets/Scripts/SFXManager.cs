using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    public AudioClip spikeClip;
    public GameObject sfxObject;
    public AudioClip bulletHitClip;
    public AudioClip buffPickupClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        GameObject sfx = Instantiate(sfxObject);
        sfx.GetComponent<AudioSource>().clip = sfxClip;
        sfx.GetComponent<AudioSource>().Play();
    }
}
