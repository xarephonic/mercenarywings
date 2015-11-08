using UnityEngine;
using System.Collections;

public class TestTargetMovement : MonoBehaviour {

	public LineRenderer headingRenderer;

	// Use this for initialization
	void Start () {

		headingRenderer = GetComponentInChildren<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

		GetComponent<MovementModule>().SetCommandsForThisTurn(0,0,0,0);

		GetComponent<MovementModule>().ExecuteMovement();

		headingRenderer.SetPosition(0,transform.position);
		headingRenderer.SetPosition(1,transform.position+transform.forward*100);
	}
}
