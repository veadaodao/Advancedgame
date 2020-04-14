using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelInfoManager : MonoBehaviour
{
    public static LevelInfoManager instance;
    public LevelInfo[] levels;
    public LevelButton[] levelButtons;
    public LevelInfo activeLevel;

    public TextMeshProUGUI LevelNameText;
    public TextMeshProUGUI LevelDesText;

    public GameObject LevelPanel;
    public GameObject GoButton;
    public GameObject[] MainHouses;
    public Camera MainCamera;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
    public void Start()
    {
        LevelNameText.text = instance.levels[0].LevelName;
        LevelDesText.text = instance.levels[0].LevelDes;

    }
        public void Update()
    {
        MouseSelect();
    }

    private void MouseSelect()
    {
        if (Input.GetMouseButtonDown(1) && LevelPanel.activeInHierarchy)
        {
            LevelPanel.SetActive(false);
        }

    }

    public void PressGoLevelButton()
    {

        FindObjectOfType<AudioManager>().Play("click");
        MainCamera.transform.position = new Vector3(MainHouses[activeLevel.LevelId].transform.position.x, MainHouses[activeLevel.LevelId].transform.position.y + 10, MainHouses[activeLevel.LevelId].transform.position.z -50);
  
    }







}

    