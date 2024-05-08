using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box_monster_area_controller : MonoBehaviour
{
    public GameObject left_box, right_box, weapon_mp;
    private bool is_open = false; 
    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (!is_open && collision.gameObject.tag == "player") {
            is_open = true;
            right_box.transform.position += new Vector3(0.3f, 0, 0);
            left_box.transform.position += new Vector3(-0.3f, 0, 0);
            int generate_num = Random.Range(10, 30);
            Bounds bounds = GetComponent<Collider2D>().bounds;
            for(int i = 1; i <= generate_num; i++) {
                Instantiate(weapon_mp, GenerateRandomPosition(bounds.center, bounds.extents), Quaternion.identity);
            }   
        }
       
    }

    Vector3 GenerateRandomPosition(Vector3 center, Vector3 extent) {
        return new Vector3(Random.Range(center.x - extent.x, center.x + extent.x),
            Random.Range(center.y - extent.y, center.y + extent.y), 0);
    }
}
