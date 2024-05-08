using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class bullet_controller : MonoBehaviour
{

    private float speed = 10.0f;
    private Vector3 direction;
    private float attack;
    // Start is called before the first frame update
    void Start()
    {
        SetDirection(transform.right);
    }

    // Update is called once per frame
    void Update() {
        // because the weapon are always facing the positive x-axis, shooting transform.right
        //Debug.Log("fire direc: " + direction);
        transform.localPosition += direction * speed * Time.deltaTime;
    }

    public void SetDirection(Vector3 bullet_direction) {
        direction = bullet_direction;
        //Debug.Log("set direc: " + direction);
    }   

    public void SetAttack(float attack) {
        this.attack = attack;
    }

    public virtual void OnCollisionEnter2D(Collision2D collision) {
        /*
        if(collision.gameObject.name == "wall"){
            Destroy(gameObject);
        }
        else
        */
        if (collision.gameObject.tag == "enemy") {
            collision.gameObject.GetComponent<monster_action>().HpReduce(attack);
            //Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
