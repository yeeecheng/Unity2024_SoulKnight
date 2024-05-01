using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class player_action : MonoBehaviour
{

    public GameObject bulletprefab;

    private float move_unit = 0.03f;
    public float hp = 6.0f;
    public float armor = 3.0f;
    public float mp = 200.0f;

    private GameObject weapon1 = null, weapon2 = null;
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


        // weapone controller 
        SwitchWeapon();

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

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "weapon") {
            if (Input.GetKeyDown(KeyCode.K)){
                // After pick, disable collision
                collision.enabled = false;
                GetWeapon(collision.gameObject);
            }
        }
    }


    void GetWeapon(GameObject new_weapon){
        
        // position of weapon on the ground.
        Vector3 org_pos = new_weapon.transform.position;
        // Set new weapon become the child object of player.
        new_weapon.transform.SetParent(transform);
        new_weapon.transform.localPosition = new Vector3(0, -0.045f, 0);
        
        // There are weapon1 and weapon2.
        if(weapon1 != null && weapon2 != null) {
            // switch position of weapon on the ground and weapon1.
            GameObject remove_weapon = weapon1;
            remove_weapon.transform.SetParent(null);
            remove_weapon.transform.position = org_pos;
            remove_weapon.GetComponent<BoxCollider2D>().enabled = true;
        }
        // There is weapon1 and not weapon2.
        else if(weapon1 != null && weapon2 == null) {
            // weapon1 become weapon2.
            weapon2 = weapon1;
        }
        // weapon on the ground become weapon1.
        weapon1 = new_weapon;

    }

    // switch weapon1 to weapon2 and weapon2 to weapon1.
    void SwitchWeapon(){
        
        if (Input.GetKeyDown(KeyCode.L) && weapon1 != null && weapon2 != null) {
            GameObject tmp = weapon1;
            weapon1 = weapon2;
            weapon2 = tmp;
        }
        
    }

    // bullet genertatation
    void Fire(){
        if (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0)){
            GameObject bullet = Instantiate(bulletprefab) as GameObject;
            //GetComponentInChildren
            //GameObject weapon = transform.GetChild(1).
            Transform[] child = this.GetComponentsInChildren<Transform>();
            
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
