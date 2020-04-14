using UnityEngine;
using UnityEngine.UI;



public class StealSlider : MonoBehaviour
{
    public Slider slider;
    public int StealYear;
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
        StealYear = Mathf.RoundToInt(slider.value);
        uiManager.Steal.text = "" + StealYear;

    }
    /*public void decrementSlider(float h)
    {
        if (slideractive == false)
        {
            slider.value -= h;
            Debug.Log("newSlider=" + StealYear);

        }
    }*/

}