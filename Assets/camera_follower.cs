using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follower : MonoBehaviour
{
    private GameObject player;  // 玩家的Transform
    private Vector3 offset = new Vector3(0, 0, -10);  // 攝像機與玩家的偏移量

    void Start() {
        player = GameObject.FindWithTag("player");
    }

    void Update()
    {
        // 攝像機的位置等於玩家的位置加上偏移量
        transform.position = player.transform.position + offset;
    }
}
