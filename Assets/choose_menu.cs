using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class choose_menu : MonoBehaviour
{
    public Button left_buttom, right_buttom, confirm_bottom, unlock_bottom;
    public Image[] player;
    public Text player_name, hp, armor, mp;
    public Canvas bg_UI, main_gam_UI;
    public GameObject bg_audio;
    private int page = 0;
    // setting
    private string[] player_names = { "ÃM¤h", "ªª®v", "¼C©v", "ºëÆF" };
    private string[,] player_property = new string[,] { { "6", "5", "180" }, { "3", "5", "200" }, { "5", "4", "160" }, { "5", "4", "180" } };
    void Start() {

        ShowPlayer(0);
        SetText(0);
        // create buttom listener
        left_buttom.onClick.AddListener(left_buttom_click);
        right_buttom.onClick.AddListener(right_buttom_click);
        confirm_bottom.onClick.AddListener(confirm_buttom_click);
    }

    void Update() {
    }

    void ShowPlayer(int idx) {
        player[idx].gameObject.SetActive(true);
        for (int i = 0; i < player.Length; i++) {
            if (i != idx) {
                player[i].gameObject.SetActive(false);
            }
        }
    }

    void SetText(int idx) {
        player_name.text = player_names[idx];
        hp.text = player_property[idx, 0];
        armor.text = player_property[idx, 1];
        mp.text = player_property[idx, 2];
        if(idx == 0) {
            confirm_bottom.gameObject.SetActive(true);
            unlock_bottom.gameObject.SetActive(false);
        }
        else {
            confirm_bottom.gameObject.SetActive(false);
            unlock_bottom.gameObject.SetActive(true);
        }
    }

    public void left_buttom_click() {
        page = ((page - 1) + player.Length) % player.Length;
        ShowPlayer(page);
        SetText(page);
    }

    public void right_buttom_click() {
        page = (page + 1) % player.Length;
        ShowPlayer(page);
        SetText(page);
    }

    public void confirm_buttom_click() {
        gameObject.SetActive(false);
        bg_UI.gameObject.SetActive(false);
        main_gam_UI.gameObject.SetActive(true);
        bg_audio.GetComponent<bg_audio_controller>().ChangeAudio("1-1");
    }

    public void unlock_buttom_click() {
        //nothing
    }
}
