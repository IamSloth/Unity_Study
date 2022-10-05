using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlModel : MonoBehaviour
{
    private Slider scaleSlider;
    private Slider rotateSlider;

    public float scaleMinValue;
    public float scaleMaxValue;

    public float rotateMinValue;
    public float rotateMaxValue;



    // Start is called before the first frame update
    void Start()
    {
        scaleSlider = GameObject.Find("ScaleSlider").GetComponent<Slider>();
        scaleSlider.minValue = scaleMinValue;
        scaleSlider.maxValue = scaleMaxValue;
        scaleSlider.onValueChanged.AddListener(ScaleSliderUpdate);

        rotateSlider = GameObject.Find("RotateSlider").GetComponent<Slider>();
        rotateSlider.minValue = rotateMinValue;
        rotateSlider.maxValue = rotateMaxValue;
        rotateSlider.onValueChanged.AddListener(RotateSliderUpdate);
    }

    void ScaleSliderUpdate(float value)
    {
        transform.localScale = new Vector3(value, value, value);
    }

    void RotateSliderUpdate(float value)
    {
        transform.localEulerAngles = new Vector3(transform.rotation.x, value, transform.rotation.z);
    }

    // Update is called once per frame
    void Update()
    {

    }



}
