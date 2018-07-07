using UnityEngine;
using System.Collections;

public class MainMenuOptioins : MonoBehaviour {
	public GameObject main;
	public GameObject option;
	
	void Start(){
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
		option.SetActive(true);	
	}
}
