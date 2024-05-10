using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transport_controller : MonoBehaviour {

    public GameObject transport_destination, bg_audio;
    public string next_level;
    private float speed = 1f, rotation;
    private GameObject player = null;
    
    void Start()
    {
        rotation = speed;
    }

    void Update() {
        transform.rotation = Quaternion.Euler(0, 0, rotation);

        rotation += speed;
        rotation %= 360;

        if (player != null) {
            if (Input.GetKeyDown(KeyCode.J)) {
                Transport();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.gameObject.tag == "player") {
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "player") {
            player = null;
        }
    }

    void Transport() {
        player.transform.position = transport_destination.transform.position;
        bg_audio.GetComponent<bg_audio_controller>().ChangeAudio(next_level);  
    }

    
}
    