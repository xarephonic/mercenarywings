using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Constants : MonoBehaviour {

    public static Constants inst;

    public string dbUrl = "46.101.97.178";
	public string localDbUrl = "";
	public string getAllPlanesLocal = "allPlanes";
	public string getAllPlayerPlanesLocal = "playerPlanes";
    public string getUserUrl = "/user";
    public string getAllPlanesUrl = "/allPlanes";
    public string getPlaneUrl = "/plane";
    public string getPilotUrl = "/pilot";
    public string getWeaponUrl = "/weapon";
	public float timeout = 5.0f;

    public static float delta = (1.0f/60.0f);
	public static int framesPerCombatRound = 60;
    public static float navigationConstant = 3.0f;

	public Text text;

    void Awake()
    {
        if (inst == null)
        {
            inst = this;
			localDbUrl = Application.persistentDataPath;
			text.text = localDbUrl;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
