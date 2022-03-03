using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject snake;
    private int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        spawnSnakes();
    }

    void spawnSnakes()
    {
        for(int i=0; i<5; i++)
        {
            Invoke("spawnSingleSnake", i);
        }
    }

    void spawnSingleSnake()
    {
        float xPos = 3.5f * (counter % 2 == 0 ? 1 : -1);
        float yPos = -3 + (counter % 3) * 3.0f;
        Vector3 position = new Vector3(xPos, yPos, 0);
        GameObject gameObject = Instantiate(snake, position, Quaternion.identity);
        counter++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
