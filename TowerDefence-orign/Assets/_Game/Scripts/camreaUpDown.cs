using UnityEngine;
using System.Collections;

public class camreaUpDown : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * -2, 0, 0));
        //this.gameObject.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * 2, 0));
    }
}
