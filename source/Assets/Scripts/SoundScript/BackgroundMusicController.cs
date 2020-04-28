using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    private AudioSource main_audio;
    public AudioClip maze_background_music;
    public AudioClip boss_Room_music;
    public AudioClip victory_music;
    // Start is called before the first frame update
    void Start()
    {
        main_audio = GetComponent<AudioSource>();
        main_audio.clip = maze_background_music;
        main_audio.loop = true;
        main_audio.volume = 0.6f;
        main_audio.Play();
    }
    public void ChangeToMazeMusic(){
        if(main_audio.clip != maze_background_music){
            main_audio.Stop();
            main_audio.clip = maze_background_music;
            main_audio.volume = 0.6f;
            main_audio.loop = true;
            main_audio.Play();
        }
    }
    public void ChangeMusicToBoomRoom(){
        main_audio.Stop();
        main_audio.clip = boss_Room_music;
        main_audio.volume = 0.45f;
        main_audio.loop = true;
        main_audio.Play();
    }
    public void ChangeMusicToVicotry(){
        main_audio.Stop();
        main_audio.clip = victory_music;
        main_audio.volume = 0.65f;
        main_audio.loop = true;
        main_audio.Play();
    }
    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.P)){
        //     ChangeMusicToBoomRoom();
        // }
        
    }
}
