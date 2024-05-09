using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_attack : bullet_controller
{
    public float damage = 1f;

    public override void OnCollisionEnter2D(Collision2D collision)
    {

        // player reduce hp after being shoot by monster bullet
        if (collision.gameObject.tag == "player")
        {
            collision.gameObject.GetComponent<player_action>().HpReduce(damage);
        }

        Destroy(gameObject);
    }
}
