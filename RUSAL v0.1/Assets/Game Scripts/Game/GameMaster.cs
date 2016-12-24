using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;
	void Start (){
		if (gm == null){
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
		}
	}

	public Transform playerPrefab;
	public Transform spawnPoint;

	public void RespawnPlayer (){
		Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);

	}

	public static void KillPlayer(Player player){
		Destroy (player);
		gm.RespawnPlayer();
	}

//	public static void KillEnemy(Enemy enemy){
//		Destroy (enemy.gameObject);
//	}
}
