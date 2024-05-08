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
        Debug.Log(round);
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
            GenerateMonster();
        }

    }

    bool ChkCurRoundStatus() {
        
        // check number of monster
        if(transform.childCount == 0) {
            return false;
        }
        return true;
    
    }

    void GenerateMonster() {
        if(round == 0) {
            return;
        }
        int num = monster_num_genetate[monster_num_genetate.Length - round];
        for(int i = 1; i <= num; i++) {
            Bounds bounds = GetComponent<Collider2D>().bounds;
            GameObject generate_monster = monsters[Random.Range(0, cur_monster_source_num)];
            Vector3 random_position = GenerateRandomPosition(bounds.center, bounds.extents);
            while(ChkCollision(random_position, generate_monster)) {
                random_position = GenerateRandomPosition(bounds.center, bounds.extents);
            }

            GameObject monster = Instantiate(generate_monster, random_position, Quaternion.identity);
            monster.GetComponent<monster_action>().monster_active = true;
            monster.transform.SetParent(transform);
        }
    }
    
    Vector3 GenerateRandomPosition(Vector3 center, Vector3 extent) {
        return new Vector3(Random.Range(center.x - extent.x, center.x + extent.x),
            Random.Range(center.y - extent.y, center.y + extent.y), 0);
    }


    bool ChkCollision(Vector3 position, GameObject monster){
        
        return Physics.CheckBox(position, new Vector3(50f, 50f, 50f));
    }
}
