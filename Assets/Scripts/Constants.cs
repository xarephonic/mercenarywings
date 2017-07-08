using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Constants : MonoBehaviour {

    public static Constants inst;

	public string dbUrl = "https://mighty-fortress-94429.herokuapp.com/";
	public string localDbUrl = "";
	public string getAllPlanesLocal = "allPlanes";
	public string getAllPlayerPlanesLocal = "playerPlanes";
	public string getUserUrl = "playerPlanes";
    public string getAllPlanesUrl = "allPlanes";
    public string getPlaneUrl = "/plane";
    public string getPilotUrl = "/pilot";
    public string getWeaponUrl = "/weapon";

	public string assetDataUrl = "http://ccsdacademy.com/mercwings/";

	public float timeout = 5.0f;

    public static float delta = (1.0f/60.0f);
	public static int framesPerCombatRound = 60;
    public static float navigationConstant = 3.0f;

	public static float scaleFactor = 1.0f;

	public static float showKmDistance = 2000;

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
