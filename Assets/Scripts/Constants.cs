using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour {

    public static Constants constants;

    public string dbUrl = "46.101.97.178";
    public string getUserUrl = "/user";
    public string getAllPlanesUrl = "/planes";
    public string getPlaneUrl = "/plane";
    public string getPilotUrl = "/pilot";
    public string getWeaponUrl = "/weapon";

    public static float delta = (1.0f/60.0f);
	public static int framesPerCombatRound = 60;
    public static float navigationConstant = 3.0f;

    void Awake()
    {
        if (constants == null)
        {
            constants = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
