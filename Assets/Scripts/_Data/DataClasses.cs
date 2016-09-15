using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;

namespace DataClasses
{
    [System.Serializable]
    public class PlaneVO
    {
        public int id;
        public string name;

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

        public int hitPoints;
        public int hardPoints;
        public float cannonFireRate;
        public float cannonAccuracy;
        public float cannonDamage;

        public int counterMeasuresCount;
        public float radarEcmStrength;
        public float infraredEcmStrength;
        public float radarTrackingStrength;
        public float infraredTrackingStrength;

        public PlaneVO()
        {

        }

        public static PlaneVO FromJson(JSONClass jClass)
        {
            PlaneVO pvo = new PlaneVO(jClass["id"].AsInt, jClass["name"], jClass["stallSpeed"].AsFloat, jClass["maxSpeed"].AsFloat,
                                         jClass["accelerationRate"].AsFloat, jClass["decelerationRate"].AsFloat, jClass["climbSpeedLoss"].AsFloat,
                                         jClass["diveSpeedGain"].AsFloat, jClass["yawRate"].AsFloat, jClass["pitchRate"].AsFloat,
                                         jClass["rollRate"].AsFloat, jClass["turnEfficiencyMultiplier"].AsFloat, jClass["hitPoints"].AsInt,
                                         jClass["hardPoints"].AsInt, jClass["cannonFireRate"].AsFloat, jClass["cannonAccuracy"].AsFloat,
                                         jClass["cannonDamage"].AsFloat, jClass["counterMeasuresCount"].AsInt, jClass["radarEcmStrength"].AsFloat,
                                         jClass["infraredEcmStrength"].AsFloat, jClass["radarTrackingStrength"].AsFloat,
                                         jClass["infraredTrackingStrength"].AsFloat);

            return pvo;
        }

		public void ToPlane(GameObject plane){
			AircraftCore core = plane.GetComponent<AircraftCore>();
			core.aircraftId = this.id;
			core.aircraftName = this.name;

			MovementModule move = plane.GetComponent<MovementModule>();
			move.stallSpeed = this.stallSpeed;
			move.maxSpeed = this.maxSpeed;
			move.accelerationRate = this.accelerationRate;
			move.decelerationRate = this.decelerationRate;
			move.climbSpeedLoss = this.climbSpeedLoss;
			move.diveSpeedGain = this.diveSpeedGain;
			move.yawRate = this.yawRate;
			move.pitchRate = this.pitchRate;
			move.rollRate = this.rollRate;
			move.turnEfficiencyMultiplier = this.turnEfficiencyMultiplier;

			HitPointModule hits = plane.GetComponent<HitPointModule>();
			hits.hitPoints = this.hitPoints;
			//TODO find solution for hardpoints
			//TODO find solution for cannon

			EcmModule ecm = plane.GetComponent<EcmModule>();
			ecm.counterMeasuresCount = this.counterMeasuresCount;
			ecm.radarEcmStrength = this.radarEcmStrength;
			ecm.infraredEcmStrength = this.infraredEcmStrength;

			TrackingModule tracking = plane.GetComponent<TrackingModule>();
			tracking.sensorStrength = this.radarTrackingStrength;

			AircraftFireControl fire = plane.GetComponent<AircraftFireControl>();
			fire.cannonAccuracy = cannonAccuracy;
			fire.cannonFireRate = cannonFireRate;
			fire.cannonDmg = cannonDamage;
		}

        public PlaneVO (int planeId,string planeName, float stallSpeed, float maxSpeed, float accRate, float decRate, float climbLoss,
                    float diveGain, float yaw, float pitch, float roll, float turnEff, int hp, int hardPoints, float cannonFireRate,
                    float cannonAcc, float cannonDmg, int counterMeasuresCount, float radarEcm, float infraredEcm, float radarTrack,
                    float infraredTrack)
        {
            this.id = planeId;
            this.name = planeName;
            this.stallSpeed = stallSpeed;
            this.maxSpeed = maxSpeed;
            this.accelerationRate = accRate;
            this.decelerationRate = decRate;
            this.climbSpeedLoss = climbLoss;
            this.diveSpeedGain = diveGain;
            this.yawRate = yaw;
            this.pitchRate = pitch;
            this.rollRate = roll;
            this.turnEfficiencyMultiplier = turnEff;
            this.hitPoints = hp;
            this.hardPoints = hardPoints;
            this.cannonFireRate = cannonFireRate;
            this.cannonAccuracy = cannonAcc;
            this.cannonDamage = cannonDmg;
            this.counterMeasuresCount = counterMeasuresCount;
            this.radarEcmStrength = radarEcm;
            this.infraredEcmStrength = infraredEcm;
            this.radarTrackingStrength = radarTrack;
            this.infraredTrackingStrength = infraredTrack;
        }
    }

	[System.Serializable]
	public class MissionVO {
		public int id;
		public string name;

		public MissionType missionType;

		public int numberOfWaves;

		public MissionVO () {
			
		}

		public static MissionVO FromJson (JSONClass jClass) {
			MissionVO mission = new MissionVO(jClass["id"].AsInt, jClass["name"], (MissionType)jClass["type"].AsInt, jClass["waves"].AsInt);

			return mission;			
		}

		public MissionVO (int id, string name, MissionType missionType, int numberOfWaves) {
			this.id = id;
			this.name = name;
			this.missionType = missionType;
			this.numberOfWaves = numberOfWaves;
		}
	}

	public enum MissionType {
		ESCORT,
		DESTROY,
		PROTECT
	}
}
