using UnityEngine;
using System.Collections;

public class MainMenueQuit : MonoBehaviour {

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

		Application.Quit();
	}
}
