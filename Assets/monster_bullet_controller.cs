using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_bullet_controller : bullet_controller
{
    public float damage = 1f;
    public override void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "wall") {
            Destroy(gameObject);
        }
        // player reduce hp after being shoot by monster bullet
        else if (collision.gameObject.tag == "player") {
            collision.gameObject.GetComponent<player_action>().HpReduce(damage);
            Destroy(gameObject);
        }
    }
}
