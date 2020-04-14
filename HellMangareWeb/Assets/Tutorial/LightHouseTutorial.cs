using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHouseTutorial : MonoBehaviour
{
    public GameObject HouseLight;
    HellScoreTutorial HellScore;

    // Start is called before the first frame update
    void Start()
    {
        GameObject ScoreObject = GameObject.Find("HellScoreManager");
        HellScore = ScoreObject.GetComponent<HellScoreTutorial>();
        HouseLight.SetActive(false);
    }
   

    // Update is called once per frame
    void Update()
    {
        MouseSelect();

        if (HellScore.HellLight == 1 || HellScore.LampOnIndex == 1)
        {
            HouseLight.SetActive(true);
        }
        else
        {
            HouseLight.SetActive(false);
        }
    }
    private void MouseSelect()
    {
        if (Input.GetMouseButtonDown(1))
        {

            if (HouseLight.activeInHierarchy == true)
            {
                HouseLight.SetActive(false);
            }

        }
    }
}
