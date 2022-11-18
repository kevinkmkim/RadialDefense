using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour
{
    private GameObject joystick;
    private Vector2 positionInput;
    [SerializeField]
    private float radius;

    void Start()
    {
        joystick = transform.GetChild(0).gameObject;
        radius = 0.5f;
    }
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f;
            joystick.transform.position = touchPosition;
            float vectorLength = joystick.transform.localPosition.magnitude;
            Debug.Log("Vector Length " + vectorLength);
            Debug.Log("Radius " + radius);
            if (vectorLength > radius)
            {
                joystick.transform.localPosition /= vectorLength / radius;
                // Debug.Log("Exceeded");
            }
        }
        else
        {
            joystick.transform.localPosition = new Vector3(0,0,0);
        }
    }
}
