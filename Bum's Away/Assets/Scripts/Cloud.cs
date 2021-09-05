using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public Sprite[] lightClouds;
    public Sprite[] darkClouds;

    private SpriteRenderer sr;
    private bool isMovingLeft = false;
    private float cloudSpeed;

    private float cloudInitialY;
    private float cloudInitialX;

    private float DEFAULT_CLOUD_SPEED = 0.001f;
    private float DELTA_TIME_BASE = 0.0035f;

    // Start is called before the first frame update
    public void Start()
    {
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        cloudInitialX = transform.position.x;
        MakeCloud();

    }


    private void OnCollisionEnter2D(Collision2D coll)
    {
        MakeCloud();
    }

    // Update is called once per frame
    public void Update()
    {
        // Move cloud
        float cloudPositionX = transform.position.x;

        /* Check if plane has reached edge of screen and turn it around if it has */
        if (isMovingLeft == true)
        {
            cloudPositionX -= cloudSpeed * (Time.deltaTime / DELTA_TIME_BASE);
        }
        else
        {
            cloudPositionX += cloudSpeed * (Time.deltaTime / DELTA_TIME_BASE);
        }

        transform.position = new Vector3(cloudPositionX, cloudInitialY, 0);
    }

    private void MakeCloud()
    {
        float rand = UnityEngine.Random.Range(0.0f, 1.0f);
        int image_num;
        float x;
        if (rand > 0.1f)
        {
            // Make light cloud
            image_num = UnityEngine.Random.Range(0, lightClouds.Length);
            sr.sprite = lightClouds[image_num];
        }
        else
        {
            // make dark cloud
            image_num = UnityEngine.Random.Range(0, darkClouds.Length);
            sr.sprite = darkClouds[image_num];
        }

        // Set cloud to move left or right 
        cloudSpeed = DEFAULT_CLOUD_SPEED;
        rand = UnityEngine.Random.Range(0.0f, 1.0f);
        if (rand > 0.5f)
        {
            isMovingLeft = false;
            x = -cloudInitialX;
        }
        else
        {
            isMovingLeft = true;
            // Move right moving cloud to opposite side of screen
            x = cloudInitialX;
        }
        cloudInitialY = UnityEngine.Random.Range(-1.0f, 3.0f);
        this.transform.position = new Vector3(x, cloudInitialY, this.transform.position.z);

        // Set speed
        cloudSpeed += UnityEngine.Random.Range(0.0f, 0.002f);

    }

}
