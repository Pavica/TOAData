using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snake : MonoBehaviour
{
    private float xStart;
    private float xOffset;
    private float xLength;
    private float yPos;
    

    // Start is called before the first frame update
    void Start()
    {
        //deathText.SetActive(false); Find way to fix
        xStart = transform.position.x;
        yPos = transform.position.y;    
        xOffset = -Random.Range(3f, 5f);
        xLength = Random.Range(2.5f, 3.5f);
    }

    void Awake(){
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xPos = transform.position.x + xOffset * Time.deltaTime;
        transform.position = new Vector3(xPos, yPos, 0);
        if(xPos > xStart + xLength || xPos < xStart - xLength){
            transform.Rotate(0, 180, 0);
            xOffset *= -1;
        }
    }
}
