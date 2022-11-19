using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOn;

    public float cameraMoveSpeed = 5f;

    [SerializeField]
    private GameObject cam;

    [SerializeField]
    private GameObject characterButton;

    [SerializeField]
    private GameObject modeButton;

    [SerializeField]
    private GameObject movementController;

    [SerializeField]
    private GameObject attackController;

    void Start()
    {
        gameOn = false;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            gameOn = true;
        }
        if (gameOn)
        {
            cam.transform.localPosition =
                Vector3
                    .Lerp(cam.transform.localPosition,
                    new Vector3(0, 0, -10),
                    Time.deltaTime * cameraMoveSpeed);
            characterButton.transform.localPosition =
                Vector3
                    .Lerp(characterButton.transform.localPosition,
                    new Vector3(-250, -1200, 0),
                    Time.deltaTime * cameraMoveSpeed);
            modeButton.transform.localPosition =
                Vector3
                    .Lerp(modeButton.transform.localPosition,
                    new Vector3(250, -1200, 0),
                    Time.deltaTime * cameraMoveSpeed);
            StartCoroutine(WaitAndHide());
        }
    }

    private float delay = 1f;

    IEnumerator WaitAndHide()
    {
        yield return new WaitForSeconds(delay);
        characterButton.SetActive(false);
        modeButton.SetActive(false);
        movementController.SetActive(true);
        attackController.SetActive(true);
    }
}
