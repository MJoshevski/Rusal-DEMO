using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

    public LevelManager lvlMngr;


    // Use this for initialization
    void Start() {
        lvlMngr = FindObjectOfType<LevelManager>();

    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
//		if(other.name == "Player")
//        {
//			Debug.Log ("DEEEEEEEEEEEEEEAAAAAAAAAAAAAAAAAAAATHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
//			lvlMngr.RespawnPlayer();
//        }
		Debug.Log ("DEEEEEEEEEEEEEEAAAAAAAAAAAAAAAAAAAATHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
		lvlMngr.RespawnPlayer();
    }
}
