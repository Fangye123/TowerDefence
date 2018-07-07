using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace TowerDefence.UI {
public class UILevelUP : AbsUIPopup {

	public Turret turret;
	[SerializeField]
		private GameManager gameManager;
	
	public delegate void DelegateLevelUPTurret( Turret turret, Vector3 position);
	public event DelegateLevelUPTurret EventLevelUP;	

	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update(){
		turret=gameManager.currentSelectedTurret;
	}
	
	
	public override void Show( Vector3 position ) {
			base.Show( position );
		}
	
	public void OnLevelUP() {
		if(gameManager.money>=2){
			TurretSelected( turret );
			if(turret.level==1){
				turret.transform.localScale=new Vector3(1.3f,1.3f,1.3f);
				gameManager.money-=2;	
			}
			else{
				turret.transform.localScale=new Vector3(1.6f,1.6f,1.6f);
				gameManager.money-=3;	
			}
			turret.GetComponent<TargetFinder>().range+=20f;
			turret.fireRate-=0.1f;
			turret.level++;
			gameManager.uiManager.UpdateHud();
		}
	}
	
	private void TurretSelected( Turret t  ) {
			if( EventLevelUP != null) {
				EventLevelUP( t, position );
			}
			OnClose();
		}
}
}
