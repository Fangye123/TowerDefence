using UnityEngine;
using System.Collections;

public class MainMenuTutorial : MonoBehaviour {
	public GameObject main;
	public GameObject tutorial;

	// Use this for initialization
	void Start () {
		tutorial.SetActive(false);
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

		main.SetActive(false);
		tutorial.SetActive(true);
	}
}
