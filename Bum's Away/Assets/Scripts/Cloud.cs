using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public Sprite[] lightClouds;

    public int MAX_CLOUD_NUM = 12;

    public static event Action<Cloud> OnCloudDeath;


    private SpriteRenderer sr;
    private bool isMovingLeft = false;
    private float cloudSpeed;

    private float cloudInitialY;
    private float cloudInitialX = 11.05f;
    private float cloudOffScreenX = 11.5f;

    private float DEFAULT_CLOUD_SPEED = 0.001f;
    private float DELTA_TIME_BASE = 0.0035f;

    private int cloudNum = 0;

    // Start is called before the first frame update
    public void Start()
    {
        sr = this.gameObject.GetComponent<SpriteRenderer>();
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
            /* Check for cloud going off screen */
            if(cloudPositionX < -cloudOffScreenX)
            {
                /* Cloud gone off screen - delete it */
                OnCloudDeath?.Invoke(this);
                Destroy(this.gameObject);
            }
        }
        else
        {
            cloudPositionX += cloudSpeed * (Time.deltaTime / DELTA_TIME_BASE);
            /* Check for cloud going off screen */
            if (cloudPositionX > cloudOffScreenX)
            {
                /* Cloud gone off screen - delete it */
                OnCloudDeath?.Invoke(this);
                Destroy(this.gameObject);
            }

        }

        transform.position = new Vector3(cloudPositionX, cloudInitialY, 0);
    }

    public void MakeCloud(int num)
    {
        float rand = UnityEngine.Random.Range(0.0f, 1.0f);
        int image_num;
        float x;
        cloudNum = num;

        if(sr == null)
        {
            sr = this.gameObject.GetComponent<SpriteRenderer>();
        }
        // Make cloud
        image_num = UnityEngine.Random.Range(0, lightClouds.Length);
        sr.sprite = lightClouds[image_num];

        // Set cloud to move left or right 
        cloudSpeed = DEFAULT_CLOUD_SPEED;
#if (PI)
        rand = UnityEngine.Random.Range(0.0f, 1.0f);
        if (rand > 0.5f)
        {
            isMovingLeft = false;
            x = -cloudInitialX;
        }
        else
        {
#endif
            isMovingLeft = true;
        // Move right moving cloud to opposite side of screen
        x = cloudInitialX;
#if (PI)
        }
#endif
        cloudInitialY = cloudNum - 3.0f + UnityEngine.Random.Range(-0.5f, 0.5f);
        this.transform.position = new Vector3(x, cloudInitialY, this.transform.position.z);
        this.sr.sortingLayerName = "clouds1";

        // Set speed
        cloudSpeed += (MAX_CLOUD_NUM - cloudNum) * 0.0004f;//UnityEngine.Random.Range(0.0001f, 0.003f);

    }
}
