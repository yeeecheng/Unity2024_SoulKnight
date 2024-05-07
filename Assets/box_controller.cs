using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class box_controller : MonoBehaviour
{

    public GameObject treasure;
    public Image indicator;
    public Text description;

    public GameObject left_box, right_box;
    private bool is_collision = false;
    private float speed = 0.4f;
    private float idicator_move = 0.9f;
    private bool box_open = false;

    // Start is called before the first frame update
    void Start()
    {
        indicator.gameObject.SetActive(false);
        description.gameObject.SetActive(false);
        treasure.GetComponent<BoxCollider2D>().enabled = false;

    }

    void Update() {
        if(is_collision) {
            if(idicator_move > 0.9f || idicator_move < 0.7f) {
                speed = -speed;
            }
           
            idicator_move += speed * Time.deltaTime;
            description.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.2f, 0));
            indicator.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, idicator_move, 0));

            ChkBoxOpen();
        }
    }
    
    void ChkBoxOpen() {
        if(!box_open && Input.GetKeyDown(KeyCode.J)) {
            box_open = true;
            // box open
            right_box.transform.position += new Vector3(0.3f, 0, 0);
            left_box.transform.position += new Vector3(-0.3f, 0, 0);
            // treasure trigger open;
            treasure.GetComponent<BoxCollider2D>().enabled = true;
            // box image cloase
            indicator.gameObject.SetActive(false);
            description.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
       
        if(collision.gameObject.tag != "player") {
            return;
        }
        if (!box_open) {
            indicator.gameObject.SetActive(true);
            description.gameObject.SetActive(true);
            is_collision = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        
        is_collision = false;
        indicator.gameObject.SetActive(false);
        description.gameObject.SetActive(false);
    }
}
