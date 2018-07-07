using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TowerDefence {

	public class GameManager : MonoBehaviour {
		public static GameManager instance = null;
		public GameObject gameOver;
		
		[SerializeField]
		private GameObject OptionMenu;
		
		[SerializeField]
		private GameObject Hero;
		
		[SerializeField]
		private GameObject camera;

		[SerializeField]
		private Transform projectileContainer;
		public Transform ProjectileContainer {
			get { return projectileContainer; }
			private set { projectileContainer = value; }
		}

		public List<GameObject> EnemyList {
			get { return spawnManager.EnemyList; }
		}
		
		public List<GameObject> turrets {
			get { return turretManager.turrets; }
		}
		
		[SerializeField]
		private Transform goal;
		public Transform Goal {
			get { return goal; }
		}

		[SerializeField]
		private PlacementTile[] placementTiles;

		[SerializeField]
		private float difficultyInterval = 3.0f;

		[SerializeField]
		private int health = 10;
		public int Health {
			get { return health; }
			private set { health = value; }
		}
		
		[SerializeField]
		private int wave;
		public int Wave {
			get { return wave; }
			private set { wave = value; }
		}
		
		[SerializeField]
		private int Herohealth;
		public int HeroHealth {
			get { return Herohealth; }
			private set { Herohealth = value; }
		}

		[SerializeField]
		public int money = 15;
		public int Money {
			get { return money; }
			private set { money = value; }
		}		

		[SerializeField]
		private int enemyHealthIncrease = 1;		

		[SerializeField]
		private float initialDelay = 3.0f;		

		public int SpawnCount {
			get { return spawnManager.SpawnCount; }
		}

		private TurretManager turretManager;
		private SpawnManager spawnManager;
		public UIManager uiManager;
		private PlacementTile currentSelectedTile;
		public Turret currentSelectedTurret;
		private bool isActive;
		
		public GameObject button;

		//===================================================
		// UNITY METHODS
		//===================================================

		/// <summary>
		/// Awake.
		/// </summary>
		void Awake() {
			# region - Singlton
			if( instance == null ) {
				instance = this;
			}/* else if( instance != this ) {
				Destroy( gameObject );
			}
			DontDestroyOnLoad( gameObject );*/
			# endregion
			
			uiManager = GetComponentInChildren<UIManager>();
			turretManager = GetComponentInChildren<TurretManager>();
			spawnManager = GetComponentInChildren<SpawnManager>();
			Time.timeScale=1;
		}

		/// <summary>
		/// Start.
		/// </summary>
		void Start() {
			isActive = true;
			gameOver.SetActive(false);

			for( int i = 0; i < placementTiles.Length; i++ ) {
				PlacementTile tile = placementTiles[ i ];
				tile.EventSelected += OnPlacementTileClicked;
			}

			uiManager.EventBuyTurret += CreateTurret;
			uiManager.UpdateHud();

			spawnManager.EventSpawned += OnEnemySpawn;
			spawnManager.EventReachedTarget += OnEnemyReachedTarget;
			spawnManager.EventDied += OnEnemyDied;

			Invoke( "StartGame", initialDelay );
			OptionMenu.SetActive(false);
		}

		/// <summary>
		/// Update.
		/// </summary>
		void Update() {
			wave=spawnManager.wave;
			HeroHealth=Hero.GetComponent<playerHealth>().currentHealth;
			if(Input.GetKeyDown(KeyCode.Escape)){
				OptionMenu.SetActive(true);
				Time.timeScale=0;
			}
			
			for( int i = 0; i < turrets.Count; i++ ) {
				GameObject tile = turrets[ i ];
				tile.GetComponent<Turret>().EventSelected += OnTurretClicked;
			}
			
			
			if(HeroHealth<=0||health<=0){
				camera.SetActive(true);
				gameOver.SetActive(true);
				Time.timeScale=0;
			}
		}

		//===================================================
		// PUBLIC METHODS
		//===================================================

		/// <summary>
		/// Starts the game.
		/// </summary>
		public void ResetGame() {
			button.GetComponent<ChangeMode>().Change=!button.GetComponent<ChangeMode>().Change;
			/*Health = 10;
			Money = 15;
			spawnManager.Reset();
			turretManager.Reset();
			uiManager.UpdateHud();*/
		}
		
		public void RestartGame(){
			Time.timeScale=1;
			Application.LoadLevel(Application.loadedLevel);
		}

		//===================================================
		// PRIVATE METHODS
		//===================================================

		/// <summary>
		/// Starts the game.
		/// </summary>
		private void StartGame() {
			spawnManager.StartSpawning();
			StartCoroutine( IncreaseDifficulty() );
		}

		/// <summary>
		/// Creates a turret.
		/// </summary>
		/// <param name="turretType">Type of the turret.</param>
		/// <param name="position">The position.</param>
		private void CreateTurret( Enums.TurretType turretType, Vector3 position ) {
			currentSelectedTile.enabled = false;
			currentSelectedTile.transform.GetChild(0).gameObject.SetActive(false);
			turretManager.CreateTurret( turretType, position );

			int cost = 0;
			switch( turretType ) {
				case Enums.TurretType.Type_A:
					cost = turretManager.TurretCostA;
					break;
				case Enums.TurretType.Type_B:
					cost = turretManager.TurretCostB;
					break;
				case Enums.TurretType.Type_C:
					cost = turretManager.TurretCostC;
					break;
				case Enums.TurretType.Type_D:
					cost = turretManager.TurretCostD;
					break;
			}
			money -= cost;
			uiManager.UpdateHud();
		}
		
		/// <summary>
		/// Increases the difficulty.
		/// </summary>
		/// <returns></returns>
		private IEnumerator IncreaseDifficulty() {
			while( isActive ) {
				spawnManager.EnemyHealth += enemyHealthIncrease;
				yield return new WaitForSeconds( difficultyInterval );
			}
		}

		//===================================================
		// EVENTS METHODS
		//===================================================

		
		/// <summary>
		/// Called when [placement tile clicked].
		/// </summary>
		/// <param name="tile">The tile.</param>
		/// <param name="position">The position.</param>
		private void OnPlacementTileClicked( PlacementTile tile, Vector3 position ) {
			if( money > 1 ) {
				currentSelectedTile = tile;
				uiManager.ShowBuyUI( position );
			}
		}
		
		private void OnTurretClicked( Turret turret, Vector3 position ) {
			if( turret.level < 3 ) {
				currentSelectedTurret=turret;
				uiManager.ShowLevelUI( position );
			}
		}

		/// <summary>
		/// Called when [enemy spawn].
		/// </summary>
		/// <exception cref="System.NotImplementedException"></exception>
		private void OnEnemySpawn() {
			uiManager.UpdateHud();
		}

		/// <summary>
		/// Called when [enemy reached target].
		/// </summary>
		/// <exception cref="System.NotImplementedException"></exception>
		private void OnEnemyReachedTarget() {
			Health -= 1;
			uiManager.UpdateHud();

			if( health <= 0 ) {
				Time.timeScale=0;
				gameOver.SetActive(true);
			}
		}


		/// <summary>
		/// Called when [enemy died].
		/// </summary>
		/// <param name="enemyMoney">The enemy money.</param>
		private void OnEnemyDied( int enemyMoney ) {
			Money += enemyMoney;
			uiManager.UpdateHud();
		}

	}
}