using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class coin : MonoBehaviour
{
    public GameObject mario;
    public GameObject score;
    public GameObject highScore;

    private int scoreCount = 0;
    private int highScoreCount = 0;
    // Start is called before the first frame update
    void Awake(){
        mario = GameObject.Find("mario");
        score = GameObject.Find("score");
        highScore = GameObject.Find("highScore");
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       if(mario.activeSelf == false)
        {
            transform.position = new Vector3(0, 0, 0);
            score.GetComponent<TextMeshProUGUI>().text = "Score: 0";
            scoreCount = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == mario)
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
        if(collision.gameObject.tag == "Platform")
        {
            setCoinPosition();
        }
       
    }

    private void setCoinPosition(){
        float xPos = Random.Range(-8f, 8f);
        float yPos = Random.Range(-4f, 4f);
        transform.position = new Vector3(xPos, yPos, 0);

    }
}
