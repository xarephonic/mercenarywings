using UnityEngine;
using System.Collections;

public class MovementEstimator : MonoBehaviour {

	public SliderControl sliderControl;

	public MovementModule movementModule;
	public LineRenderer estimationLine;

	private int m_playingFrames;

	public void ImitateMovementModule()
	{
		//this is very ugly. you may want to find a fix to this. be careful not to shallow copy!!!

		MovementModule selectedTargetMovementModule = CombatSelectionHandler.selectedObject.GetComponent<MovementModule>();

		movementModule.airSpeed = selectedTargetMovementModule.airSpeed;
		movementModule.altitude = selectedTargetMovementModule.altitude;
		movementModule.stallSpeed = selectedTargetMovementModule.stallSpeed;
		movementModule.maxSpeed = selectedTargetMovementModule.maxSpeed;
		movementModule.accelerationRate = selectedTargetMovementModule.accelerationRate;
		movementModule.decelerationRate = selectedTargetMovementModule.decelerationRate;
		movementModule.climbSpeedLoss = selectedTargetMovementModule.climbSpeedLoss;
		movementModule.diveSpeedGain = selectedTargetMovementModule.diveSpeedGain;
		movementModule.turnRate = selectedTargetMovementModule.turnRate;
		movementModule.turnEfficiencyMultiplier = selectedTargetMovementModule.turnEfficiencyMultiplier;

		EstimateMovementForGivenFrames(m_playingFrames);
	}

	public void EstimateMovementForGivenFrames(int amountOfFramesToBeEstimated)
	{
		transform.position = CombatSelectionHandler.selectedObject.transform.position;
		transform.eulerAngles = CombatSelectionHandler.selectedObject.transform.eulerAngles;

		movementModule.SetCommandsForThisTurn(sliderControl.speedSlider.value,sliderControl.yawSlider.value,sliderControl.joystick.pitch,sliderControl.joystick.roll);

		for (int i = 0; i < amountOfFramesToBeEstimated; i++) {

			estimationLine.SetPosition(i,transform.position);

			movementModule.ExecuteMovement();
		}
	}

	// Use this for initialization
	void Awake () {
		m_playingFrames = Constants.framesPerCombatRound;

		movementModule = GetComponent<MovementModule>();

		estimationLine.SetVertexCount(m_playingFrames);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
