using UnityEngine;
using System.Collections;

public class ToMainMenu : MonoBehaviour {
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
		main.SetActive(true);
		option.SetActive(false);
	}
}
