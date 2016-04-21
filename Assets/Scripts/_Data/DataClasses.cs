using SimpleJSON;
using System.Collections.Generic;

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
}
