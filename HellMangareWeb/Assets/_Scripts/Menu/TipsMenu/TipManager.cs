using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TipManager : MonoBehaviour
{
    public static TipManager instance;
    public Tip[] Tips;
    public TipButton[] TipButtons;
    public Tip activeTip;

    public TextMeshProUGUI TipNameText;
    public TextMeshProUGUI TipDesText;

    public GameObject TipPanel;
   

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
        TipNameText.text = instance.Tips[0].TipName;
        TipDesText.text = instance.Tips[0].TipDes;

    }
    public void Update()
    {
        MouseSelect();
    }

    private void MouseSelect()
    {
        if (Input.GetMouseButtonDown(1) && TipPanel.activeInHierarchy)
        {
            TipPanel.SetActive(false);
        }

    }

  






}