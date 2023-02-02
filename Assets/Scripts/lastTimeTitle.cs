using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class lastTimeTitle : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.HasKey("lastTime") && Mathf.Approximately(PlayerPrefs.GetFloat("lastTime"), PlayerPrefs.GetFloat("lastLvl1Time") + PlayerPrefs.GetFloat("lastLvl2Time") + PlayerPrefs.GetFloat("lastLvl3Time") + PlayerPrefs.GetFloat("lastLvl4Time")))
        {
            
            GetComponent<TextMeshProUGUI>().text = "Last \n" + FormatTime(PlayerPrefs.GetFloat("lastTime")) + "\n";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = "Last \n 00:00:000 \n";
        }

        if (PlayerPrefs.HasKey("lastLvl1Time"))
        {
            GetComponent<TextMeshProUGUI>().text += "LVL1 \n" + FormatTime(PlayerPrefs.GetFloat("lastLvl1Time")) + "\n";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text += "LVL1 \n 00:00:000 \n";
        }
        if (PlayerPrefs.HasKey("lastLvl2Time"))
        {
            GetComponent<TextMeshProUGUI>().text += "LVL2 \n" + FormatTime(PlayerPrefs.GetFloat("lastLvl2Time")) + "\n";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text += "LVL2 \n 00:00:000 \n";
        }
        if (PlayerPrefs.HasKey("lastLvl3Time"))
        {
            GetComponent<TextMeshProUGUI>().text += "LVL3 \n" + FormatTime(PlayerPrefs.GetFloat("lastLvl3Time")) + "\n";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text += "LVL3 \n 00:00:000 \n";
        }
        if (PlayerPrefs.HasKey("lastLvl4Time"))
        {
            GetComponent<TextMeshProUGUI>().text += "LVL4 \n" + FormatTime(PlayerPrefs.GetFloat("lastLvl4Time")) + "\n";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text += "LVL4 \n 00:00:000 \n";
        }
        if (PlayerPrefs.HasKey("distanceRank"))
        {
            GetComponent<TextMeshProUGUI>().text += "Luck \n" + ((int)PlayerPrefs.GetFloat("distanceRank")).ToString("000") + "\n";
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
