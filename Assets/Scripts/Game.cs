using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

	public GameObject[] enemies;
	public float minCooldown, maxCooldown, minYSpawn,maxYSpawn,xSpawn;
	public static Game controller;
	public int lives = 5;

	void Awake()
	{
		controller = this;
	}
	
	void Start () {
		StartCoroutine(SpawnEnemy());
	}

	private IEnumerator SpawnEnemy()
	{
		Instantiate(enemies[Random.Range(0,enemies.Length)], new Vector3(xSpawn * (Random.Range(0, 2) == 1 ? 1 : -1), Random.Range(minYSpawn, maxYSpawn)),Quaternion.identity);
		yield return new WaitForSeconds(Random.Range(minCooldown,maxCooldown));
		StartCoroutine(SpawnEnemy());
	}
}
