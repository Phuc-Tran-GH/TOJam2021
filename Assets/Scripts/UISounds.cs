using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : Singleton<UISounds>
{
    [SerializeField] private AudioSource audio;

    public AudioClip upgradeSound;
    public AudioClip okSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayUpgradeSound()
    {
        audio.PlayOneShot(upgradeSound);
    }

    public void PlayOKSound()
    {
        audio.PlayOneShot(okSound);
    }
}
