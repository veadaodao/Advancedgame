using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButton : MonoBehaviour
{
    public TextMeshProUGUI LevelNameText;
    public TextMeshProUGUI LevelDesText;


    public int buttonId;


        // Clike button to display each level Info
        public void PressLevelButton()
    {
        FindObjectOfType<AudioManager>().Play("click");
        LevelInfoManager.instance.activeLevel = transform.GetComponent<LevelInfo>();
        LevelNameText.text = LevelInfoManager.instance.levels[buttonId].LevelName;
        LevelDesText.text = LevelInfoManager.instance.levels[buttonId].LevelDes;

    }
}
