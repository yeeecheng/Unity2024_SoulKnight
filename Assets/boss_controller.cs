using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boss_controller : monster_action
{

    public GameObject range_bullet, shoot_bullet;
    private float timer3 = 0.0f, range_attack_interval = 1.0f;
    public Canvas gameover_UI;
    public Text title_text;

    void Update()
    {

        if (monster_active)
        {
            timer1 += Time.deltaTime;
            timer2 += Time.deltaTime;
            timer3 += Time.deltaTime;

            if (IsDie())
            {
                return;
            }
            // For moving.
            Action();

            Attack();
            RangeAttack();
        }
    }



    public override void HpReduce(float hp_need_reduce)
    {
        this.hp -= hp_need_reduce;
      
        if (hp <= 0) {
            hp = 0;
            gameover_UI.gameObject.SetActive(true);
            gameover_UI.GetComponent<gameover_UI>().SetClockText(Time.time.ToString());
            gameover_UI.GetComponent<gameover_UI>().SetKillText(11.ToString());
            title_text.text = "You Win!";
            Destroy(gameObject);
        }
    }
    void RangeAttack() {
        
        if (timer3 >= range_attack_interval){
            float angleStep = 360f / 18;
            for (int i = 0; i < 18; i++) {
               
                float angle = angleStep * i;
                Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                bullet_controller bullet = Instantiate(range_bullet, transform.position + 2 * direction, Quaternion.identity).GetComponent<bullet_controller>();
                bullet.SetSpeed(3.0f);
                bullet.SetDirection(direction);
                
            }
            timer3 = 0.0f;
        }
    }

    public override void Attack()
    {

        if (timer2 >= attack_interval) {
            // set monster bullet shoot forward player.
            Vector3 nor_direction = ChangeDirection().normalized;
            float angle = Mathf.Atan2(nor_direction.y, nor_direction.x) * Mathf.Rad2Deg;
            bullet_controller bullet = Instantiate(shoot_bullet, transform.position + 2 * nor_direction, Quaternion.Euler(new Vector3(0, 0, angle))).GetComponent<bullet_controller>();
            bullet.SetSpeed(10.0f);
            bullet.SetDirection(nor_direction);
            timer2 = 0;
        }
    }
}
