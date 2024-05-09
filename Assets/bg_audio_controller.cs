using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg_audio_controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeAudio(string next_level) {
        AudioSource[] audio = GetComponents<AudioSource>();
        for (int i = 0; i < audio.Length; i++) {
            // stop current music
            if (audio[i].isPlaying) {
                audio[i].Pause();
            }
            // start new music
            if (audio[i].clip.name == next_level) {
                audio[i].Play();
            }
        }
    }
}
