using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_action : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, -21);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void change_direction(bool[] walk_direc) {

        int rotation_direc = 90;
        // W, D => up_right / W, A => up_left
        if ((walk_direc[0] && walk_direc[2]) || (walk_direc[0] && walk_direc[3])) {
            rotation_direc = 45;
        }
        else if((walk_direc[1] && walk_direc[2]) || (walk_direc[1] && walk_direc[3])) {
            rotation_direc = -45;
        }
        else if (walk_direc[0]) {
            rotation_direc = 90;
        }
        else if (walk_direc[1]) {
            rotation_direc = -90;
        }
        else if (walk_direc[2] || walk_direc[3]) {
            rotation_direc = 0;
        }
      
        transform.localRotation = Quaternion.Euler(0, 0, rotation_direc);
    }
}
