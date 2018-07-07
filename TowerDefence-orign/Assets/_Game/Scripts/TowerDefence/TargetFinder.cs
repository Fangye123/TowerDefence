using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace TowerDefence {

	public class TargetFinder : MonoBehaviour {

		[SerializeField]
		public float range = 1.0f;

		public List<Transform> enemyTargets;
		private GameManager gameManager;

		//===================================================
		// UNITY METHODS
		//===================================================

		/// <summary>
		/// Awake.
		/// </summary>
		void Awake() {
			enemyTargets = new List<Transform>();
			
		}

		/// <summary>
		/// Start.
		/// </summary>
		void Start() {
			gameManager = GameManager.instance;
		}

		/// <summary>
		/// Update.
		/// </summary>
		void Update() {

		}

		//===================================================
		// PUBLIC METHODS
		//===================================================

		/// <summary>
		/// Get the list of Enemies from the game manager and test if they are within range.
		/// </summary>
		/// <returns></returns>
		public Transform Find() {
			enemyTargets.Clear();
			
			int numEnemies = gameManager.EnemyList.Count;
			for( int i = 0; i < numEnemies; i++ ) {
				Transform target = gameManager.EnemyList[ i ].transform;
				
				float distance = Vector3.Distance( target.position, transform.position );
				if( distance <= range ) {
					enemyTargets.Add( target );
				}
			}

			// return first enemy if found.
			if( enemyTargets.Count > 0 ) {
				if(enemyTargets[0].GetComponent<Enemy>().hit){
					//transform.LookAt(enemyTargets[0].transform);
					return enemyTargets[ 0 ];
				}
				else if(enemyTargets.Count > 1){
					//transform.LookAt(enemyTargets[1].transform);
					return enemyTargets[ 1 ];
				}
				else
					return null;
			} else {
				return null;
			}
		}

		//===================================================
		// PRIVATE METHODS
		//===================================================


		//===================================================
		// EVENTS METHODS
		//===================================================


	}
}