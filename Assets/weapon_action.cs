using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class weapon_action : MonoBehaviour {


    public Image indicator;
    public Text description;
    public Image status;
    public float attack, mp;

    private Text[] status_text;
    private bool is_trigger = false;
    private float speed = 0.4f;
    private float idicator_move = 0.9f;
    void Start()
    {
        status.gameObject.SetActive(false);
        indicator.gameObject.SetActive(false);
        description.gameObject.SetActive(false);

        status_text = status.GetComponentsInChildren<Text>();
        for(int i = 0; i < status_text.Length; i++) {
         
            if (status_text[i].gameObject.name == "mp_text") {
                status_text[i].text = mp.ToString();
            }
            else if (status_text[i].gameObject.name == "attack_text") {
                status_text[i].text = attack.ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (is_trigger) {
            if (idicator_move > 0.9f || idicator_move < 0.7f) {
                speed = -speed;
            }

            idicator_move += speed * Time.deltaTime;
            description.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.2f, 0));
            indicator.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, idicator_move, 0));

        }
    }

    public void change_direction(bool[] walk_direc) {

        int rotation_direc = 90;
        // W, D => up_right / W, A => up_left
        if ((walk_direc[0] && walk_direc[2]) || (walk_direc[0] && walk_direc[3])) {
            rotation_direc = 45;
        }
        else if((walk_direc[1] && walk_direc[2]) || (walk_direc[1] && walk_direc[3])) {
            rotation_direc = -45;
        }
        else if (walk_direc[0]) {
            rotation_direc = 90;
        }
        else if (walk_direc[1]) {
            rotation_direc = -90;
        }
        else if (walk_direc[2] || walk_direc[3]) {
            rotation_direc = 0;
        }
      
        transform.localRotation = Quaternion.Euler(0, 0, rotation_direc);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     
        if(collision.gameObject.tag == "player") {
            indicator.gameObject.SetActive(true);
            description.gameObject.SetActive(true);
            status.gameObject.SetActive(true);
            is_trigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        indicator.gameObject.SetActive(false);
        description.gameObject.SetActive(false);
        status.gameObject.SetActive(false);
        is_trigger = false;
    }
}
