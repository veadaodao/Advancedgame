using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoreNewSoul : MonoBehaviour
{
    Renderer renderer;

    public bool selected = false;
    public bool hover = false;
    bool justSelected = false;
    public RaycastHit hit;
    public LayerMask layerMask;

    public Transform NewSoulShowUpBox; 
    public GameObject[] Souls;

    public Sprite PureSoulImage;
    public Sprite SinSoulImage;
    public Sprite BackgroundImage;
    public Image ProfileImage;
    public Image ProfileImage1;
    public Image ProfileImage2;
    private int index;
    private bool IsCreatePure;
    private bool MainBuildInfo;
    private bool PressPure;
    public Image ProgressBar;
    private float WaitToProduce;
    private float ProgressBarNumber=0;
    private float Timer;
    public RealTime realtime;
    public GameObject NewSoulEffect;
    public ChangeCenterAndLeftInfo ChangePanel;
    public GameObject HoverEffect;
    public SoulScoreManager PayCoin;
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
        ProgressBar = ProgressBar.GetComponent<Image>();
        index = 0;
        IsCreatePure = false;
        WaitToProduce = realtime.SecondsInAFullDay;
    }

    // Update is called once per frame
    void Update()
    {
        MouseSelect();
        WaitToProduce = realtime.SecondsInAFullDay;

        switch (index)
        {
            case 0:
                ProfileImage.gameObject.SetActive(false);
                PressPure = false;
                ProgressBarNumber = 0;
                break;
            case 1:
                ProfileImage.gameObject.SetActive(true);                
                //Debug.Log("first block");                
                if (!IsCreatePure)
                {

                    ProfileImage.sprite = PureSoulImage;
                    ProfileImage1.sprite = BackgroundImage;
                    ProfileImage2.sprite = BackgroundImage;
                    StartCoroutine(BeginPureCount());
                }
                break;
            case 2: 
                Debug.Log("second block");
                if (IsCreatePure)
                {
                    ProfileImage1.sprite = PureSoulImage;
                    if (ProfileImage2.sprite = PureSoulImage)
                    {
                        ProfileImage2.sprite = BackgroundImage;
                    }
                }
                else
                {
                    ProfileImage.gameObject.SetActive(true);
                    ProfileImage.sprite = PureSoulImage;
                    StartCoroutine(BeginPureCount());

                }
                break;
            case 3:
                if (IsCreatePure)
                {
                    ProfileImage2.sprite = PureSoulImage;
                }
                Debug.Log("third block");
                break;
            case 4:
                if (IsCreatePure)
                {
                    index += 0;
                }

                break;
        }
        ProgressBar.fillAmount = ProgressBarNumber * 20f / 100f;
    }
   
    public void OnMouseDown()
    {
        FindObjectOfType<AudioManager>().Play("click");
        selected = true;
        justSelected = true;
        MainBuildInfo = true;
        ChangePanel.BuildingLevelInfo = 1;
    }
    void OnMouseOver()
    {
        hover = true;
        HoverEffect.gameObject.SetActive(true);
    }
    void OnMouseExit()
    {
        hover = false;
        HoverEffect.gameObject.SetActive(false);
    }
    public void MorePureSoul()
    {
        if (PayCoin.HellCoin > 0)
        {
            FindObjectOfType<AudioManager>().Play("click");
            index += 1;
            PayCoin.HellCoin -= 1;
        }
    }
    private void MouseSelect()
    {
        if (Input.GetMouseButtonDown(1))
        {
            MainBuildInfo = false;
            ChangePanel.BuildingLevelInfo = 0;
        }
    }
    IEnumerator BeginPureCount()
    {
        //Debug.Log("123");
        IsCreatePure = true;
        yield return new WaitForSeconds(WaitToProduce/5);
        ProgressBarLoading();
        //Debug.Log("456");
        yield return new WaitForSeconds(WaitToProduce/5);
        ProgressBarLoading();
        yield return new WaitForSeconds(WaitToProduce/5);
        ProgressBarLoading();
        yield return new WaitForSeconds(WaitToProduce/5);
        ProgressBarLoading();
        yield return new WaitForSeconds(WaitToProduce/5);
        index -= 1;
        int SoulIndex = Random.Range(0,Souls.Length);
        Instantiate(Souls[SoulIndex], NewSoulShowUpBox.position, NewSoulShowUpBox.rotation);
        Vector3 NewSoulShowUpPosition = new Vector3(NewSoulShowUpBox.position.x, NewSoulShowUpBox.position.y, NewSoulShowUpBox.position.z);

        Destroy(Instantiate(NewSoulEffect, NewSoulShowUpBox.position, Quaternion.Euler(-90f, 0f, 0f)), 5f);
        ProfileImage.gameObject.SetActive(false);
        IsCreatePure = false;
        ProgressBarLoading();
        //Debug.Log("456");
    }
    public void ProgressBarLoading()
    {
        Timer += WaitToProduce;
        Debug.Log(Timer);
        if (Timer >= WaitToProduce)
        {
            Timer = 0;
            ProgressBarNumber += 1f;
            if (ProgressBarNumber>=5)
            {
                ProgressBarNumber = 0;
            }
        }              
    }
}   
