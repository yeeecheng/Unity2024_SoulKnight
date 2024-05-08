using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

public class trap_controller : MonoBehaviour
{
    public Tile trap_on_tile, trap_off_tile;

    private float interval = 5f, timer1 = 0f, damage_interval = 2f, timer2;
    private Tilemap tilemap;
    private bool trap_on = false, damage_on = false;
    void Start() {
        tilemap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        timer1 += Time.deltaTime;
        if(timer1 > interval) {
            if (!trap_on) {
                ChangeTile(trap_on_tile, trap_off_tile);
                trap_on = true;
            }
            else {
                ChangeTile(trap_off_tile, trap_on_tile);
                trap_on = false;
            }
            timer1 = 0f;
        }

        if(damage_on) {
            timer2 += Time.deltaTime; 
            if(timer2 > damage_interval) {
                damage_on = false;
                timer2 = 0f;
            }
        }


    }

    void ChangeTile(Tile new_tile, Tile old_tile) {
       
        foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin) {
            if(tilemap.GetTile(position) == old_tile) {
                tilemap.SetTile(position, new_tile);
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.gameObject.tag == "player") {
            if (trap_on && !damage_on) {
                collision.gameObject.GetComponent<player_action>().HpReduce(1);
                damage_on = true;
            }
        }
    }
}
