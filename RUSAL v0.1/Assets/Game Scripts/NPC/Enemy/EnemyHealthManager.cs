using UnityEngine;
using System.Collections;

public class EnemyHealthManager : MonoBehaviour {

	public int enemyHealth;

	public GameObject deathEffect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyHealth <= 0) {
			Instantiate (deathEffect, transform.position, transform.rotation);
			Destroy (gameObject);
		}

	}

	public void giveDamage(int dmgToGive)
	{
		enemyHealth -= dmgToGive;
	}

}
