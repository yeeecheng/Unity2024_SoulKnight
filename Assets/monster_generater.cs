using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.iOS;
using UnityEngine;

public class monster_generater : MonoBehaviour
{
    public int[] monster_num_genetate;
    private GameObject[] monsters;
    private int cur_monster_source_num;
    public int round = 1;
    
    void Start() {
        
        // first round already generate.
        round += monster_num_genetate.Length;
        // must in the Assets/Resources;
        monsters = Resources.LoadAll<GameObject>("monster_prefab");
        cur_monster_source_num = monsters.Length;
    }

    void Update() {
        
        
        // current round over
        if (!ChkCurRoundStatus() && round > 0) {
            round -= 1;
            generate_monster();
        }

    }

    bool ChkCurRoundStatus() {
        
        // check number of monster
        if(transform.childCount == 0) {
            return false;
        }
        return true;
    
    }

    void generate_monster() {

        int num = monster_num_genetate[monster_num_genetate.Length - round];
        for(int i = 1; i <= num; i++) {
            Bounds bounds = GetComponent<Collider2D>().bounds;
            Vector3 random_position = generate_random_position(bounds.center, bounds.extents);
            GameObject monster = Instantiate(monsters[Random.Range(0, cur_monster_source_num)], random_position, Quaternion.identity);
            monster.GetComponent<monster_action>().monster_active = true;
            monster.transform.SetParent(transform);
        }
    }
    
    Vector3 generate_random_position(Vector3 center, Vector3 extent) {
        return new Vector3(Random.Range(center.x - extent.x, center.x + extent.x),
            Random.Range(center.y - extent.y, center.y + extent.y), 0);
    }

}
