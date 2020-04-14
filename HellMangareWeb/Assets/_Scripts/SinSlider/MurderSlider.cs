using UnityEngine;
using UnityEngine.UI;



public class MurderSlider : MonoBehaviour
{
    public Slider slider;
    public int MurderYear;
    public Gradient gradient;
    public UIManager uiManager;



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
        MurderYear = Mathf.RoundToInt(slider.value);
        uiManager.Murder.text = "" + MurderYear;

    }
    /*public void decrementSlider(float h)
    {
        if (slideractive == false)
        {
            slider.value -= h;
            Debug.Log("newSlider=" + MurderYear);

        }
    }*/
}