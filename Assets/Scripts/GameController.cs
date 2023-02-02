using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject snake;
    private int counter = 0;
    Vector2[] array = { };

    private Vector2[] snakePos =
    {
        new Vector2(4.5f,2f),
        new Vector2(-4.5f, 2f),
        new Vector2(4.5f,-1f),
        new Vector2(-4.5f, -1f),
    };

    private Vector2[] snakePos2 =
    {
        new Vector2(4.5f,2f),
        new Vector2(-4.5f, 2f),
        new Vector2(4.5f,-1f),
        new Vector2(-4.5f, -1f),
        new Vector2(0f, 6.5f),
        new Vector2(-4.5f, 12f),
        new Vector2(4.5f, 12f)
    };

    private Vector2[] snakePos3 =
    {
        new Vector2(4.5f,1f),
        new Vector2(-4.5f, 1f),
        new Vector2(4.5f,6.5f),
        new Vector2(-4.5f, 6.5f),
        new Vector2(0, 12f),
    };

    private Vector2[] snakePos4 =
    {
        new Vector2(4.5f,3.75f),
        new Vector2(-4.5f, 3.75f),
        new Vector2(4.5f,0f),
        new Vector2(-4.5f, 0f),
        new Vector2(0f, 6.5f),
        new Vector2(0f, 12f),
    };

    // Start is called before the first frame update
    void Start()
    {
        spawnSnakes();
    }

    void spawnSnakes()
    {
        counter = 0;
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            array = snakePos;
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            array = snakePos2;
        }
        else if (SceneManager.GetActiveScene().name == "Level3")
        {
            array = snakePos3;
        }
        else if (SceneManager.GetActiveScene().name == "Level4")
        {
            array = snakePos4;
        }
        for (int i = 0; i < array.Length; i++)
        {
            Invoke("spawnSingleSnake", i);
        }
    }

    void spawnSingleSnake()
    {
        Vector3 position = new Vector3(array[counter].x, array[counter].y, 0);
        GameObject gameObject = Instantiate(snake, position, Quaternion.identity);
        counter++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
