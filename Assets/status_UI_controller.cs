using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class status_UI_controller : MonoBehaviour
{
    private Image hp_bar, armor_bar, mp_bar;
    private Text hp_text, armor_text, mp_text;
    
    /// <summary>
    /// Accroding to the changing of hp, mp and armor, update the UI. 
    /// </summary>
    void Start() {
        hp_bar = GameObject.Find("HP").GetComponent<Image>();
        armor_bar = GameObject.Find("Armor").GetComponent<Image>();
        mp_bar = GameObject.Find("MP").GetComponent<Image>();
        hp_text = GameObject.Find("hp_text").GetComponent<Text>();
        armor_text = GameObject.Find("armor_text").GetComponent<Text>();
        mp_text = GameObject.Find("mp_text").GetComponent<Text>();

    }

    public void HP_change(float cur_hp, float hp_capcity) {
        hp_bar.fillAmount = cur_hp / hp_capcity;
        hp_text.text = cur_hp.ToString() + "/" + hp_capcity.ToString(); ;
    }

    public void MP_change(float cur_mp, float mp_capcity) {
        mp_bar.fillAmount = cur_mp / mp_capcity;
        mp_text.text = cur_mp.ToString() + "/" + mp_capcity.ToString();
    }

    public void Armor_change(float cur_armor, float armor_capcity){
        armor_bar.fillAmount = cur_armor / armor_capcity;
        armor_text.text = cur_armor.ToString() + "/" + armor_capcity.ToString();
    }
}
