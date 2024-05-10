using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bg_audio_controller : MonoBehaviour
{

    public Scrollbar scrollbar;
    private float current_audio_height;
    private AudioSource current_audio;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audio = GetComponents<AudioSource>();
        for (int i = 0; i < audio.Length; i++)
        {
            if (audio[i].isPlaying)
            {
                current_audio = audio[i];
                break;
            }
        }

        scrollbar.value = current_audio.volume;
    }
    // Update is called once per frame
    void Update()
    {
        VolumeChanged();
       
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
                current_audio = audio[i];
            }
        }
    }

    public void VolumeChanged() {
        current_audio.volume = scrollbar.value;
        current_audio_height = scrollbar.value;
   
    }
}
