using UnityEngine;
using System.Collections;

public class expolon : MonoBehaviour {

    public GameObject effect;
    AudioSource bulletAudio;
    public GameObject effect2;
    void awake()
    {
        
    }
    // Use this for initialization
    void Start () {
        bulletAudio = GameObject.Find("Player").GetComponent<AudioSource>();
        //Destroy(gameObject,5); 
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {

        bulletAudio.Play();
        GameObject firework2 = Instantiate(effect2) as GameObject;
        GameObject firework = Instantiate(effect) as GameObject;
        //AudioSource bu1 = Instantiate(bulletAudio) as AudioSource;
        firework.transform.position = transform.position;
        firework2.transform.position = transform.position;
        //bu1.Play();
        Destroy(gameObject);
        Destroy(firework,7);
        Destroy(firework, 15);
    }
}
