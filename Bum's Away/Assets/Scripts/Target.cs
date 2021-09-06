using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Target : MonoBehaviour
{
    protected bool isMovingLeft = true;

    public float speed;

    //    private bool isMovingLeft = true;

    private float screenEdgeOffset = 0.15f;
    private float leftClamp = 0;
    private float rightClamp = 410;
    private float targetInitialY;
    private Vector3 topRightCorner;
    private SpriteRenderer sr;

    public bool isActive;

    public Animator animatorTarget;

    public float RANDOM_SPEED_RANGE = 0.005f;
    public float WALKING_SPEED = 0.01f;
    public float RUNNING_SPEED = 0.025f;
    public float DELTA_TIME_BASE = 0.0035f;
    public float POOH_HOLDOFF_TIME = 1.0f;
    public int MAX_POOHINESS = 2;

    public bool poohHitActive = false;
    public float poohTimer = 0.0f;

    public void TargetInit(Animator anim)
    {
        float targetInitialX;
        float rand; 
        animatorTarget = anim;
        animatorTarget.SetBool("IsMovingLeft", true);
        animatorTarget.SetInteger("Poohiness", 0);
        isMovingLeft = true;
        screenEdgeOffset = Utilities.ResizeXValue(screenEdgeOffset);

        /* Set initial position to random X location */
        targetInitialY = transform.position.y;
        targetInitialX = transform.position.x;
        rand = UnityEngine.Random.Range(0, targetInitialX);
        targetInitialX -= rand * 2;
        transform.position = new Vector3(targetInitialX, targetInitialY, transform.position.z);

        /* Set random speed */
        speed = UnityEngine.Random.Range(WALKING_SPEED - 0.003f, WALKING_SPEED);

        /* Set random animation speed */
        animatorTarget.speed = 0.7f + UnityEngine.Random.Range(0.0f, 0.6f);

        sr = GetComponent<SpriteRenderer>();

        topRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        SetClamps();

        isActive = true;
        poohHitActive = false;
    }

    public void MoveTarget()
    {
        float targetPositionX = transform.position.x;

        /* Check if target has reached edge of screen and turn it around if it has */
        if (isMovingLeft == true)
        {
            targetPositionX -= speed * (Time.deltaTime / DELTA_TIME_BASE);
            if (targetPositionX <= leftClamp)
            {
                isMovingLeft = false;
                animatorTarget.SetBool("IsMovingLeft", false);
            }
        }
        else
        {
            targetPositionX += speed * (Time.deltaTime / DELTA_TIME_BASE);
            if (targetPositionX >= rightClamp)
            {
                isMovingLeft = true;
                animatorTarget.SetBool("IsMovingLeft", true);
            }
        }

        transform.position = new Vector3(targetPositionX, targetInitialY, 0);

        CheckForPoohHoldoff();
    }

    public int TargetHit()
    {
        int poohiness = 0;
        // Increase Target poohiness
        if (poohHitActive == false)
        {
            poohiness = animatorTarget.GetInteger("Poohiness");
            poohiness++;
            if (poohiness > MAX_POOHINESS)
            {
                poohiness = MAX_POOHINESS;
            }
            animatorTarget.SetInteger("Poohiness", poohiness);
            if (poohiness == 2)
            {
                speed = UnityEngine.Random.Range(RUNNING_SPEED - 0.003f, RUNNING_SPEED);
            }
            // Start pooh holdoff timer to stop double trigger
            poohHitActive = true;
            poohTimer = 0.0f;
        }

        return poohiness;
    }

    private void CheckForPoohHoldoff()
    {
        // Hold off a pooh hit for 1 second to stop double trigger
        if (poohHitActive == true)
        {
            poohTimer += Time.deltaTime;
            if (poohTimer > POOH_HOLDOFF_TIME)
            {
                poohHitActive = false;
            }
        }
    }

    private void SetClamps()
    {
        float objectWidth = sr.bounds.extents.x;

        leftClamp = -topRightCorner.x + objectWidth;
        rightClamp = topRightCorner.x - objectWidth;
    }
#if (PI)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pooh")
        {
            if (poohHitActive == false)
            {
                int poohiness = animator.GetInteger("Poohiness");
                poohiness++;
                if (poohiness > MAX_POOHINESS)
                {
                    poohiness = MAX_POOHINESS;
                }
                animator.SetInteger("Poohiness", poohiness);
                if (poohiness == 2)
                {
                    speed = RUNNING_SPEED;
                }
                // Start pooh holdoff timer to stop double trigger
                poohHitActive = true;
                poohTimer = 0.0f;
            }
        }
    }
#endif
}
