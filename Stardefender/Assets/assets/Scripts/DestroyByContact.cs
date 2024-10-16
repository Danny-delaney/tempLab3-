﻿using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;
	public int HP;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "EnemyBullet")
		{
			return;
		}

		if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}

		if (other.tag == "Player")
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		}

		if(other.tag == "Enemy" && tag == "Player")
		{
            Destroy(other.gameObject);
			Debug.Log("DAMAGE TAKEN: PLAYER");
            HP--;
        }

		if (other.tag == "Bullet" && tag == "Enemy")
		{
            Destroy(other.gameObject);
            HP--;
		}

        if (HP == 0)
		{
			gameController.AddScore(scoreValue);
			//Destroy (other.gameObject);
			Destroy (gameObject);
		}
		
	}
}