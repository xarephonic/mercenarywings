using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class SliderControl : MonoBehaviour {

	public Text yawText;
	public Text speedText;

	public Slider yawSlider;
	public Slider speedSlider;

	public JoystickControl joystick;

	public float[] currentControlValues = new float[4];
	public float[] oldControlValues = new float[4];
	public UnityEvent OnAircraftControlsChanged;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		currentControlValues[0] = speedSlider.value;
		currentControlValues[1] = yawSlider.value;
		currentControlValues[2] = joystick.pitch;
		currentControlValues[3] = joystick.roll;

		for (int i = 0; i < oldControlValues.Length; i++) {
			if(oldControlValues[i] != currentControlValues[i])
			{
				OnAircraftControlsChanged.Invoke();
				break;
			}
		}

		for (int i = 0; i < oldControlValues.Length; i++) {
			oldControlValues[i] = currentControlValues[i];
		}

		yawText.text = "YAW("+yawSlider.value.ToString()+")";
		speedText.text = "SPEED("+speedSlider.value.ToString()+")";
	}
}
