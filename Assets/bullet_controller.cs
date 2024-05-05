using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class bullet_controller : MonoBehaviour
{

    private float speed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        // because the weapon are always facing the positive x-axis, shooting transform.right
        transform.localPosition += transform.right * speed;
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
        // Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name == "wall"){
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "enemy") {
            collision.gameObject.GetComponent<monster_action>().HpReduce(1);
            Destroy(gameObject);
        }
    }
}
