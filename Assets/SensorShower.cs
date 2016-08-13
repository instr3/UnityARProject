using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SensorShower : MonoBehaviour
{
    private RectTransform XButton, YButton, ZButton;
    private Text XText, YText, ZText;
    private float height = 18;
    [HideInInspector]
    public Vector3 Value = new Vector3(-0.5f, 0.333333f, 1f);
    // Use this for initialization
    void Start()
    {
        Text XLabel, YLabel, ZLabel;
        XLabel = transform.FindChild("XLabel").GetComponent<Text>();
        YLabel = transform.FindChild("YLabel").GetComponent<Text>();
        ZLabel = transform.FindChild("ZLabel").GetComponent<Text>();
        XButton = XLabel.GetComponentInChildren<Button>().GetComponent<RectTransform>();
        YButton = YLabel.GetComponentInChildren<Button>().GetComponent<RectTransform>();
        ZButton = ZLabel.GetComponentInChildren<Button>().GetComponent<RectTransform>();
        XText = XButton.GetComponentInChildren<Text>();
        YText = YButton.GetComponentInChildren<Text>();
        ZText = ZButton.GetComponentInChildren<Text>();
    }
    void SetSize(RectTransform rect,float x)
    {
        float length = 40f * x + 60f;
        rect.sizeDelta = new Vector2(length, height);
        rect.localPosition = new Vector3(length / 2 + 24, 0, 0);
    }
    public void SetTitle(string text)
    {
        GetComponent<Text>().text = text;
    }
    // Update is called once per frame
    void Update()
    {
        XText.text = Value.x.ToString("F2");
        YText.text = Value.y.ToString("F2");
        ZText.text = Value.z.ToString("F2");
        SetSize(XButton, Value.x);
        SetSize(YButton, Value.y);
        SetSize(ZButton, Value.z);
        Vector3 test = 40f * Value + new Vector3(60f, 60f, 60f);
        XButton.sizeDelta = new Vector2(Mathf.Clamp(test.x, 20f, 100f), height);
        YButton.sizeDelta = new Vector2(Mathf.Clamp(test.y, 20f, 100f), height);
        ZButton.sizeDelta = new Vector2(Mathf.Clamp(test.z, 20f, 100f), height);
    }
}
