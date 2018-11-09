using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialVelocity : MonoBehaviour
{

	public float startVelMin, startVelMax = 99;
	
	void Start ()
	{
		Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
		//rb2d.velocity = Vector2.left * Random.Range(startVelMin,startVelMax) * Mathf.Sign(transform.position.x);
		rb2d.velocity = new Vector2(5,0);
		GetComponent<SpriteRenderer>().flipX = transform.position.x > 0;
	}
	
}
