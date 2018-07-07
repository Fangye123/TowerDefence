using UnityEngine;
using System.Collections;
 
namespace TowerDefence {
	public class ChangeMode : MonoBehaviour {
		public GameObject MainCamera;
		public GameObject Player;
		public GameObject PlayerCamera;
		public GameObject aim;
		public bool Change;
		bool isrotation=false;
		float time;
		int curParkTime = 0;
		// Use this for initialization
		void Start () {
			Player=GameObject.Find("Player");
			PlayerCamera=Player.transform.GetChild(0).gameObject;
			MainCamera=GameObject.Find("Camera");
			Player.GetComponent<PlayerControl>().enabled=false;
			Player.GetComponent<Turret>().enabled=true;
			Player.GetComponent<TargetFinder>().enabled=true;
			Player.GetComponent<findAttackWp>().enabled = false;
			Change=false;
			MainCamera.SetActive(true);
			PlayerCamera.SetActive(false);
            aim.SetActive(false);
		}
	
	// Update is called once per frame
		void Update () {
			if(Change){
				MainCamera.SetActive(false);
				PlayerCamera.SetActive(true);
				Player.GetComponent<PlayerControl>().enabled=true;
				Player.GetComponent<Turret>().enabled=false;
				Player.GetComponent<TargetFinder>().enabled=false;
				Player.GetComponent<TargetFinder>().range=0;
				aim.SetActive(true);
				Player.GetComponent<findAttackWp>().enabled = true;
				if(!isrotation)
					Player.transform.rotation = Quaternion.identity;
				if (Time.time > time + 1f)
				{
					
					time = Time.time;
					curParkTime++;
				}
				if (curParkTime >= 2)
				{
					isrotation=true;
				}
			}
			else{
				MainCamera.SetActive(true);
				PlayerCamera.SetActive(false);
				Player.GetComponent<PlayerControl>().enabled=false;
				Player.GetComponent<Turret>().enabled=true;
				Player.GetComponent<TargetFinder>().enabled=true;
				Player.GetComponent<TargetFinder>().range=100;
				aim.SetActive(false);
				Player.GetComponent<findAttackWp>().enabled = false;
				isrotation=false;
				curParkTime=0;
			}
		}
	}
}