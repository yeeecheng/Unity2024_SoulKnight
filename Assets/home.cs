using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class home : MonoBehaviour
{

    public Button start_buttom;
    public Canvas bg_UI, choose_menu;
    void Start()
    {
        gameObject.SetActive(true);
        start_buttom.onClick.AddListener(start_button_click);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void start_button_click() {
        gameObject.SetActive(false);
        bg_UI.gameObject.SetActive(true);
        choose_menu.gameObject.SetActive(true);
    }
}
