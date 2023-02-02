using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class StartGame : MonoBehaviour
{
    public string thisName;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Level1");
        }
    }
    public void loadGame()
    {
        SceneManager.LoadScene(thisName);
    }
}
