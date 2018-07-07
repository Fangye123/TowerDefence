using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

namespace TowerDefence {

	public class Turret : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

		[SerializeField]
		private GameObject projectilePrefab;

		[SerializeField]
		public float fireRate = 1.0f;

		private TargetFinder targetFinder;
		private Transform projectileContainer;
		private bool isActive;
		public int level;
		
		public delegate void DelegateSelected( Turret tile, Vector3 position );
		public event DelegateSelected EventSelected;
		
		private Color rolloverColor = Color.white;
		
		private Color defaultColor;
		private Renderer rendererer;
		
		//===================================================
		// UNITY METHODS
		//===================================================

		/// <summary>
		/// Awake.
		/// </summary>
		void Awake() {
			targetFinder = GetComponent<TargetFinder>();
			rendererer = GetComponentInChildren<Renderer>();
			defaultColor = rendererer.material.color;
		}

		/// <summary>
		/// Start.
		/// </summary>
		void Start() {
			isActive = true;
			projectileContainer = GameManager.instance.ProjectileContainer;
			Invoke( "StartLookingForTargets", 1.0f );
			level=1;
		}	

		/// <summary>
		/// Update.
		/// </summary>
		void Update() {
		}

		//===================================================
		// PUBLIC METHODS
		//===================================================

		//===================================================
		// PRIVATE METHODS
		//===================================================

		/// <summary>
		/// Starts the looking for targets.
		/// </summary>
		private void StartLookingForTargets() {
			StartCoroutine( LookForTarget() );
		}

		/// <summary>
		/// Looks for target and if found calls fire.
		/// </summary>
		/// <returns></returns>
		private IEnumerator LookForTarget() {
			while( isActive ) {
				Transform target = targetFinder.Find();
				if( target != null  ) {
					FireAtTarget( target );
				}
				yield return new WaitForSeconds( fireRate );
			}
		}

		/// <summary>
		/// Createsa projectile.
		/// </summary>
		/// <param name="target">The target.</param>
		private void FireAtTarget( Transform target ) {
			Vector3 position = transform.position;
			position.y += 0.4f;

			GameObject projectileGO = Instantiate( projectilePrefab, position, Quaternion.identity ) as GameObject;
			projectileGO.transform.SetParent( projectileContainer );

			Projectile projectile = projectileGO.GetComponent<Projectile>();
			if(this.transform.GetChild(0).gameObject.GetComponent<Animation>()!=null)
				this.transform.GetChild(0).gameObject.GetComponent<Animation>().Play();
			if(this.GetComponent<Animation>()!=null)
				this.GetComponent<Animation>().Play("Attack");
			projectile.Fire( target );
		}

		//===================================================
		// EVENTS METHODS
		//===================================================
		
		public void OnPointerClick( PointerEventData eventData ) {
			if( EventSelected != null ) {				
				EventSelected( this, transform.position );
			}
		}

		/// <summary>
		/// Called when [pointer enter].
		/// </summary>
		/// <param name="eventData">The event data.</param>
		public void OnPointerEnter( PointerEventData eventData ) {
			rendererer.material.color = rolloverColor;
		}

		/// <summary>
		/// Called when [pointer exit].
		/// </summary>
		/// <param name="eventData">The event data.</param>
		public void OnPointerExit( PointerEventData eventData ) {
			rendererer.material.color = defaultColor;
		}
		
		void OnTriggerEnter(Collider other){
			if(other.gameObject.tag == "bufftower"&&this.tag!="Player"){
				fireRate-=0.2f;
				Behaviour halo=(Behaviour)GetComponent("Halo");
				halo.enabled=true;
				this.GetComponent<TargetFinder>().range+=10;
			}
				
		}
		
		void OnTriggerExit(Collider other){
			if(other.gameObject.tag == "bufftower"&&this.tag!="Player"){
				fireRate+=0.2f;
				Behaviour halo=(Behaviour)GetComponent("Halo");
				halo.enabled=false;
				this.GetComponent<TargetFinder>().range-=10;
			}
				
		}

	}

}
