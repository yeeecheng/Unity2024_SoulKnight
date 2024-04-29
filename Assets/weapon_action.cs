using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_action : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0.01f, -0.05f, 0);
        transform.rotation = Quaternion.Euler(0, 0, -21);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
