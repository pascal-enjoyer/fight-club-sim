using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Transform playerBody;

    public Transform mainBone;

    //Скорость ходьбы
    public float speed = 3f;
    //Гравитация
    public float gravity = -9.8f;
    public float groundDistance = 0.4f;
    //Высота прыжка
    public float jumpHeight = 1f;
    //Ускорение
    Vector3 velocity;
    //Проверка на состояние падения
    bool isGrounded = true;
    // Update is called once per frame
    void Update()
    {
        bool runPressed = Input.GetKey("left shift");
        bool forwardPressed = Input.GetKey("w");
        bool backwardPressed = Input.GetKey("s");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");

        bool crouchDownPressed = Input.GetKeyDown(KeyCode.LeftControl);
        bool crouchUpPressed = Input.GetKeyUp(KeyCode.LeftControl);
        bool crouchPressed = Input.GetKey(KeyCode.LeftControl);
        bool jumpPresseed = Input.GetKey(KeyCode.Space);
        controller.transform.position = mainBone.transform.position;

        //Перемещение в осях xz
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        //Перемещение по y
        //Нажатие прыжка
        if (jumpPresseed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        //Проверка падения
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (crouchDownPressed)
        {
            controller.height = 7f;
            controller.height = 6f;
            controller.height = 5f;
            controller.height = 4f;
            controller.center /= 2f;
            speed = 1.5f;
        }
        if (crouchUpPressed)
        {
            controller.height = 5f;
            controller.height = 6f;
            controller.height = 7f;
            controller.height = 8f;
            controller.center *= 2;
            speed = 3f;
        }
        if ((forwardPressed || backwardPressed || leftPressed || rightPressed) && !crouchPressed)
        {
            if (runPressed)
            {
                speed = 7f;
            }
            else
            {
                speed = 3f;
            }
        }
    }
}