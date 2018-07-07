using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

	[SerializeField]
	private GameObject prefab;
	
	[SerializeField]
	private GameObject enemy1;
	
	[SerializeField]
	private GameObject enemy2;
	
	[SerializeField]
	private GameObject enemy3;
	
	[SerializeField]
	private GameObject enemy4;
	
	[SerializeField]
	private GameObject enemy5;
	
	[SerializeField]
	private GameObject enemy6;
	
	[SerializeField]
	private GameObject enemy7;
	
	[SerializeField]
	private GameObject enemy8;
	
	[SerializeField]
	private GameObject enemy9;
	
	[SerializeField]
	private GameObject boss1;
	
	[SerializeField]
	private GameObject Angel;
	
	[SerializeField]
	private GameObject herokiller;
	
	[SerializeField]
	private GameObject towerkiller;
	
	[SerializeField]
	private int numOfObjects = 5;

	[SerializeField]
	private GameObject optionalParent;

	private List<GameObject> pool;
	

	//===================================================
	// PUBLIC METHODS
	//===================================================

	/// <summary>
	/// Initializes the pool with the specified number of GameObject derived from the prefab.
	/// </summary>
	public void Init() {
		pool = new List<GameObject>( numOfObjects );
		for( int i = 0; i < numOfObjects; i++ ) {
			AddGameObject();
		}
	}

	/// <summary>
	/// Gets a GameObject from the pool or creates a new one if it is full/empty.
	/// </summary>
	/// <returns></returns>
	public GameObject GetGameObject() {
		for( int i = 0; i < pool.Count; i++ ) {
			GameObject ob = pool[ i ];
			if( !ob.activeSelf ) {
				ob.SetActive( true );
				return ob;
			}
		}

		// if here, then there is not enough to spawn so add another one.
		GameObject additionalGO = AddGameObject();
		additionalGO.SetActive( true );
		return additionalGO;
		
	}

	/// <summary>
	/// Releases the object back to the pool but it is so simple it is not really needed.
	/// </summary>
	/// <param name="go">The go.</param>
	public void ReleaseObject( GameObject go ) {
		go.SetActive( false );
	}	

	/// <summary>
	/// Releases all the gameObjects - disables them.
	/// </summary>
	public void ReleaseAll() {
		for( int i = 0; i < pool.Count; i++ ) {
			GameObject ob = pool[ i ];
			ob.SetActive( false );
		}
	}

	//===================================================
	// PRIVATE METHODS
	//===================================================
	
	public void ChangeGameObject(int i){
		switch(i){
			case 1:prefab=enemy1;break;
			case 2:prefab=enemy2;break;
			case 3:prefab=enemy3;break;
			case 4:prefab=enemy4;break;
			case 5:prefab=enemy5;break;
			case 6:prefab=enemy6;break;
			case 7:prefab=enemy7;break;
			case 8:prefab=enemy8;break;
			case 9:prefab=enemy9;break;
			case 11:prefab=boss1;break;
			case 12:prefab=Angel;break;
			case 21:prefab=herokiller;break;
			case 22:prefab=towerkiller;break;
			default:prefab=enemy1;break;
		}
		
	}
	/// <summary>
	/// Adds a GameObject to pool.
	/// </summary>
	/// <returns></returns>
	public GameObject AddGameObject() {
		//GameObject go = Instantiate( _prefab, Vector3.zero, Quaternion.identity ) as GameObject;
		GameObject go = Instantiate( prefab ) as GameObject;
		if( optionalParent == null ) {
			go.transform.SetParent( this.transform );
		} else {
			go.transform.SetParent( optionalParent.transform, true );
		}
		go.SetActive( true );
		pool.Add( go );
				
		return go;
	}
}
