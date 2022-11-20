using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IDragHandler
{
    private GameObject joystick;

    private Vector2 positionInput;

    private bool touched;

    [SerializeField]
    private float radius;

    public GameObject player;
    private CharacterController characterController;

    [SerializeField]
    private float speed;

    private Vector2 posInput;

    void Start()
    {
        joystick = transform.GetChild(0).gameObject;
        radius = 0.3f;
        touched = false;
        speed = 5;
        characterController = player.GetComponent<CharacterController>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<Image>().rectTransform, eventData.position, eventData.pressEventCamera, out posInput))
        {
            Debug.Log(posInput.x.ToString());
        }
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
                Debug.Log("touched");
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

                float xMovement = joystick.transform.localPosition.x * Time.deltaTime * speed;
                float yMovement = joystick.transform.localPosition.y * Time.deltaTime * speed;


                Vector3 playerMovement = player.transform.right * xMovement + player.transform.up * yMovement;
                characterController.Move(playerMovement);
            }
        }
        else
        {
            joystick.transform.localPosition = new Vector3(0, 0, 0);
            touched = false;
        }
    }
}
