using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderControl : MonoBehaviour {

	public Text yawText;
	public Text pitchText;
	public Text rollText;
	public Text speedText;

	public Slider yawSlider;
	public Slider pitchSlider;
	public Slider rollSlider;
	public Slider speedSlider;

	public KolControl kol;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		yawText.text = "YAW("+yawSlider.value.ToString()+")";
		pitchText.text = "PITCH("+pitchSlider.value.ToString()+")";
		rollText.text = "ROLL("+rollSlider.value.ToString()+")";
		speedText.text = "SPEED("+speedSlider.value.ToString()+")";
	}
}
