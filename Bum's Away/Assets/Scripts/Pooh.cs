using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Pooh : MonoBehaviour
{
    public Sprite[] poohImages;
    private SpriteRenderer sr;  // For splatter particle effect
    private Rigidbody2D rb;  // For splatter particle effect

    public ParticleSystem DestroyEffect;

    public static event Action<Pooh> OnPoohDeath;

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
                Target pm = coll.gameObject.GetComponent<Target>();
                // Increase Target poohiness
                if (pm.poohHitActive == false)
                {
                    int poohiness = pm.animatorTarget.GetInteger("Poohiness");
                    poohiness++;
                    if (poohiness > pm.MAX_POOHINESS)
                    {
                        poohiness = pm.MAX_POOHINESS;
                    }
                    pm.animatorTarget.SetInteger("Poohiness", poohiness);
                    if (poohiness == 2)
                    {
                        pm.speed = pm.RUNNING_SPEED;
                    }
                    // Start pooh holdoff timer to stop double trigger
                    pm.poohHitActive = true;
                    pm.poohTimer = 0.0f;
                }
                break;
            case "Ground":
                // Hit the ground and explode
                OnPoohDeath?.Invoke(this);
                SpawnDestroyEffect();
                Destroy(this.gameObject, 0.1f);
                break;
            default:
                break;
        }

        // Enable partical effect to splatter

        // Destroy this pooh
    }

    private void SpawnDestroyEffect()
    {
        Vector3 poohPos = gameObject.transform.position;
        Vector3 spawnPosition = new Vector3(poohPos.x, poohPos.y, poohPos.z - 0.2f);
        GameObject effect = Instantiate(DestroyEffect.gameObject, spawnPosition, Quaternion.identity);

        MainModule mm = effect.GetComponent<ParticleSystem>().main;
        mm.startColor = this.sr.color;
        Destroy(effect, DestroyEffect.main.startLifetime.constant);
    }
}
