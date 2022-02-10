using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject mario;

    void Start()
    {
        mario = GameObject.Find("mario");
    }
    void FixedUpdate()
    {
        transform.position = new Vector3(0, mario.transform.position.y + 2, -8);
    }
}
