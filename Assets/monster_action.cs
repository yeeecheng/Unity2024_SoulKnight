using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class monster_action : MonoBehaviour
{
    public bool monster_active = false;

    public float timer1 = 0.0f, timer2 = 0.0f;
    private float speed = 1.0f;
    public float hp = 4.0f;
    protected Animator animator;
    private GameObject player;
    private float walk_interval;
    private float idle_interval;
    protected float attack_interval;

    void Start() {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("player");
        walk_interval = Random.Range(3, 16);
        idle_interval = Random.Range(2, 6);
        attack_interval = 3.0f;
    }

    // Update is called once per frame
    void Update() {

        if (monster_active) {
            timer1 += Time.deltaTime;
            timer2 += Time.deltaTime;

            if (IsDie()) {
                return;
            }
            // For moving.
            Action();

            Attack();
        }
    }

    public virtual bool IsDie() {

        if (hp == 0) {
            animator.SetTrigger("DieTrigger");
            return true;
        }
        return false;
    }

    // switch action between move and idel
    public void Action() {
       
        if (timer1 <= walk_interval){
            Move();
        }
        ChangeDirection();
        if (timer1 > (walk_interval + idle_interval)){
            timer1 = 0.0f;
        }
    }

    public Vector3 ChangeDirection() {

        // track the player position.
        Vector3 distance = player.transform.position - transform.position;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        if (distance.x < 0){
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
        }
        return distance;
    }
    public void Move() {
        animator.SetTrigger("WalkTrigger");
        Vector3 distance = ChangeDirection();
        Vector3 direction = distance.normalized;
        //Debug.Log(Mathf.Sqrt(Mathf.Pow(distance.x, 2) + Mathf.Pow(distance.y, 2)));
        if (Mathf.Sqrt(Mathf.Pow(distance.x, 2) + Mathf.Pow(distance.y, 2)) > 0.85f){
            transform.position += direction * speed * Time.deltaTime;
        }

    }
    public virtual void HpReduce(float hp_need_reduce) {
        this.hp -= hp_need_reduce;
       
        if(hp <= 0) {
            hp = 0;
            Destroy(gameObject);
        }
    }

    public virtual void Attack() {
        
        // for short-distance attack monster in the touch player.
        Vector3 distance = ChangeDirection();
        if (Mathf.Sqrt(Mathf.Pow(distance.x, 2) + Mathf.Pow(distance.y, 2)) <= 1.0f) {
            player.GetComponent<player_action>().HpReduce(1);
        }
        timer2 = 0.0f;
    }
    
}
