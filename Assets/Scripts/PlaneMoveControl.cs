using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlaneMoveControl : MonoBehaviour {
	
	public float airSpeed;
	public float altitude;

	public float stallSpeed;
	public float maxSpeed;
	public float accelerationRate;
	public float decelerationRate;
	public float climbSpeedLoss;
	public float diveSpeedGain;
	public float turnRate;

	public float turnEfficiencyMultiplier;

	public float xRot;	//pitch
	public float yRot;	//yaw
	public float zRot;	//roll

	public Slider yawSlider;
	public Slider pitchSlider;
	public Slider rollSlider;
	public Slider speedSlider;

	public KolControl kol;

	public SliderControl sliderControl;

	public bool playing;

	public GameObject fakePlane;
	public LineRenderer lr;
	public Vector3[] points;

	public float delta;
	public int count;

	public int playingFrames;

	private Vector3 previousPoint;	//Point at which the plane was in at the previous frame

	public List<Dictionary<string,float>> commands = new List<Dictionary<string, float>>();
	public List<Vector3> positions = new List<Vector3>();

	public bool useEff;

	public AudioClip brrrt;
	public GameObject brrrtParticle;

	public void Commit()
	{
		//StartCoroutine(TogglePlay());

		playing = true;

		playingFrames = 60;

		Dictionary<string, float> commandThisTurn = new Dictionary<string, float>();

		commandThisTurn.Add("speed",speedSlider.value);
		commandThisTurn.Add("yaw",yawSlider.value);
		commandThisTurn.Add("pitch",pitchSlider.value);
		commandThisTurn.Add("roll",rollSlider.value);

		commands.Add(commandThisTurn);
	}

	public IEnumerator TogglePlay()
	{
		playing = true;

		yawSlider.interactable = false;
		pitchSlider.interactable = false;
		rollSlider.interactable = false;

		yield return new WaitForSeconds(1);

		playing = false;

		yawSlider.interactable = true;
		pitchSlider.interactable = true;
		rollSlider.interactable = true;

		yawSlider.value = 0;
		pitchSlider.value = 0;
		rollSlider.value = 0;
	}

	public void Fire()
	{
		if(brrrtParticle != null)
		{			
			brrrtParticle.SetActive(true);

			Camera.main.GetComponent<AudioSource>().PlayOneShot(brrrt);

			Debug.Log("BRRRRRT"+Time.time);
		}
	}

	public void StopFire()
	{
		if(brrrtParticle != null)
		{
			brrrtParticle.SetActive(false);

			Camera.main.GetComponent<AudioSource>().Stop();
		}
	}

	// Use this for initialization
	void Start () {

		fakePlane = GameObject.Find("FakePlane");
		lr = GameObject.Find("LineRenderer").GetComponent<LineRenderer>();

		sliderControl = GameObject.Find("SliderControl").GetComponent<SliderControl>();

		yawSlider = sliderControl.yawSlider;
		pitchSlider = sliderControl.pitchSlider;
		rollSlider = sliderControl.rollSlider;
		speedSlider = sliderControl.speedSlider;

		kol = sliderControl.kol;

		delta = 0.02f;

		count = 60;

		points = new Vector3[count];

		useEff = true;

	}
	
	// Update is called once per frame
	void Update () {

		altitude = transform.position.y;

		xRot = transform.localEulerAngles.x;
		yRot = transform.localEulerAngles.y;
		zRot = transform.localEulerAngles.z;

		if(playing && playingFrames > 0)
		{
			//find the angle between the ground and the plane

//			Vector3 planeVector = (transform.forward * airSpeed);
//			Vector3 groundVector = (transform.forward.z >= 0) ? Vector3.forward : Vector3.back;
//			float planeVectorLength = planeVector.magnitude;
//			float groundVectorLength = groundVector.magnitude;
//
//			float product = Vector3.Dot(planeVector,groundVector);
//
//			float angle = Mathf.Acos(product / planeVectorLength*groundVectorLength) * Mathf.Rad2Deg;

			float optimalSpeed = (maxSpeed+stallSpeed)/2.0f;

			float turnEfficiency = 1.0f;

			if(useEff)
			{
				if(airSpeed != optimalSpeed)
				{
					turnEfficiency = (Mathf.Abs(airSpeed - optimalSpeed) / ((maxSpeed-stallSpeed)/2.0f)) * turnEfficiencyMultiplier;

					turnEfficiency = 1 - turnEfficiency;

					Debug.Log("EFF:"+turnEfficiency);
				}
			}

			airSpeed += (transform.forward.y >= 0) ? climbSpeedLoss*-1*transform.forward.y*delta : diveSpeedGain*-1*transform.forward.y*delta;

			airSpeed += (speedSlider.value >= 0) ? speedSlider.value/100.0f*accelerationRate*delta: speedSlider.value/100.0f*decelerationRate*delta;

			airSpeed = Mathf.Clamp(airSpeed,0,maxSpeed);

			transform.Rotate(new Vector3(kol.pitch/100.0f * turnRate * turnEfficiency * delta,yawSlider.value/100.0f * turnRate * turnEfficiency * delta, kol.roll/100.0f * turnRate * turnEfficiency * delta));

			transform.position += transform.forward*airSpeed*delta;

			playingFrames--;
		}
		else
		{
			if(PlaneSelector.selectedPlane.gameObject != gameObject)
				return;

			playing = false;

			PlaneSelector.currentMode = PlaneSelector.PlayMode.commandMode;

			StopFire();

			float fakeAirSpeed = airSpeed;

			float optimalSpeed = (maxSpeed+stallSpeed)/2.0f;
			
			float turnEfficiency = 1.0f;



			lr.SetVertexCount(count);

			fakePlane.transform.position = transform.position;
			fakePlane.transform.rotation = transform.rotation;

			for (int i = 0; i < count; i++) {

				fakeAirSpeed += (fakePlane.transform.forward.y >= 0) ? climbSpeedLoss*-1*fakePlane.transform.forward.y*delta : diveSpeedGain*-1*fakePlane.transform.forward.y*delta;

				fakeAirSpeed += (speedSlider.value >= 0) ? speedSlider.value/100.0f*accelerationRate*delta: speedSlider.value/100.0f*decelerationRate*delta;

				fakeAirSpeed = Mathf.Clamp(fakeAirSpeed,0,maxSpeed);

				if(useEff)
				{
					if(fakeAirSpeed != optimalSpeed)
					{
						turnEfficiency = (Mathf.Abs(fakeAirSpeed - optimalSpeed) / ((maxSpeed-stallSpeed)/2.0f)) * turnEfficiencyMultiplier;
						
						turnEfficiency = 1 - turnEfficiency;
					}
				}

				fakePlane.transform.Rotate(new Vector3(kol.pitch/100.0f * turnRate * turnEfficiency * delta,yawSlider.value/100.0f * turnRate * turnEfficiency * delta, kol.roll/100.0f * turnRate * turnEfficiency * delta));
				fakePlane.transform.position += fakePlane.transform.forward * fakeAirSpeed * delta;

				points[i] = fakePlane.transform.position;

				lr.SetPosition(i,points[i]);
			}
		}


	}
}
