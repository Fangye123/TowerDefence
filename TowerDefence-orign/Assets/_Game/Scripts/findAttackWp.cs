using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace TowerDefence
{
    public class findAttackWp : MonoBehaviour
    {
        public GameObject bullet;
        //public AudioSource throww;

        void start()
        {
            //bullet = GameObject.Find("bullet1") as GameObject;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //throww.Play();
                GameObject projectile = Instantiate(bullet) as GameObject;
                projectile.transform.position = transform.position+ new Vector3 (0,12,0)  + transform.GetChild(0).gameObject.transform.forward*20;
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                rb.velocity = transform.GetChild(0).gameObject.transform.forward *300;
                projectile.GetComponent<AudioSource>().Play();
                Destroy(projectile,5);
            }
        }

       
    }
}
