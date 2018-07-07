using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pause : MonoBehaviour {
	public GameObject Menu;

	// Use this for initialization
	void Start () {
	
	}
	
	public void Click()
	{
		Menu.SetActive(false);
		Time.timeScale=1;
		Application.LoadLevel(Application.loadedLevel+1);
	}
	
	public void ClickEnd()
	{
		Menu.SetActive(false);
		Time.timeScale=1;
		Application.LoadLevel(0);
	}
	
	public void pause(){
		Menu.SetActive(false);
		Time.timeScale=1;
	}
	
	public void Reset()
	{
		Menu.SetActive(false);
		Time.timeScale=1;
		Application.LoadLevel(Application.loadedLevel);
	}
	
}
