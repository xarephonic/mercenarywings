using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class SliderControl : MonoBehaviour {

	public Text yawText;
	public Text speedText;

	public Slider yawSlider;
	public Slider speedSlider;

	public JoystickControl joystick;

	public float[] currentControlValues = new float[4];
	public float[] oldControlValues = new float[4];

	public UnityEvent OnAircraftControlsChanged;

	public void HandleSelectedAircraftChange(){

		MovementModule mov = PlayerPlaneSelectionHandler.selectedPlane.GetComponent<MovementModule>();

		if(mov.commands.Count == TurnManager.currentTurn){
			SetControlsPosition(mov.commands[TurnManager.currentTurn-1]["speed"],
			                    mov.commands[TurnManager.currentTurn-1]["yaw"],
			                    mov.commands[TurnManager.currentTurn-1]["pitch"],
			                    mov.commands[TurnManager.currentTurn-1]["roll"]);
		}
		else
		{
			SetControlsPosition(0,0,0,0);
		}
	}

	public void SetControlsPosition(float speed, float yaw,float pitch,float roll){
		speedSlider.value = speed;
		yawSlider.value = yaw;

		joystick.SetJoystickPositionAccordingToValues(pitch,roll);
	}

	public void Commit(){
		PlayerPlaneSelectionHandler.selectedPlane.GetComponent<MovementModule>().SetCommandsForThisTurn(speedSlider.value,yawSlider.value,joystick.pitch,joystick.roll,TurnManager.currentTurn);


	}

	public void SetSlidersInteractable(bool b){
		speedSlider.interactable = b;
		yawSlider.interactable = b;

		joystick.interactable = b;
	}

	// Use this for initialization
	void Start () {
		PlayerPlaneSelectionHandler.OnSelectedPlaneChanged += delegate(GameObject newSelectedPlane) {

			Dictionary<string,float> commands = newSelectedPlane.GetComponent<MovementModule>().GetCommandsForThisTurn();

			if(commands.Count > 0)
				speedSlider.value = commands["speed"];
		};
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
