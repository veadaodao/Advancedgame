using UnityEngine;
using UnityEngine.UI;



public class LustSlider : MonoBehaviour
{
    public Slider slider;
    public int LustYear;
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
        LustYear = Mathf.RoundToInt(slider.value);

        uiManager.Lust.text = "" + LustYear;

    }

    /*public void decrementSlider(float h)
    {
        if (slideractive == false)
        {
            slider.value -= h;
            Debug.Log("newSlider=" + LustYear);

        }
    }*/

}