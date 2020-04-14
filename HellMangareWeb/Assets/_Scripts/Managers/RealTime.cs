using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealTime : MonoBehaviour
{
    public int currentDay = 0; //day 8287... still stuck in this grass prison... no esacape... no freedom...
    public Light directionalLight; //the directional light in the scene we're going to work with
    public float SecondsInAFullDay; //in realtime, this is about two minutes by default. (every 1 minute/60 seconds is day in game)
    [Range(0, 1)]
    public float currentTime = 0; //at default when you press play, it will be nightTime. (0 = night, 1 = day)
    [HideInInspector]
    public float timeMultiplier = 1f; //how fast the day goes by regardless of the secondsInAFullDay var. lower values will make the days go by longer, while higher values make it go faster. 
    //This may be useful if you're siumulating seasons where daylight and night times are altered.
    //public TextMeshProUGUI Day;
    // Start is called before the first frame update
    public int index;

    public GameObject PauseButton;
    public GameObject ResumeButton;

    protected bool paused;
    public bool IfYearUpdate;
    void Start()
    {

    }

    public void OnPauseGame()
    {
        PauseButton.SetActive(false);
        ResumeButton.SetActive(true);
        FindObjectOfType<AudioManager>().Play("click");
        paused = true;
    }
    
    public void OnResumeGame()
    {
        FindObjectOfType<AudioManager>().Play("click");
        PauseButton.SetActive(true);
        ResumeButton.SetActive(false);
        paused = false;
    }
    public void OnSlowDownTime()
    {
        FindObjectOfType<AudioManager>().Play("click");
        index = 1;
    }
    public void OnNormalTime()
    {
        FindObjectOfType<AudioManager>().Play("click");
        index = 2;
    }
    public void OnSuperSpeedUpTime()
    {
        FindObjectOfType<AudioManager>().Play("click");
        index = 3;
    }
    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            currentTime += (Time.deltaTime / SecondsInAFullDay) * timeMultiplier;
            IfYearUpdate = false;
            if (currentTime >= 1)
            {
                currentTime = 0;//once we hit "midnight"; any time after that sunrise will begin.
                currentDay++; //make the day counter go up
                IfYearUpdate = true;
            }
    

        }
        switch (index)
        {
            case 1:
                SecondsInAFullDay = 300f;
                break;
            case 2:
                SecondsInAFullDay = 120f;
                break;
            case 3:
                SecondsInAFullDay = 60f;
                break;
        }
    }

}
