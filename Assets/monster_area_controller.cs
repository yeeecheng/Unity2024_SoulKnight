using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class monster_area_controller : MonoBehaviour
{
    private Tilemap doorBody_tilemap;
    private Tilemap doorHead_tilemap;
    private bool monster_active = false;
    private GameObject player = null;
    private bool player_inside = false;

    void Start()
    {
        doorBody_tilemap = GameObject.Find("door_body").GetComponent<Tilemap>();
        doorHead_tilemap = GameObject.Find("door_head").GetComponent <Tilemap>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (ChkEndFight()) {
            doorBody_tilemap.GetComponent<TilemapCollider2D>().enabled = false;
            doorBody_tilemap.GetComponent<TilemapRenderer>().enabled = false;
            doorHead_tilemap.GetComponent<TilemapCollider2D>().enabled = false;
            doorHead_tilemap.GetComponent<TilemapRenderer>().enabled = false;
        }
        
        if (player != null && !player_inside && IsPlayerInTriggerArea()) {
            doorBody_tilemap.GetComponent<TilemapCollider2D>().enabled = true;
            doorBody_tilemap.GetComponent<TilemapRenderer>().enabled = true;
            doorHead_tilemap.GetComponent<TilemapCollider2D>().enabled = true;
            doorHead_tilemap.GetComponent<TilemapRenderer>().enabled = true;
            monster_active = true;
            ActiveMonsters();
            player_inside = true;
            
        }
        if(monster_active) {
            // generate monster
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if(collision.gameObject.tag != "player") {
            return;
        }
        player = collision.gameObject;
        Debug.Log(player);
    }

    bool IsPlayerInTriggerArea() {
        Bounds player_bounds = player.GetComponent<Collider2D>().bounds;
        Bounds trigger_bounds = GetComponent<Collider2D>().bounds;
        Vector3 t_center = trigger_bounds.center, t_extent = trigger_bounds.extents;
        Vector3 p_center = player_bounds.center, p_extent = player_bounds.extents;

        return (t_center.x + t_extent.x >= p_center.x + p_extent.x) && (t_center.x - t_extent.x <= p_center.x - p_extent.x) &&
            (t_center.y + t_extent.y >= p_center.y + p_extent.y) && (t_center.y - t_extent.y <= p_center.y - p_extent.y);
    }
    bool ChkEndFight() {
        return false;
    }

    void ActiveMonsters() {
        monster_action[] all_monstesr = transform.GetComponentsInChildren<monster_action>();
        for(int i = 0; i < all_monstesr.Length; i++){
            all_monstesr[i].monster_active = true;
        }
    }


}
