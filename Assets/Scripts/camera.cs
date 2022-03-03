using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject sonic;

    void Start()
    {
        sonic = GameObject.Find("sonic");
    }
    void FixedUpdate()
    {
        transform.position = new Vector3(0, sonic.transform.position.y + 2, -8);
    }
}
