using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class bullet_controller : MonoBehaviour
{

    private float speed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed, 0, 0); 
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
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name == "wall"){
            Destroy(gameObject);
        }
    }
}
