using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Animator animator;
    public float speed;

    private bool isMovingLeft = true;

    private float screenEdgeOffset = 0.15f;
    private float leftClamp = 0;
    private float rightClamp = 410;
    private float targetInitialY;
    private Vector3 topRightCorner;
    private SpriteRenderer sr;

    public bool isActive;

    private float WALKING_SPEED = 0.01f;
    private float RUNNING_SPEED = 0.025f;
    private float DELTA_TIME_BASE = 0.0035f;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("IsMovingLeft", true);
        animator.SetInteger("Poohiness", 0);
        speed = WALKING_SPEED;
        isMovingLeft = true;
        screenEdgeOffset = Utilities.ResizeXValue(screenEdgeOffset);
        targetInitialY = transform.position.y;
        sr = GetComponent<SpriteRenderer>();

        topRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        SetClamps();

        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTarget();
    }

    private void SetClamps()
    {
        float objectWidth = sr.bounds.extents.x;

        leftClamp = -topRightCorner.x + objectWidth;
        rightClamp = topRightCorner.x - objectWidth;
    }

    private void MoveTarget()
    {
        float targetPositionX = transform.position.x;

        /* Check if plane has reached edge of screen and turn it around if it has */
        if (isMovingLeft == true)
        {
            targetPositionX -= speed * (Time.deltaTime / DELTA_TIME_BASE);
            if (targetPositionX <= leftClamp)
            {
                isMovingLeft = false;
                animator.SetBool("IsMovingLeft", false);
            }
        }
        else
        {
            targetPositionX += speed * (Time.deltaTime / DELTA_TIME_BASE);
            if (targetPositionX >= rightClamp)
            {
                isMovingLeft = true;
                animator.SetBool("IsMovingLeft", true);
            }
        }

        transform.position = new Vector3(targetPositionX, targetInitialY, 0);
    }
}
