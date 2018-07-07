using UnityEngine;
using System.Collections;

namespace TowerDefence
{
    public class ro : MonoBehaviour
    {

        //public GameObject effect;
        // Use this for initialization
        public GameObject enemy;
        Enemy en;

        void Start()
        {
            en=enemy.GetComponent<Enemy>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(new Vector3(15, 0, 45) * Time.deltaTime);
        }

        void OnTriggerEnter(Collider other)
        {

            en.pointhitted();
            //GameObject firework = Instantiate(effect) as GameObject;

            //firework.transform.position = transform.position;

            Destroy(gameObject);
            //Destroy(firework, 7);
        }
    }
}