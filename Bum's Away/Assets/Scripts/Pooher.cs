using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooher : MonoBehaviour
{
    public Sprite[] pooher_images;

    private SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bool mouse_pressed = false;
        float timer = 0.0f;
        Vector3 plane_pos = Plane.Instance.transform.position;
        Vector3 pooher_pos = new Vector3(plane_pos.x, plane_pos.y, plane_pos.z);

        float x_offset = Plane.Instance.GetPlaneWidth() / 6;
        float y_offset = Plane.Instance.GetPlaneHeight() / 10;

        mouse_pressed = Plane.Instance.GetMouseButtonInfo(out timer);

        if (Plane.Instance.IsMovingLeft() == true)
        {
            pooher_pos.x = pooher_pos.x + x_offset;
            pooher_pos.y = pooher_pos.y + y_offset;
            if (pooher_images.Length > 0)
            {
                if ((mouse_pressed == true) && (timer <= 1.0f))
                {
                    pooher_pos.y = pooher_pos.y + (y_offset * 3);
                    spriteRenderer.sprite = pooher_images[2];
                    spriteRenderer.sortingOrder = 2;
                }
                else
                {
                    spriteRenderer.sprite = pooher_images[0];
                    spriteRenderer.sortingOrder = 0;
                }
            }
        }
        else
        {
            pooher_pos.x = pooher_pos.x - x_offset;
            pooher_pos.y = pooher_pos.y + y_offset;
            if ((mouse_pressed == true) && (timer <= 1.0f))
            {
                pooher_pos.y = pooher_pos.y + (y_offset * 3);
                spriteRenderer.sprite = pooher_images[2];
                spriteRenderer.sortingOrder = 2;
            }
            else
            {
                spriteRenderer.sprite = pooher_images[1];
                spriteRenderer.sortingOrder = 0;
            }
        }

        this.transform.position = pooher_pos;
    }
}
