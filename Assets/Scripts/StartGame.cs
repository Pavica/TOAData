using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class StartGame : MonoBehaviour
{
    public string name;

    public void loadGame()
    {
        SceneManager.LoadScene(name);
    }
}
