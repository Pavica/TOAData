using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject snake;
    private int counter = 0;

    private Vector2[] snakePos =
    {
        new Vector2(4.5f,2f),
        new Vector2(-4.5f, 2f),
        new Vector2(4.5f,-1f),
        new Vector2(-4.5f, -1f),
        new Vector2(0f, 6.5f),
        new Vector2(-4.5f, 12f),
        new Vector2(4.5f, 12f)
    };

    // Start is called before the first frame update
    void Start()
    {
        spawnSnakes();
    }

    void spawnSnakes()
    {
        for (int i = 0; i < snakePos.Length; i++)
        {
            Invoke("spawnSingleSnake", i);
        }
    }

    void spawnSingleSnake()
    {

        Vector3 position = new Vector3(snakePos[counter].x, snakePos[counter].y, 0);
        GameObject gameObject = Instantiate(snake, position, Quaternion.identity);
        counter++;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
