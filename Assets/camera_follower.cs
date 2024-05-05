using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follower : MonoBehaviour
{
    private GameObject player; 
    private Vector3 offset = new Vector3(0, 0, -10);  

    void Start() {
        player = GameObject.FindWithTag("player");
    }

    void Update()
    {
       
        transform.position = player.transform.position + offset;
    }
}
