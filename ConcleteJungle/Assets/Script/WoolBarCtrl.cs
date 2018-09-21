using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WoolBarCtrl : MonoBehaviour
{

    Slider _slider;
    void Start()
    {
        // スライダーを取得する
        _slider = GameObject.Find("WoolBar").GetComponent<Slider>();
    }

    float wool = 0;
    void Update()
    {
        wool += 0.01f;
        if (wool > 1)
        {
            wool = 0;
        }
        _slider.value = wool;
    }
}
