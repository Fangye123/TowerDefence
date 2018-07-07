using UnityEngine;
using System;

namespace TowerDefence
{

    public class enemyTK : MonoBehaviour
    {

        public delegate void DelegateDestroy(Enemy enemy);
        public event DelegateDestroy EventDied;
        public event DelegateDestroy EventReachedTarget;

        public GameObject red;
		public GameObject red1;
        public GameObject black;

        private Health health;
        private HitFlash hitFlash;
        private int startingHealth;
        private int maxHP;

		public int Diedtime;
        public GameObject footman;
        public GameObject MainCamera;
		public bool hit=true;
        Animator a;

        bool isSinking;

        float maxSpeed;
        float time;
        int curParkTime = 0;//use to count time



        //===================================================
        // UNITY METHODS
        //===================================================

        /// <summary>
        /// Awake.
        /// </summary>
        void Awake()
        {
            health = GetComponent<Health>();
            hitFlash = GetComponent<HitFlash>();
            startingHealth = health.HealthValue;
            a = footman.GetComponent<Animator>();

        }

        /// <summary>
        /// Start.
        /// </summary>
        void Start()
        {
            maxHP = health.HealthValue;
            MainCamera = GameObject.Find("Camera");
            maxSpeed = this.GetComponent<NavMeshAgent>().speed;
        }

        /// <summary>
        /// Update.
        /// </summary>
        void Update()
        {
            red.GetComponent<Transform>().localScale = new Vector3(health.HealthValue * 60f / maxHP, 5f, 0f);
			red1.GetComponent<Transform>().localScale=new Vector3( health.HealthValue*60f / maxHP,5f,0f);


            if (isSinking)
            {
                transform.Translate(-Vector3.up * Time.deltaTime);
                if (Time.time > time + 1f)
                {

                    time = Time.time;
                    curParkTime++;
                }
                if (curParkTime >= Diedtime)
                {
                    if (footman.tag == "towerkiller")
                    {

                        footman.SetActive(false);
                    }
                   // Died();
                }
            }

        }

        /// <summary>
        /// Called when [enable].
        /// </summary>
        void OnEnable()
        {
            health.SetStartingHealth(startingHealth);
        }

        /// <summary>
        /// Called when a the trigger is fired.
        /// </summary>
        /// <param name="collider">The collider.</param>
        /// 
        public void pointhitted()
        {
            health.Damage(5);
            if (health.HealthValue <= 0)
            {


                a.SetTrigger("Die");
                GetComponent<NavMeshAgent>().enabled = false;
                footman.GetComponent<BoxCollider>().enabled = false;
                isSinking = true;

            }
        }
        void OnTriggerEnter(Collider collider)
        {
            if (collider.tag == "tower" && gameObject.tag == "towerkiller")
            {
                GetComponent<NavMeshAgent>().enabled = false;
                a.SetTrigger("shoot");

            }
            if (collider.tag == "soulfire" && gameObject.tag == "towerkiller")
            {
                pointhitted();
            }
            if (collider.gameObject.tag == Tags.Projectile)
            {

                // apply the projectile damage.
                Projectile projectile = collider.gameObject.transform.parent.gameObject.GetComponent<Projectile>();
                health.Damage(projectile.Damage);
                if (projectile.slow)
                {
                    float t = 0f;
                    int ct = 0;

                    if (Time.time > t + 1f)
                    {

                        t = Time.time;
                        ct++;
                        this.GetComponent<NavMeshAgent>().speed = maxSpeed * 0.3f;
                        //hitFlash.Flash();
                    }
                    if (curParkTime >= 1)
                    {
                        this.GetComponent<NavMeshAgent>().speed = maxSpeed * 0.3f;
                    }
                }

                // destroy the collidee - projectile
                Destroy(collider.gameObject.transform.parent.gameObject);


                // check health
                if (health.HealthValue <= 0)
                {


                    a.SetTrigger("Die");
					hit=false;
                    this.GetComponent<NavMeshAgent>().enabled = false;
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    isSinking = true;
                    //Destroy(footman, 2f);

                    //Died();



                }
            }
            else if (collider.gameObject.tag == Tags.Goal)
            {
                footman.SetActive(false);
                //ReachedTarget();
            }
        }

        //===================================================
        // PUBLIC METHODS
        //===================================================

        /// <summary>
        /// Sets the health value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void SetHealthValue(int value)
        {
            health.SetStartingHealth(value);
        }

        //===================================================
        // PRIVATE METHODS
        //===================================================

        /// <summary>
        /// Called when there is no health.
        /// </summary>

       // private void Died()
      //  {
       //     if (EventDied != null)
        //    {
       //         EventDied(this);
       //     }
      //  }

        /// <summary>
        /// Called when it reacheds the target.
        /// </summary>
        //private void ReachedTarget()
        //{
        //    if (EventReachedTarget != null)
        //    {
        //        EventReachedTarget(this);
        //    }
        //}

        //===================================================
        // EVENTS METHODS
        //===================================================


    }
}