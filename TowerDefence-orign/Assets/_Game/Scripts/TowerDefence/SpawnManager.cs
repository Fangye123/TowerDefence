using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TowerDefence {

	public class SpawnManager : MonoBehaviour {

		public delegate void DelegateEnemy();
		public event DelegateEnemy EventSpawned;
		public event DelegateEnemy EventReachedTarget;

		public delegate void DelegateEnemyDied( int money );
		public event DelegateEnemyDied EventDied;

		[SerializeField]
		public float spawnInterval = 2.0f;

		[SerializeField]
		public int enemyMoney = 1;

		[SerializeField]
		public int enemyHealth;
		public int EnemyHealth {
			get { return enemyHealth; }
			set { enemyHealth = value; }
		}	

		public List<GameObject> EnemyList {
			get;
			private set;
		}

		public int SpawnCount {
			get;
			private set;
		}
		
		[SerializeField]
		public GameObject WinMenu;

		private ObjectPool objectPool;
		private bool spawnEndlessly;
		private int num=0;
		public int wave=1;
		public GameObject angel;
		
		float time;
		int curParkTime = 0;
		//===================================================
		// UNITY METHODS
		//===================================================

		/// <summary>
		/// Awake.
		/// </summary>
		void Awake() {
			EnemyList = new List<GameObject>();
			objectPool = GetComponent<ObjectPool>();
			objectPool.Init();
		}

		/// <summary>
		/// Start.
		/// </summary>
		void Start() {
		}

		/// <summary>
		/// Update.
		/// </summary>
		void Update() {
			if(wave==5||wave==10){
				objectPool.ChangeGameObject(21);
				if(num>=10)
					objectPool.ChangeGameObject(22);
			}
				
			if(wave==10){
				if(Application.loadedLevel!=10)
					objectPool.ChangeGameObject(11);
				else{
					angel.SetActive(false);
					objectPool.ChangeGameObject(12);
				}
				if(num==1)
					spawnEndlessly=false;
				if(EnemyList.Count==0){
					WinMenu.SetActive(true);
					Time.timeScale=0;
				}
			}
			if(num==20){
					num=0;
					wave++;
					int n=Random.Range(1,9);
					objectPool.ChangeGameObject(n);
				
				
			}
			
		}

		//===================================================
		// PUBLIC METHODS
		//===================================================

		/// <summary>
		/// Starts the spawning.
		/// </summary>
		/// <param name="spawnDelay">The spawn delay.</param>
		public void StartSpawning() {
			spawnEndlessly = true;
			SpawnCount = 0;			
			StartCoroutine( SpawnEntity() );
		}

		/// <summary>
		/// Resets this instance.
		/// </summary>
		public void Reset() {
			SpawnCount = 0;
			for( int i = 0; i < EnemyList.Count; i++ ) {
				GameObject enemyGO = EnemyList[ i ];
				Enemy enemy = enemyGO.GetComponent<Enemy>();
				enemy.EventDied -= OnEnemyDied;
				enemy.EventReachedTarget -= OnEnemyReachedTarget;

				objectPool.ReleaseObject( enemyGO );
			}
			EnemyList.Clear();
			wave=1;
			num=0;
		}

		//===================================================
		// PRIVATE METHODS
		//===================================================

		/// <summary>
		/// Spawns the entity.
		/// </summary>
		/// <param name="spawnDelay">The spawn delay.</param>
		/// <returns></returns>
		private IEnumerator SpawnEntity() {
			while( spawnEndlessly ) {
				GameObject enemyGO;
				if (Time.time > time + 1f)
                {

                    time = Time.time;
                    curParkTime++;
                }
                if (curParkTime >= 2)
				{
					enemyGO = objectPool.AddGameObject();
					enemyGO.transform.position = transform.position;
					SpawnCount += 1;
					num++;

					Enemy enemy = enemyGO.GetComponent<Enemy>();
					enemy.EventDied += OnEnemyDied;
					enemy.EventReachedTarget += OnEnemyReachedTarget;
					enemy.SetHealthValue( EnemyHealth *wave+Application.loadedLevel*5);

					MoveTowardsGoal moveToGoal = enemyGO.GetComponent<MoveTowardsGoal>();
					moveToGoal.StartMoving();

					EnemyList.Add( enemyGO );
					curParkTime=0;
				}
					if( EventSpawned != null ) {
						EventSpawned();
					}

					yield return new WaitForSeconds( spawnInterval );
				
			}
		}


		/// <summary>
		/// Disposes the enemy.
		/// </summary>
		/// <param name="enemy">The enemy.</param>
		private void DisposeEnemy( Enemy enemy ) {
			enemy.EventDied -= OnEnemyDied;
			enemy.EventReachedTarget -= OnEnemyReachedTarget;

			int index = EnemyList.IndexOf( enemy.gameObject );
			if( index >= 0 ) {
				EnemyList.RemoveAt( index );
			}

			enemy.gameObject.transform.position = transform.position;
			objectPool.ReleaseObject( enemy.gameObject );
		}

		//===================================================
		// EVENTS METHODS
		//===================================================

		/// <summary>
		/// Called when [enemy died].
		/// </summary>
		/// <param name="enemy">The enemy.</param>
		private void OnEnemyDied( Enemy enemy ) {
			DisposeEnemy( enemy );

			if( EventDied != null ) {
				EventDied( enemyMoney );
			}			
		}

		/// <summary>
		/// Called when [enemy reached target].
		/// </summary>
		/// <param name="enemy">The enemy.</param>
		private void OnEnemyReachedTarget( Enemy enemy ) {
			DisposeEnemy( enemy );

			if( EventReachedTarget != null ) {
				EventReachedTarget();
			}
		}

	}
}