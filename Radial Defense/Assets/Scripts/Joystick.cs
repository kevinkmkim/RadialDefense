using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour
{
    private GameObject joystick;

    private Vector2 positionInput;

    private bool touched;

    [SerializeField]
    private float radius;

    void Start()
    {
        joystick = transform.GetChild(0).gameObject;
        radius = 0.3f;
        touched = false;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition =
                Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 touchPosition2D =
                new Vector2(touchPosition.x, touchPosition.y);

            RaycastHit2D hit =
                Physics2D
                    .Raycast(touchPosition2D, Camera.main.transform.forward);
            if (
                hit.collider != null &&
                hit.transform.gameObject.name == "Movement Controller"
            )
            {
                touched = true;
            }

            if (touched)
            {
                touchPosition.z = 0f;
                joystick.transform.position = touchPosition;
                float vectorLength = joystick.transform.localPosition.magnitude;
                if (vectorLength > radius)
                {
                    joystick.transform.localPosition /= vectorLength / radius;
                }
            }
        }
        else
        {
            joystick.transform.localPosition = new Vector3(0, 0, 0);
            touched = false;
        }
    }
}
