using UnityEngine;
using System.Collections;

namespace TowerDefence
{
    public class TKattack : MonoBehaviour
    {

        public float timeBetweenAttacks = 0.5f;
        public int attackDamage = 10;


        Animator anim;
      //GameObject tower;
        Thealth tHealth;
        Health enemyHealth;
        bool playerInRange;
        float timer;
        NavMeshAgent nav;

        void Awake()
        {
            //tower = GameObject.FindGameObjectWithTag("tower");
            //tHealth = tower.GetComponent<THealth>();
            enemyHealth = GetComponent<Health>();
            anim = GetComponentInChildren<Animator>();
            nav = GetComponent<NavMeshAgent>();
        }


        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "tower")
            {
                tHealth = other.gameObject.GetComponent<Thealth>();    
                playerInRange = true;
                anim.SetTrigger("shoot");
                nav.enabled = false;
            }
        }


        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "tower")
            {
                playerInRange = false;
                anim.SetTrigger("run");
                nav.enabled = true;
            }
            if (other.gameObject.tag == "Goal")
            {
                gameObject.SetActive(false);
            }
        }


        void Update()
        {
            timer += Time.deltaTime;

            if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.HealthValue > 0)
            {
                Attack();
            }

            if (tHealth != null && tHealth.currentHealth <= 0)
            {
                anim.SetTrigger("run");
                nav.enabled = true;
            }
        }


        void Attack()
        {
            timer = 0f;

            if (tHealth.currentHealth > 0)
            {
                tHealth.TakeDamage(attackDamage);
            }
        }
    }
}