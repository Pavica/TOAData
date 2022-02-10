using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snake : MonoBehaviour
{
    public GameObject deathText;
    public GameObject mario;
    private float xStart;
    private float xOffset;
    private float xLength;
    private float yPos;
    
    // Start is called before the first frame update
    void Start()
    {
        deathText.SetActive(false);
        xStart = transform.position.x;
        yPos = transform.position.y;    
        xOffset = -Random.Range(5f, 7.5f) * Time.deltaTime;
        xLength = Random.Range(2.5f, 3.5f);
    }

    void Awake(){
        mario = GameObject.Find("mario");
        deathText = GameObject.Find("deathText");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xPos = transform.position.x + xOffset;
        transform.position = new Vector3(xPos, yPos, 0);
        if(xPos > xStart + xLength || xPos < xStart - xLength){
            transform.Rotate(0, 0, 180);
            xOffset *= -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject == mario){
            mario.SetActive(false);
            deathText.SetActive(true);
            Invoke("nextLife", 2);
        }
    }

    void nextLife(){
        mario.transform.position = new Vector3(0, -4f, 0);
        mario.SetActive(true);
        deathText.SetActive(false);
    }
}
