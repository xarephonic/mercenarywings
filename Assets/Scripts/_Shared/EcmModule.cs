using UnityEngine;
using System.Collections;

public class EcmModule : MonoBehaviour {

	public enum CounterMeasureType
	{
		flare,
		chaff
	}

	public int counterMeasuresCount;
	public int flareCount;
	public int chaffCount;

	public float radarEcmStrength;
	public float infraredEcmStrength;

	public void FireFlare()
	{
		flareCount--;
	}

	public void FireChaff()
	{
		chaffCount--;
	}
}
