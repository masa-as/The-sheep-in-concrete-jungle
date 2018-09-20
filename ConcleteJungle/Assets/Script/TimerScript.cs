using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

	float time;
	Text text;

	void Start () {	
		time = 99;
		text = GetComponent<Text>();
    }

	void Update () {
		time += Time.deltaTime;//毎フレームの時間を加算.
		int minute = (int)time/60;//分.timeを60で割った値.
		int second = (int)time;//秒.timeを60で割った余り.
		int msecond = (int)(time*100%100);
		string minText, secText, msecText;//分・秒を用意.

        if (second < 10)
            secText = "00" + second.ToString();
        else if (second < 100)
            secText = "0" + second.ToString();
		else
			secText = second.ToString();

		if (msecond < 10)
			msecText = "0" + msecond.ToString ();

        else 
			msecText = msecond.ToString ();
		

		text.text = "[Time] " + secText + ":" + msecText ;

		Debug.Log (time);
	}
}