using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public LevelManager lvlMngr;
	[System.Serializable]
	public class PlayerStats{
	public int Health = 100;
	}

	public PlayerStats playerStats = new PlayerStats();

	public int fallBoundary = -20;

	void Start(){
		lvlMngr = FindObjectOfType<LevelManager> ();
	}
	void Update (){
		if(transform.position.y <= fallBoundary){
			DamagePlayer (99999);
		}
	}

	public void DamagePlayer (int damage){
		playerStats.Health -= damage;

		if(playerStats.Health <= 0){
			lvlMngr.RespawnPlayer();
			playerStats.Health = 100;
		}
			
	}

}
