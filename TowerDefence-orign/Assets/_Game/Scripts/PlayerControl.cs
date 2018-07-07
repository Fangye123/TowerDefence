using UnityEngine;
using System.Collections;

namespace TowerDefence {

public class PlayerControl : MonoBehaviour {
	int MoveSpeed=3;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.Rotate( new Vector3(0,Input.GetAxis("Mouse X")*3,0)); 
		if(Input.GetKey(KeyCode.W)){
			transform.Translate(Vector3.forward*MoveSpeed);
			this.GetComponent<Animation>().Play("Run");
		}
		else if(Input.GetKey(KeyCode.S)){
			transform.Translate(Vector3.forward*-MoveSpeed);
			this.GetComponent<Animation>().Play("Run");
		}
		else if(Input.GetKey(KeyCode.A)){
			transform.Translate(Vector3.left*MoveSpeed);
			this.GetComponent<Animation>().Play("Run");
		}
		else if(Input.GetKey(KeyCode.D)){
			transform.Translate(Vector3.right*MoveSpeed);
			this.GetComponent<Animation>().Play("Run");
		}
		else{
			this.GetComponent<Animation>().Play("Stand");
		}
	}
}
}