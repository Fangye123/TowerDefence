using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnMouseEnter()
	{
		GetComponent<Renderer>().material.color = Color.blue;
	}
	
	void OnMouseExit()
	{
		GetComponent<Renderer>().material.color = Color.white;
	}
	
		void OnMouseUp()
	{

		Application.LoadLevel(1);
	}
}
