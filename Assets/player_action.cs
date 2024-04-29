using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class player_action : MonoBehaviour
{

    public GameObject bulletprefab;

    private float move_unit = 0.03f;
    public float hp = 6.0f;
    public float armor = 3.0f;
    public float mp = 200.0f;

    private Animator animator;
    //private SpriteRenderer spriteRenderer;
    void Start(){
        animator = GetComponent<Animator>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){
        
        if (IsDie()) {
            return;
        }
        Move();
        Fire();

        /* test die
        if (Input.GetKeyDown(KeyCode.Space)){
            hp -= 1;
        }
        */
    }

    void Move(){

        
        Vector3 prev_pos = transform.position;

        if (Input.GetKey(KeyCode.W)){
            transform.position += new Vector3(0, move_unit, 0);
        }

        if (Input.GetKey(KeyCode.A)){
            transform.position += new Vector3(-move_unit, 0, 0);
            if(transform.localScale.x > 0){
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }

        if (Input.GetKey(KeyCode.S)){
            transform.position += new Vector3(0, -move_unit, 0);
        }

        if (Input.GetKey(KeyCode.D)){
            transform.position += new Vector3(move_unit, 0, 0);
            if (transform.localScale.x < 0){
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }

        if(transform.position != prev_pos){
            animator.SetTrigger("WalkTrigger");
        }

    }

    // bullet genertatation
    void Fire(){
        if (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0)){
            GameObject bullet = Instantiate(bulletprefab) as GameObject;
            Vector3 pos = transform.position;
            bullet.transform.position = pos;
        }
    }

    bool IsDie(){

        if (hp == 0){
            animator.SetTrigger("DieTrigger");
            return true;       
        }
        return false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        if(collision.gameObject.tag == "Enemy") { 
            
        }
        else if(collision.gameObject.tag == "wall")
        {
            Destroy(gameObject);
        }
        */
        print(collision.gameObject.name);
        Debug.Log(collision.gameObject.name);
    }
}
