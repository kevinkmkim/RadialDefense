using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 40f;

    public void Awake()
    {
        StartCoroutine(InitializeLocation());
    }

    public IEnumerator InitializeLocation()
    {
        // In Unity Remote 5 (takes time to load)
        yield return new WaitForSeconds(5);

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("location disabled by user");
            yield break;
        }

        // enable compass
        Input.compass.enabled = true;

        // start the location service
        Debug.Log("start location service");
        Input.location.Start();

        // Wait until service initializes
        int maxSecondsToWaitForLocation = 20;
        while (Input.location.status == LocationServiceStatus.Initializing &&
            maxSecondsToWaitForLocation > 0
        )
        {
            yield return new WaitForSeconds(1);
            maxSecondsToWaitForLocation--;
            Debug.Log (maxSecondsToWaitForLocation);
        }

        // Service didn't initialize in 20 seconds
        if (maxSecondsToWaitForLocation < 1)
        {
            Debug.Log("location service timeout");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("unable to determine device location");
            yield break;
        }
        Debug.Log("location service loaded");
        yield break;
    }

    void Start()
    {
        Input.location.Start();
        Input.compass.enabled = true;
    }

    void Update()
    {
        transform.rotation =
            Quaternion.Euler(0, 0, -Input.compass.magneticHeading);

        // movement = joystick * runSpeed;
    }
}
