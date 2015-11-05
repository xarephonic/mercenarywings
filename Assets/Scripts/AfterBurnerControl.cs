using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AfterBurnerControl : MonoBehaviour {

	public ParticleSystem[] afterBurners;

	public Slider speedSlider;

	// Use this for initialization
	void Start () {

		speedSlider = GameObject.Find("SliderControl").GetComponent<SliderControl>().speedSlider;
	}
	
	// Update is called once per frame
	void Update () {

		if(PlaneSelector.selectedPlane.gameObject == gameObject)
		{
			foreach(ParticleSystem p in afterBurners)
			{
				p.emissionRate = 160 + speedSlider.value/100.0f * 40;
			}
		}
	}
}
