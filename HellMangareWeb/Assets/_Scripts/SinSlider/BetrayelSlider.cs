using UnityEngine;
using UnityEngine.UI;



public class BetrayelSlider: MonoBehaviour
{
    public Slider slider;
    public int BetrayelYear;
    public Gradient gradient;
    public UIManager uiManager;
    //public bool slideractive = true;

    void Start()
    {
        GameObject UIObject = GameObject.Find("UIManager");
        uiManager = UIObject.GetComponent<UIManager>();

    }
    public void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate { OnSliderWasChanged(); });
        OnSliderWasChanged();

    }


    public void OnSliderWasChanged()
    {
        BetrayelYear = Mathf.RoundToInt(slider.value);
        uiManager.Betrayel.text = "" + BetrayelYear;

    }
    /*public void decrementSlider(float h)
    {
        if (slideractive == false)
        {
            slider.value -= h;
            Debug.Log("newSlider=" + BetrayelYear);

        }
    }*/

}