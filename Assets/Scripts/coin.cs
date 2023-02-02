using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class coin : MonoBehaviour 
{
    public GameObject sonic;
    public GameObject score;
    public GameObject bestTime;
    public GameObject timer;
  
    private int scoreCount;

    //luck calculation
    public float distance = 0.0f;
    public float positionChangeTimer = 0.0f;
    public bool distanceable;

    //timers
    public float myTimer = 0.0f;
    public float myLvlTimer = 0.0f;
    public float myHelpTimer = 0.0f;

    // Start is called before the first frame update
    void Awake(){
        sonic = GameObject.Find("sonic");
        score = GameObject.Find("score");
        bestTime = GameObject.Find("bestTime");
        timer = GameObject.Find("timer");

        distance = 0;

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            PlayerPrefs.SetFloat("time", 0.0f);
        }
        if (PlayerPrefs.HasKey("time"))
        {
            myTimer = PlayerPrefs.GetFloat("time");
            myLvlTimer = myTimer;
        }

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            PlayerPrefs.SetFloat("distance", 0.0f);
        }

        if (PlayerPrefs.HasKey("distance"))
        {
            distance = PlayerPrefs.GetFloat("distance");

        }
      
        if (PlayerPrefs.HasKey("bestTime"))
        {
            bestTime.GetComponent<TextMeshProUGUI>().text = "Best: " + FormatTime(PlayerPrefs.GetFloat("bestTime"));
        }
        else
        {
            bestTime.GetComponent<TextMeshProUGUI>().text = "Best: 00:00:000";
        }
    }

    void Start()
    {
        distance += Vector3.Distance(transform.position, sonic.transform.position);
    }

    private void Update()
    {
        positionChangeTimer += Time.deltaTime;

        if(positionChangeTimer > 0.05f && distanceable)
        {
            distance += Vector3.Distance(transform.position, sonic.transform.position);
            distanceable = false;
        }

        myTimer += Time.deltaTime;
        timer.GetComponent<TextMeshProUGUI>().text = "Time: " + FormatTime(myTimer);       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerPrefs.SetFloat("time", myTimer);
        PlayerPrefs.SetFloat("distance", distance);

        if (scoreCount == 10 && SceneManager.GetActiveScene().name == "Level1")
        {
            PlayerPrefs.SetFloat("lastLvl1Time", myTimer);
            if (!PlayerPrefs.HasKey("segmentedLvl1Time") || myTimer < PlayerPrefs.GetFloat("segmentedLvl1Time"))
            {
                PlayerPrefs.SetFloat("segmentedLvl1Time", myTimer);
            }
            SceneManager.LoadScene("Level2");

        } else if(scoreCount == 15 && SceneManager.GetActiveScene().name == "Level2") {
            myHelpTimer = myTimer - myLvlTimer;
            PlayerPrefs.SetFloat("lastLvl2Time", myHelpTimer);
            if (!PlayerPrefs.HasKey("segmentedLvl2Time") || myHelpTimer < PlayerPrefs.GetFloat("segmentedLvl2Time"))
            {
                PlayerPrefs.SetFloat("segmentedLvl2Time", myHelpTimer);
            }
            SceneManager.LoadScene("Level3");
        }
        else if (scoreCount == 20 && SceneManager.GetActiveScene().name == "Level3")
        {
            myHelpTimer = myTimer - myLvlTimer;
            PlayerPrefs.SetFloat("lastLvl3Time", myHelpTimer);
            if (!PlayerPrefs.HasKey("segmentedLvl3Time") || myHelpTimer < PlayerPrefs.GetFloat("segmentedLvl3Time"))
            {
                PlayerPrefs.SetFloat("segmentedLvl3Time", myHelpTimer);
            }
            SceneManager.LoadScene("Level4");
        }
        else if (scoreCount == 25 && SceneManager.GetActiveScene().name == "Level4")
        {
            PlayerPrefs.SetFloat("distanceRank", rankAverageDistance(distance / 70));
            
            myHelpTimer = myTimer - myLvlTimer;
            PlayerPrefs.SetFloat("lastLvl4Time", myHelpTimer);
            if (!PlayerPrefs.HasKey("segmentedLvl4Time") || myHelpTimer < PlayerPrefs.GetFloat("segmentedLvl4Time"))
            {
                PlayerPrefs.SetFloat("segmentedLvl4Time", myHelpTimer);
            }

            PlayerPrefs.SetFloat("lastTime", myTimer);
            if (!PlayerPrefs.HasKey("bestTime") || myTimer < PlayerPrefs.GetFloat("bestTime"))
            {
                PlayerPrefs.SetFloat("bestTime", myTimer);
                PlayerPrefs.SetFloat("bestLvl1Time", PlayerPrefs.GetFloat("lastLvl1Time"));
                PlayerPrefs.SetFloat("bestLvl2Time", PlayerPrefs.GetFloat("lastLvl2Time"));
                PlayerPrefs.SetFloat("bestLvl3Time", PlayerPrefs.GetFloat("lastLvl3Time"));
                PlayerPrefs.SetFloat("bestLvl4Time", PlayerPrefs.GetFloat("lastLvl4Time"));
                PlayerPrefs.SetFloat("distanceRankBest", PlayerPrefs.GetFloat("distanceRank"));
            }
            SceneManager.LoadScene("Title");
        }

        PlayerPrefs.SetFloat("segmentedTime", PlayerPrefs.GetFloat("segmentedLvl1Time") + PlayerPrefs.GetFloat("segmentedLvl2Time") + PlayerPrefs.GetFloat("segmentedLvl3Time") + PlayerPrefs.GetFloat("segmentedLvl4Time"));

        if (sonic.GetComponent<sonic>().isDead)
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject == sonic)
        {
            scoreCount++;
            score.GetComponent <TextMeshProUGUI>().text = "Score: " + scoreCount.ToString();
            setCoinPosition();
        }
        else if(collision.gameObject.tag != "Enemy")
        {
            setCoinPosition();
        }
    }

    public string FormatTime(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        return string.Format("{0:D2}:{1:D2}:{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
    }

    public float rankAverageDistance(float distance)
    {
        float formula = 100 * ((Vector3.Distance(new Vector3(-8, -4), new Vector3(8, 12))/2 - distance) / 
            (Vector3.Distance(new Vector3(-8,-4), new Vector3(8,12))/2))*2;
        return formula;
    }

    private void setCoinPosition(){
        positionChangeTimer = 0.0f;
        float xPos = UnityEngine.Random.Range(-8f, 8f);
        float yPos = UnityEngine.Random.Range(-4f, 12f);
        transform.position = new Vector3(xPos, yPos, 0);
        distanceable = true;
    }
}
