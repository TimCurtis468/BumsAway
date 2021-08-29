using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooh : MonoBehaviour
{
    public Sprite[] poohImages;
    private SpriteRenderer sr;  // For splatter particle effect
    private Rigidbody2D rb;  // For splatter particle effect

    public static event Action<Pooh> OnPoohHit;

    private void Awake()
    {
        this.sr = GetComponentInChildren<SpriteRenderer>();
        this.rb = GetComponentInChildren<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
    }

    public void SetPoohImage(int imageNum)
    {
        if( imageNum <= poohImages.Length)
        {
            sr.sprite = poohImages[imageNum];
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        switch (coll.gameObject.tag)
        {
            case "Target":
                // Increase Target poohiness
                break;
            default:
                break;
        }

        // Enable partical effect to splatter

        // Destroy this pooh
    }


}
