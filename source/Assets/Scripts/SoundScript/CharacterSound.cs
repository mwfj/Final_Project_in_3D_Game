using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class CharacterSound : MonoBehaviour
{
    private AudioSource audioCharater;
    public AudioClip c_walk;
    public AudioClip c_jump;
    public AudioClip c_attack1;
    public AudioClip c_attack5;
    // Start is called before the first frame update
    void Start()
    {
        audioCharater = this.GetComponent<AudioSource>();
    }
    void CharacterWalkSound(float vol = 1f){
        audioCharater.PlayOneShot(c_walk);
        audioCharater.volume = vol;
    }
    void CharacterJumpSound(float vol = 1f){
        audioCharater.PlayOneShot(c_jump);
        audioCharater.volume = vol;
    }
    void CharacterAttack1Sound(float vol = 1f){
        audioCharater.PlayOneShot(c_attack1);
        audioCharater.volume = vol;
    }
    void CharacterAttack5Sound(float vol = 1f){
        audioCharater.PlayOneShot(c_attack5);
        audioCharater.volume = vol;
    }
}
