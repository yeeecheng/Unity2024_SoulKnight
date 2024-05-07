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
    private float timer = 0.0f;
    private float waiting_armor_interval = 5.0f, armor_increase_interval = 2.0f;
    public GameObject bulletprefab;
    private float speed = 0.03f;
    public float armor_capacity = 3.0f, mp_capacity = 200.0f, hp_capacity = 6.0f;
    private float hp, mp, armor;
    private status_UI_controller status_UI;
    private GameObject weapon1 = null, weapon2 = null;
    private Animator animator;
    //private SpriteRenderer spriteRenderer;
    void Start(){
        animator = GetComponent<Animator>();
        hp = hp_capacity;
        mp = mp_capacity;
        armor = armor_capacity;
        status_UI = transform.GetComponent<status_UI_controller>();
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

        // armor increase timer
        if(timer != 0.0f){
            timer -= Time.deltaTime;
            if(timer < 0.0f) {
                ArmorIncrease();
            }
        }

        Status_UI_change();

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

    
    private void OnTriggerStay2D(Collider2D collision) {
        
        if (collision.gameObject.tag == "weapon") {
            //Debug.Log(collision.gameObject.name);
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
        new_weapon.GetComponent<Renderer>().sortingOrder = 2;
       
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
            weapon1.GetComponent<Renderer>().sortingOrder = 0;
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
            weapon1.GetComponent<Renderer>().sortingOrder = 2;
            weapon2.GetComponent<Renderer>().sortingOrder = 0;
        }
       
    }

    // bullet genertatation
    void Fire(){
        if (weapon1 != null && mp >= weapon1.GetComponent<weapon_action>().mp  && (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0))){
            // position and rotation of the bullet are the same as with the weapon.
            GameObject bullet = Instantiate(bulletprefab, weapon1.transform.position + weapon1.transform.right * 1.0f, weapon1.transform.rotation );
            bullet.GetComponent<bullet_controller>().SetAttack(weapon1.GetComponent<weapon_action>().attack);
            mp -= weapon1.GetComponent<weapon_action>().mp;
        }
    }

    public void HpReduce(float hp_need_reduce) {

        // first reduce armor
        if(armor > 0 && armor >= hp_need_reduce) {
            armor -= hp_need_reduce;
        }
        // second reduce HP
        else if(armor < hp_need_reduce) {
            hp_need_reduce -= armor;
            hp -= hp_need_reduce;
        }
        timer = waiting_armor_interval;

        //Debug.Log("armor: " + armor + " hp: " + hp);
    }


    void Status_UI_change() {
        status_UI.HP_change(hp, hp_capacity);
        status_UI.MP_change(mp, mp_capacity);
        status_UI.Armor_change(armor, armor_capacity);
    }

    // Haven't been attacked in a while, armor increase.
    void ArmorIncrease() {
        armor += 1;
        timer = 0;
        if(armor < armor_capacity) {
            timer = armor_increase_interval;
        }

        //Debug.Log("armor: " + armor + " hp: " + hp);
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
        //Debug.Log(collision.gameObject.name);
    }
}
