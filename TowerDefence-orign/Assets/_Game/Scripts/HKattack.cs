using UnityEngine;
using System.Collections;

namespace TowerDefence
{
    public class HKattack : MonoBehaviour
    {

        public float timeBetweenAttacks = 0.5f;
        public int attackDamage = 10;


        Animator anim;
        GameObject player;
        playerHealth playerHealth;
        Health enemyHealth;
        bool playerInRange;
        float timer;
        NavMeshAgent nav;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<playerHealth>();
            enemyHealth = GetComponent<Health>();
            anim = GetComponentInChildren<Animator>();
            nav = GetComponent<NavMeshAgent>();
        }


        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == player)
            {
                playerInRange = true;
                anim.SetTrigger("attack");
                nav.enabled = false;
            }
        }


        void OnTriggerExit(Collider other)
        {
            if (other.gameObject == player)
            {
                playerInRange = false;
                anim.SetTrigger("run");
                nav.enabled = true;
            }
        }


        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.HealthValue > 0)
            {
                Attack();
            }

            if (playerHealth.currentHealth <= 0)
            {
                anim.SetTrigger("playerdead");
            }
        }


        void Attack()
        {
            timer = 0f;
            if (playerHealth.currentHealth > 0)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }
}