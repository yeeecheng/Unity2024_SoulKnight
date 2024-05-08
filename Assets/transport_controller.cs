using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class transport_controller : MonoBehaviour
{
    private float speed = 1f, rotation;
    // Start is called before the first frame update
    void Start()
    {
        rotation = speed;
    }

    // Update is called once per frame
    void Update() {
        transform.rotation = Quaternion.Euler(0, 0, rotation);

        rotation += speed;
        rotation %= 360;
    }
}
    