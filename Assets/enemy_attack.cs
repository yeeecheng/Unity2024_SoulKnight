using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_attack : monster_action
{
    public GameObject bulletprefab;
    public override void Attack() {
       
        if (timer2 >= attack_interval) {
            // set monster bullet shoot forward player.
            Vector3 nor_direction = ChangeDirection().normalized;
            float angle = Mathf.Atan2(nor_direction.y, nor_direction.x) * Mathf.Rad2Deg;
            bullet_controller bullet = Instantiate(bulletprefab, transform.position + 2 * nor_direction, Quaternion.Euler(new Vector3(0, 0, angle))).GetComponent<bullet_controller>();
            bullet.SetDirection(nor_direction);
            timer2 = 0;
        }
    }
}
