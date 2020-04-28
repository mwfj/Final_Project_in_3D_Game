using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class BossSoundController : MonoBehaviour
{
    private AudioSource audioBoss;
    public AudioClip b_roar;
    public AudioClip b_punch;
    // Start is called before the first frame update
    void Start()
    {
        audioBoss = this.GetComponent<AudioSource>();
    }

    void BossRoarSound(float vol = 1f){
        audioBoss.PlayOneShot(b_roar);
        audioBoss.volume = vol;
    }
    void BossPunchSound(float vol = 1f){
        audioBoss.PlayOneShot(b_punch);
        audioBoss.volume = vol;
    }
    
}
