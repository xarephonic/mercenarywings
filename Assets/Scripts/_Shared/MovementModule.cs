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

	private float tanBot;
	private float tanTop;

	public bool hasCommands(){
		if(commands.Count == TurnManager.currentTurn)
			return true;
		else
			return false;
	}

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

	public void SetCommandsForThisTurn(float speed,float yaw,float pitch,float roll,int turnNumber)
	{
		Dictionary<string,float> commandsThisTurn = new Dictionary<string, float>();
		
		commandsThisTurn.Add("speed",speed);
		commandsThisTurn.Add("yaw",yaw);
		commandsThisTurn.Add("pitch",pitch);
		commandsThisTurn.Add("roll",roll);

		if(commands.Count < turnNumber)
		{
			commands.Add(commandsThisTurn);
		}else {
		commands[turnNumber-1]= commandsThisTurn;
		}

	}

	public Dictionary<string,float> GetCommandsForThisTurn() {
		if(commands.Count > 0)
			return commands[commands.Count-1];
		else 
			return new Dictionary<string, float>();
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

		airSpeed += (transform.forward.y >= 0) ? climbSpeedLoss*-1*3.6f*transform.forward.y*Constants.delta : diveSpeedGain*-1*3.6f*transform.forward.y*Constants.delta;
		
		airSpeed += (s >= 0) ? s/100.0f*accelerationRate*3.6f*Constants.delta: s/100.0f*decelerationRate*3.6f*Constants.delta;
		
		airSpeed = Mathf.Clamp(airSpeed,1,maxSpeed);

		float bankRotation = 0.0f;

		float v = airSpeed/3.6f;
		float v2 = airSpeed*airSpeed;
		float bankDeg = transform.eulerAngles.z % 180;
		/*
		if(bankDeg < 91 && bankDeg > 89) {
			bankDeg = (bankDeg >=90) ? 91 : 89;
		}
		*/

		if(Mathf.Abs(bankDeg) > 1){

			float tan = 0.0f;

			if(bankDeg < 91 && bankDeg > 89){
				float perc = (bankDeg - 89) / 2;
				tan = Mathf.Lerp(tanBot,tanTop,perc);
			}else {
				tan = Mathf.Tan(bankDeg*Mathf.Deg2Rad);
			}

			float radius = v2 / (tan*9.82f);
			float bankRotationTime = (2*radius*Mathf.PI) / v;
			bankRotation = Mathf.Sqrt(Mathf.Abs(360.0f / bankRotationTime));
		}

		//transform.Rotate(new Vector3(p/100.0f * pitchRate * turnEfficiency * Constants.delta, r/100.0f * yawRate * turnEfficiency * Constants.delta, r/100.0f * -rollRate * turnEfficiency * Constants.delta));

		//transform.RotateAround(transform.position, Vector3.right, p/100.0f * pitchRate * Constants.delta);

		//transform.RotateAround(transform.position, transform.forward, -1 * r/100.0f * rollRate * Constants.delta);

		//transform.Rotate(new Vector3(0, 0, -1 * r/100.0f * rollRate * Constants.delta));

		bankRotation *= (transform.eulerAngles.z > 180) ? 1 : -1;

		//transform.RotateAround(transform.position,Vector3.up,bankRotation);

		transform.Rotate(new Vector3(p/100.0f * pitchRate * Constants.delta, bankRotation * Constants.delta, -1 * r/100.0f * rollRate * Constants.delta));

		transform.position += transform.forward*airSpeed/3.6f*Constants.delta;
	}

	void Start(){

		//these two are constants to interpolate between 89 and 91 degree banks' rotation rates

		tanBot = Mathf.Abs(Mathf.Tan(89 * Mathf.Deg2Rad));
		tanTop = Mathf.Abs(Mathf.Tan(91 * Mathf.Deg2Rad));
	}

	void Update(){

		if(SceneStateManager.currentState == SceneStateManager.CombatSceneState.MOVEMENT)
			ExecuteMovement();
	}
}
