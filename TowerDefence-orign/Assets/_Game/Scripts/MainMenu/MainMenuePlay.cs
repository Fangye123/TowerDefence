using UnityEngine;
using System.Collections;

public class MainMenuePlay : MonoBehaviour {

	public GameObject main;
	public GameObject story;
	public GameObject option;

    // Use this for initialization
	void Start () {
		main.SetActive(true);
		option.SetActive(false);
		story.SetActive(false);
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
		story.SetActive(true);
		
	}
}
