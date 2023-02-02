using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class segmentedTimeTitle : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.HasKey("segmentedTime"))
        {
            GetComponent<TextMeshProUGUI>().text = "Segmented \n" + FormatTime(PlayerPrefs.GetFloat("segmentedTime")) + "\n";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = "Segmented \n 00:00:000 \n";
        }

        if (PlayerPrefs.HasKey("segmentedLvl1Time"))
        {
            GetComponent<TextMeshProUGUI>().text += "LVL1 \n" + FormatTime(PlayerPrefs.GetFloat("segmentedLvl1Time")) + "\n";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text += "LVL1 \n 00:00:000 \n";
        }
        if (PlayerPrefs.HasKey("segmentedLvl2Time"))
        {
            GetComponent<TextMeshProUGUI>().text += "LVL2 \n" + FormatTime(PlayerPrefs.GetFloat("segmentedLvl2Time")) + "\n";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text += "LVL2 \n 00:00:000 \n";
        }
        if (PlayerPrefs.HasKey("segmentedLvl3Time"))
        {
            GetComponent<TextMeshProUGUI>().text += "LVL3 \n" + FormatTime(PlayerPrefs.GetFloat("segmentedLvl3Time")) + "\n";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text += "LVL3 \n 00:00:000 \n";
        }
        if (PlayerPrefs.HasKey("segmentedLvl4Time"))
        {
            GetComponent<TextMeshProUGUI>().text += "LVL4 \n" + FormatTime(PlayerPrefs.GetFloat("segmentedLvl4Time")) + "\n";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text += "LVL4 \n 00:00:000 \n";
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
