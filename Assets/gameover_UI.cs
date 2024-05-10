using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameover_UI : MonoBehaviour
{
    public Button next_button;
    public Text clock_text, kill_text;
    void Start()
    {
        next_button.onClick.AddListener(next_button_click);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetClockText(string text) {
        clock_text.text = text;
    }

    public void SetKillText(string text) {
        kill_text.text = text;
    }


    public void next_button_click() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
