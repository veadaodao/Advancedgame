using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPCTutorialThree : MonoBehaviour
{
    private bool selected = false;
    private bool hover = false;
    public bool BeginDialogueThree = false;
    float DoubleClickTime = 0;
    public GameObject NPCPanel;
    public GameObject HoverEffect;
    public Image MissionMark;
    public UnitInfoTutorial unitTutorial;
    // Start is called before the first frame update
    void Start()
    {
        DoubleClickTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Unselected();

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
    public void OnMouseDown()
    {
        FindObjectOfType<AudioManager>().Play("click");
        selected = true;

        if (Time.time - DoubleClickTime <= 0.3f)
        {
            BeginDialogueThree = true;
            NPCPanel.gameObject.SetActive(true);
            MissionMark.gameObject.SetActive(false);
        }
        DoubleClickTime = Time.time;
    }
    public void Unselected()
    {
        if (Input.GetMouseButtonDown(1))
        {
            selected = false;
            BeginDialogueThree = false;
            NPCPanel.gameObject.SetActive(false);
        }
    }
}
