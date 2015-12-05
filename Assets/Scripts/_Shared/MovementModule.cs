using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MovementModule : MonoBehaviour {

	public float airSpeed;
	public float altitude;
	
	public float stallSpeed;
	public float maxSpeed;
	public float accelerationRate;
	public float decelerationRate;
	public float climbSpeedLoss;
	public float diveSpeedGain;

	public float yawRate;
	public float pitchRate;
	public float rollRate;
	
	public float turnEfficiencyMultiplier;

	public List<Dictionary<string,float>> commands = new List<Dictionary<string, float>>();

	public float GetOptimalSpeed()
	{
		float ret = (stallSpeed+maxSpeed)*0.75f;

		return ret;
	}

	public void SetCommandsForThisTurn(float speed,float yaw,float pitch,float roll)
	{
		Dictionary<string,float> commandsThisTurn = new Dictionary<string, float>();

		commandsThisTurn.Add("speed",speed);
		commandsThisTurn.Add("yaw",yaw);
		commandsThisTurn.Add("pitch",pitch);
		commandsThisTurn.Add("roll",roll);

		commands.Add(commandsThisTurn);
	}

	public void ExecuteMovement()
	{
		float s = commands[commands.Count-1]["speed"];
		float y = commands[commands.Count-1]["yaw"];
		float p = commands[commands.Count-1]["pitch"];
		float r = commands[commands.Count-1]["roll"];

		float optimalSpeed = (maxSpeed+stallSpeed)*0.75f;
		
		float turnEfficiency = 1.0f;

		turnEfficiency = (Mathf.Abs(airSpeed - optimalSpeed) / ((maxSpeed-stallSpeed)/2.0f)) * turnEfficiencyMultiplier;
		
		turnEfficiency = 1 - turnEfficiency;

		//Debug.Log(gameObject.name + "turneff: "+turnEfficiency);

		airSpeed += (transform.forward.y >= 0) ? climbSpeedLoss*-1*transform.forward.y*Constants.delta : diveSpeedGain*-1*transform.forward.y*Constants.delta;
		
		airSpeed += (s >= 0) ? s/100.0f*accelerationRate*Constants.delta: s/100.0f*decelerationRate*Constants.delta;
		
		airSpeed = Mathf.Clamp(airSpeed,0,maxSpeed);

		transform.Rotate(new Vector3(p/100.0f * pitchRate * turnEfficiency * Constants.delta,y/100.0f * yawRate * turnEfficiency * Constants.delta, r/100.0f * rollRate * turnEfficiency * Constants.delta));
		
		transform.position += transform.forward*airSpeed/3.6f*Constants.delta;
	}
}
