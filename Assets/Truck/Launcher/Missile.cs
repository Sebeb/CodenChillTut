using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Collider2D col2d;
    public float explosionRadius, explosionMaxForce;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, rb2d.velocity));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 contactPoint = other.contacts[0].point;
        foreach (Collider2D otherObject in Physics2D.OverlapCircleAll(contactPoint, explosionRadius))
        {
            if (otherObject.attachedRigidbody == null) continue;
            
            Vector2 relativePosition = (Vector2) otherObject.transform.position - contactPoint;
            otherObject.attachedRigidbody.AddForce(relativePosition.normalized * explosionMaxForce * (1 - (relativePosition.magnitude / explosionMaxForce)), ForceMode2D.Impulse);
        }
        
        Destroy(gameObject);
    }
}