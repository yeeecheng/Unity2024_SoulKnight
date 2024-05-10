using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_action : MonoBehaviour
{
    public float armor_capacity = 3.0f, mp_capacity = 200.0f, hp_capacity = 6.0f;
    public Canvas gameover_UI, bg_UI;
    
    private float timer = 0.0f;
    private float waiting_armor_interval = 5.0f, armor_increase_interval = 2.0f;
    private float speed = 6.0f;
    private float hp, mp, armor;
    private status_UI_controller status_UI;
    private GameObject weapon1 = null, weapon2 = null;
    private Animator animator;
    private AudioSource fire_audio, switch_audio;
    private bool is_die = false;

    void Start(){
        animator = GetComponent<Animator>();
        hp = hp_capacity;
        mp = mp_capacity;
        armor = armor_capacity;
        status_UI = transform.GetComponent<status_UI_controller>();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        fire_audio = audioSources[0];
        switch_audio = audioSources[1];

    }

    // Update is called once per frame
    void Update(){
        //Debug.Log(Time.time);
        if (!is_die && IsDie() ) {
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
            transform.localPosition += transform.up * speed * Time.deltaTime;
            walk_direc[0] = true;
        }

        if (Input.GetKey(KeyCode.S)){
            transform.localPosition += transform.up * -1 * speed * Time.deltaTime;
            walk_direc[1] = true;
        }

        if (Input.GetKey(KeyCode.A)){
            // player always is facing the positive x-axis
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
            transform.position += transform.right * speed * Time.deltaTime;
            walk_direc[2] = true;
        }

        if (Input.GetKey(KeyCode.D)){
            // player always is facing the positive x-axis
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
            transform.position += transform.right * speed * Time.deltaTime;
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
            
            if (Input.GetKeyDown(KeyCode.K)){
                // After pick, disable collision
                collision.enabled = false;
                GetWeapon(collision.gameObject);
            }
        }
        else if (collision.gameObject.tag == "mp") {
            
            GetMp(collision);
        }
    }

    private void GetMp(Collider2D mp) {

        Destroy(mp.gameObject);

        if (this.mp == mp_capacity) {
            return;
        }

        if(this.mp + 4  <= mp_capacity) {
            this.mp += 4;
        }
        else {
            this.mp = mp_capacity;
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
            switch_audio.Play();
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
            fire_audio.Play();
            // position and rotation of the bullet are the same as with the weapon.
            bullet_controller bullet = Instantiate(weapon1.GetComponent<weapon_action>().bulletprefab, weapon1.transform.position + weapon1.transform.right * 1.0f, weapon1.transform.rotation).GetComponent<bullet_controller>();
            bullet.SetAttack(weapon1.GetComponent<weapon_action>().attack);
            //bullet.SetDirection(transform.right);
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
            // armor should be 0
            armor = 0;
            // remaining damage
            if(hp < hp_need_reduce) {
                hp = 0;
            }
            else {
                hp -= hp_need_reduce;
            }
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
            gameover_UI.gameObject.SetActive(true);
            gameover_UI.GetComponent<gameover_UI>().SetClockText(Time.time.ToString());
            gameover_UI.GetComponent<gameover_UI>().SetKillText(11.ToString());
            is_die = true;
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
