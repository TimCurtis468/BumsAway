using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plane : MonoBehaviour
{

    #region Singleton
    private static Plane _instance;
    public static Plane Instance => _instance;


    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public Animator animator;
    public float speed;

    private bool isMovingLeft = true;

    private float screenEdgeOffset = 0.15f;
    private float leftClamp = 0;
    private float rightClamp = 410;
    private float planeInitialY;
    private Vector3 topRightCorner;
    private SpriteRenderer sr;

    public bool isActive;

    private bool mouseButtonLatch = false;

    private float mouseTimer;

    private float MAX_SPEED = 0.075f;
    private float MIN_SPEED = 0.025f;
    private float DELTA_TIME_BASE = 0.0035f;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("IsMovingLeft", true);
        speed = MIN_SPEED;
        isMovingLeft = true;

        screenEdgeOffset = Utilities.ResizeXValue(screenEdgeOffset);
        planeInitialY = transform.position.y;
        sr = GetComponent<SpriteRenderer>();

        topRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        SetClamps();

        isActive = true;

        mouseButtonLatch = false;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessPlaneSpeed();
        CheckForEdgeOfScreen();
    }

    public float GetPlaneHeight()
    {
        return sr.bounds.extents.y;
    }

    public float GetPlaneWidth()
    {
        return sr.bounds.extents.x;
    }

    public bool IsMovingLeft()
    {
        return isMovingLeft;
    }

    public bool GetMouseButtonInfo(out float timer)
    {
        timer = mouseTimer;
        return mouseButtonLatch;
    }

    private void SetClamps()
    {
        float objectWidth = sr.bounds.extents.x;

        leftClamp = -topRightCorner.x + objectWidth;
        rightClamp = topRightCorner.x - objectWidth;
    }

    private void ProcessPlaneSpeed()
    {
        /* Check if mouse button has just been pressed down */
        if ((Input.GetMouseButtonDown(0) == true) &&
            (mouseButtonLatch == false))
        {
            /* Raise mouse button latch and reset timer */
            mouseButtonLatch = true;
            mouseTimer = 0.0f;

        }
        /* Check if mouse button is up */
        else if (Input.GetMouseButtonUp(0) == true)
        {
            /* Check to see if pooh should be dropped */
            CheckForPoohDrop();

            /* Clear mouse button latch */
            mouseButtonLatch = false;
        }
        if (Input.GetMouseButton(0) == false)
        {
            /* Clear mouse button latch if button is up */
            mouseButtonLatch = false;
        }


        /* Is the mouse button down? */
        if (mouseButtonLatch == true)
        {
            /* Yes - Speed up */
            /* Update timer */
            mouseTimer += Time.deltaTime;

            if (mouseTimer > 1.0f)
            {
                /* After 1 second, start to accelerate up to maximum */
                speed = speed * 1.01f;
                if (speed >= MAX_SPEED)
                {
                    speed = MAX_SPEED;
                }
            }
        }
        else
        {
            /* No - slow down until minimum */
            speed = speed / 1.01f;
            if (speed <= MIN_SPEED)
            {
                speed = MIN_SPEED;
            }
        }
    }

    private void CheckForEdgeOfScreen()
    {
        float planePositionX = transform.position.x;

        /* Check if plane has reached edge of screen and turn it around if it has */
        if (isMovingLeft == true)
        {
            planePositionX -= speed * (Time.deltaTime/ DELTA_TIME_BASE);
            if (planePositionX <= leftClamp)
            {
                isMovingLeft = false;
                animator.SetBool("IsMovingLeft", false);
            }
        }
        else
        {
            planePositionX += speed * (Time.deltaTime / DELTA_TIME_BASE);
            if (planePositionX >= rightClamp)
            {
                isMovingLeft = true;
                animator.SetBool("IsMovingLeft", true);
            }
        }

        transform.position = new Vector3(planePositionX, planeInitialY, 0);
    }

    private void CheckForPoohDrop()
    {
        float poohSpeed = speed * -5000.0f;
        if ((mouseButtonLatch == true) &&
            (mouseTimer < 1.0f))
        {
            Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y);
            /* Pooh drop - create a pooh */
            if (isMovingLeft == false)
            {
                poohSpeed = -poohSpeed;
            }
            PoohManager.Instance.SpawnPooh(pos, poohSpeed);
        }
    }
}
