using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class bestTimeTitle : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.HasKey("bestTime"))
        {
            GetComponent<TextMeshProUGUI>().text = "Best \n" + FormatTime(PlayerPrefs.GetFloat("bestTime")) + "\n";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = "Best \n 00:00:000 \n";
        }

        if (PlayerPrefs.HasKey("bestLvl1Time"))
        {
            GetComponent<TextMeshProUGUI>().text += "LVL1 \n" + FormatTime(PlayerPrefs.GetFloat("bestLvl1Time")) + "\n";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text += "LVL1 \n 00:00:000 \n";
        }
        if (PlayerPrefs.HasKey("bestLvl2Time"))
        {
            GetComponent<TextMeshProUGUI>().text += "LVL2 \n" + FormatTime(PlayerPrefs.GetFloat("bestLvl2Time")) + "\n";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text += "LVL2 \n 00:00:000 \n";
        }
        if (PlayerPrefs.HasKey("bestLvl3Time"))
        {
            GetComponent<TextMeshProUGUI>().text += "LVL3 \n" + FormatTime(PlayerPrefs.GetFloat("bestLvl3Time")) + "\n";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text += "LVL3 \n 00:00:000 \n";
        }
        if (PlayerPrefs.HasKey("bestLvl4Time"))
        {
            GetComponent<TextMeshProUGUI>().text += "LVL4 \n" + FormatTime(PlayerPrefs.GetFloat("bestLvl4Time")) + "\n";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text += "LVL4 \n 00:00:000 \n";
        }
        if (PlayerPrefs.HasKey("distanceRankBest"))
        {
            GetComponent<TextMeshProUGUI>().text += "Luck \n" + ((int)PlayerPrefs.GetFloat("distanceRankBest")).ToString("000") + "\n";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text += "Luck \n 000 \n";
        }
    }

    public string FormatTime(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        return string.Format("{0:D2}:{1:D2}:{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
