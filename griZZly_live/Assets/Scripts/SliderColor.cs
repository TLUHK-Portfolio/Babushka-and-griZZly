using UnityEngine;
using UnityEngine.UI;

public class SliderColor : MonoBehaviour {
    public Gradient gradient;
    private Slider slider;
    private Image sliderFill; //connected the Image Fill from the slider

    public void Start() {
        slider = gameObject.GetComponent<Slider>();
        sliderFill = slider.fillRect.GetComponent<Image>();
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck() {
        Debug.Log(slider.value);
        sliderFill.color = ColorFromGradient(slider.value);
    }

    Color ColorFromGradient(float value) // float between 0-1
    {
        return gradient.Evaluate(value);
    }
}