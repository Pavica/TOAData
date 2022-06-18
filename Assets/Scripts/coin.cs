using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class coin : MonoBehaviour
{
    public GameObject sonic;
    public GameObject score;
    public GameObject highScore;
    public string sceneName;

    private int scoreCount = 0;
    private int highScoreCount = 0;
    // Start is called before the first frame update
    void Awake(){
        sonic = GameObject.Find("sonic");
        score = GameObject.Find("score");
        highScore = GameObject.Find("highScore");
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(scoreCount == 3)
        {
            SceneManager.LoadScene(sceneName);
        }

       if(sonic.GetComponent<sonic>().isDead)
        {
            transform.position = new Vector3(0, 0, 0);
            score.GetComponent<TextMeshProUGUI>().text = "Score: 0";
            scoreCount = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == sonic)
        {
            scoreCount++;
            score.GetComponent <TextMeshProUGUI>().text = "Score: " + scoreCount.ToString();
            if(scoreCount > highScoreCount)
            {
                highScoreCount = scoreCount;
                highScore.GetComponent<TextMeshProUGUI>().text = "H. Score: " + highScoreCount.ToString();
            }
            setCoinPosition();
        }
        else if(collision.gameObject.tag != "Enemy")
        {
            setCoinPosition();    
        }
        
    }
   
    private void setCoinPosition(){
        //make this not random and connect it to the tilemap somehow
        float xPos = Random.Range(-8f, 8f);

        int eitherOr = Random.Range(0,2);
        float yPos;

        if (eitherOr == 0)
        {
            yPos = Random.Range(-4f, 4f);
        }
        else
        {
            yPos = Random.Range(8f, 14f);
        }
        
        transform.position = new Vector3(xPos, yPos, 0);
    }
}
