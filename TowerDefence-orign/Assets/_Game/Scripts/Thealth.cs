using UnityEngine;
using System.Collections;

namespace TowerDefence
{
    public class Thealth : MonoBehaviour
    {
        //public float timeBetweenAttacks = 0.5f;
        //public int attackDamage = 10;
        //float timer;
        public int startingHealth = 100;
        public int currentHealth;
        //public Slider healthSlider;
        //public Image damageImage;
        //public AudioClip deathClip;
        public float flashSpeed = 5f;
        //public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
        //public 
        public GameObject effect;
        bool playerInRange;
        //Animator anim;
        //AudioSource playerAudio;
        //PlayerMovement playerMovement;
        //PlayerShooting playerShooting;
        bool isDead;
        //bool damaged;


        void Awake()
        {
            //anim = GetComponent<Animator>();
            //playerAudio = GetComponent<AudioSource>();
            //playerMovement = GetComponent<PlayerMovement>();
            //playerShooting = GetComponentInChildren<PlayerShooting>();
            //effect = GameObject.Find("FireMobile");
            currentHealth = startingHealth;
        }


        void Update()
        {
            //timer += Time.deltaTime;

            //if (timer >= timeBetweenAttacks && playerInRange && currentHealth > 0)
            //{
            //    timer = 0f;

            //    if (currentHealth > 0)
            //    {
            //        TakeDamage(attackDamage);
            //    }
            //}
        }

        //void OnTriggerEnter(Collider other)
        //{
        //    //effect.transform.position = transform.position;
        //    if (other.gameObject.tag == "towerkiller")
        //    {
        //        effect.transform.position = transform.position;
        //        playerInRange = true;
                
        //    }
        //}
        //void OnTriggerExit(Collider other)
        //{
        //    if (other.gameObject.tag == "towerkiller")
        //    {

        //        playerInRange = false;

        //    }
        //}
        public void TakeDamage(int amount)
        {
            //damaged = true;

            currentHealth -= amount;

            //healthSlider.value = currentHealth;

            //playerAudio.Play();

            if (currentHealth <= 0 && !isDead)
            {
                Death();
            }
        }


        void Death()
        {
            isDead = true;
            
            //Destroy(gameObject);
            gameObject.GetComponent<Turret>().enabled=false;
            gameObject.GetComponent<TargetFinder>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.SetActive(false);
            gameObject.SetActive(true);
            GameObject firework = Instantiate(effect) as GameObject;
            firework.transform.position = transform.position;
            //Destroy(firework, 15);
            //playerShooting.DisableEffects();

            //anim.SetTrigger("Die");

            //playerAudio.clip = deathClip;
            //playerAudio.Play();

            //playerMovement.enabled = false;
            //playerShooting.enabled = false;
        }


        //public void RestartLevel()
        //{
        //    Application.LoadLevel(Application.loadedLevel);
        //}
    }
}