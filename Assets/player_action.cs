using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class player_action : MonoBehaviour
{

    public GameObject bulletprefab;
    private float speed = 0.03f;
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

        Vector3 prev_pos = transform.localPosition;
        // W, S, A, D
        bool[] walk_direc = {false, false, false, false};
        

        if (Input.GetKey(KeyCode.W)){
            transform.localPosition += transform.up * speed;
            walk_direc[0] = true;
        }

        if (Input.GetKey(KeyCode.S)){
            transform.localPosition += transform.up * -1 * speed;
            walk_direc[1] = true;
        }

        if (Input.GetKey(KeyCode.A)){
            // player always is facing the positive x-axis
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
            transform.position += transform.right * speed;
            walk_direc[2] = true;
        }

        if (Input.GetKey(KeyCode.D)){
            // player always is facing the positive x-axis
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
            transform.position += transform.right * speed;
            walk_direc[3] = true;
        }

        // triiger walk animation
        if(transform.localPosition != prev_pos){
            animator.SetTrigger("WalkTrigger");

            if(weapon1 != null) {
                weapon1.GetComponent<weapon_action>().change_direction(walk_direc);
            }
            
            // Fixing weapons don't change position.
            Transform[] child_transform = transform.GetComponentsInChildren<Transform>();
            for (int i = 0; i < child_transform.Length; i++) {
                if (child_transform[i].tag == "weapon") {
                    child_transform[i].localPosition = new Vector3(0, -0.045f, 0);
                }
            }
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
        //new_weapon.transform.localScale = new Vector3(Mathf.Abs(new_weapon.transform.localScale.x), new_weapon.transform.localScale.y, new_weapon.transform.localScale.z);

        // There are weapon1 and weapon2.
        if (weapon1 != null && weapon2 != null) {
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
        if (weapon1 != null && (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0))){
            // position and rotation of the bullet are the same as with the weapon.
            GameObject bullet = Instantiate(bulletprefab, weapon1.transform.position + weapon1.transform.right * 1.0f, weapon1.transform.rotation );


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
