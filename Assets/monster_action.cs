using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class monster_action : MonoBehaviour
{
    private float speed = 0.005f;
    float timer = 0.0f;
    public float hp = 4.0f;
    private Animator animator;
    private GameObject player;
    public bool monster_active = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("player");
        //InvokeRepeating("Move", 5f, moveInterval);
    }

    // Update is called once per frame
    void Update() {


        if (monster_active) {
            timer += Time.deltaTime;
            //Debug.Log(timer);
            if (IsDie()) {
                return;
            }
            Action();
        }


    }

    bool IsDie()
    {

        if (hp == 0)
        {
            animator.SetTrigger("DieTrigger");
            return true;
        }
        return false;
    }

    // switch action between move and idel
    void Action()
    {
        if (timer < 10.0f){
            Move();
        }
        ChangeDirection();
        if (timer >= 15.0f){
            timer = 0.0f;
        }
    }

    Vector3 ChangeDirection()
    {
        Vector3 distance = player.transform.position - transform.position;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        if (distance.x < 0){
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
        }
        return distance;
    }
    void Move()
    {
        animator.SetTrigger("JumpTrigger");
        Vector3 distance = ChangeDirection();
        Vector3 direction = distance.normalized;
        //Debug.Log(Mathf.Sqrt(Mathf.Pow(distance.x, 2) + Mathf.Pow(distance.y, 2)));
        if (Mathf.Sqrt(Mathf.Pow(distance.x, 2) + Mathf.Pow(distance.y, 2)) > 0.85f){
            transform.position += direction * speed;
        }

    }
    public void HpReduce(float hp_need_reduce){
        this.hp -= hp_need_reduce; 
        if(hp <= 0) {
            Destroy(gameObject);
        }
    }
}
