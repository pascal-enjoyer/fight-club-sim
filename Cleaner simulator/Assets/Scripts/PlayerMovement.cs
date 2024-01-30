using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/*  
    playerBody.GetComponent<Animator>().SetBool("CrouchDownBool", false);
    playerBody.GetComponent<Animator>().SetBool("CrouchStayBool", false);
    playerBody.GetComponent<Animator>().SetBool("CrouchWalkBool", false);
    playerBody.GetComponent<Animator>().SetBool("CrouchUpBool", false);
    playerBody.GetComponent<Animator>().SetBool("StayBool", false);
    playerBody.GetComponent<Animator>().SetBool("WalkBool", false);
    playerBody.GetComponent<Animator>().SetBool("RunBool", false);
    playerBody.GetComponent<Animator>().SetBool("HitBool", false);
    playerBody.GetComponent<Animator>().SetBool("JumpBool", false);
*/
public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Transform playerBody;
    public float crouchingSpeed = 1.5f;
    public Transform mainBone;
    bool isCrouching = false;
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
    bool isJumped;
    // Update is called once per frame
    void Update()
    {
        controller.transform.position = mainBone.transform.position;

        //Перемещение в осях xz
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        //Перемещение по y
        //Нажатие прыжка
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerBody.GetComponent<Animator>().SetBool("CrouchDownBool", false);
            playerBody.GetComponent<Animator>().SetBool("CrouchStayBool", false);
            playerBody.GetComponent<Animator>().SetBool("CrouchWalkBool", false);
            playerBody.GetComponent<Animator>().SetBool("CrouchUpBool", false);
            playerBody.GetComponent<Animator>().SetBool("StayBool", false);
            playerBody.GetComponent<Animator>().SetBool("WalkBool", false);
            playerBody.GetComponent<Animator>().SetBool("RunBool", false);
            playerBody.GetComponent<Animator>().SetBool("HitBool", false);
            playerBody.GetComponent<Animator>().SetBool("JumpBool", true);
            isJumped = true;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        //Проверка падения
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            isJumped = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !isCrouching)
        {

            (speed, crouchingSpeed) = (crouchingSpeed, speed);
            if (Input.GetKey(KeyCode.LeftControl) && (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")))
            {

                playerBody.GetComponent<Animator>().SetBool("CrouchDownBool", false);
                playerBody.GetComponent<Animator>().SetBool("CrouchWalkBool", true);
                playerBody.GetComponent<Animator>().SetBool("CrouchStayBool", false);
            }
            else
            {
                playerBody.GetComponent<Animator>().SetBool("CrouchDownBool", true);
                playerBody.GetComponent<Animator>().SetBool("CrouchUpBool", false);
                playerBody.GetComponent<Animator>().SetBool("StayBool", false);
                playerBody.GetComponent<Animator>().SetBool("WalkBool", false);
                playerBody.GetComponent<Animator>().SetBool("RunBool", false);
                playerBody.GetComponent<Animator>().SetBool("HitBool", false);
                playerBody.GetComponent<Animator>().SetBool("JumpBool", false);
                isCrouching = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && isCrouching)
        {
            isCrouching = false;
            (speed, crouchingSpeed) = (crouchingSpeed, speed);
            playerBody.GetComponent<Animator>().SetBool("CrouchDownBool", false);
            playerBody.GetComponent<Animator>().SetBool("CrouchStayBool", false);
            playerBody.GetComponent<Animator>().SetBool("CrouchWalkBool", false);
            playerBody.GetComponent<Animator>().SetBool("CrouchUpBool", true);
        }
        if (isCrouching)
        {
            playerBody.GetComponent<Animator>().SetBool("CrouchDownBool", false);
            playerBody.GetComponent<Animator>().SetBool("CrouchStayBool", true);
            playerBody.GetComponent<Animator>().SetBool("CrouchWalkBool", false);
            playerBody.GetComponent<Animator>().SetBool("CrouchUpBool", false);
            playerBody.GetComponent<Animator>().SetBool("StayBool", false);
            playerBody.GetComponent<Animator>().SetBool("WalkBool", false);
            playerBody.GetComponent<Animator>().SetBool("RunBool", false);
            playerBody.GetComponent<Animator>().SetBool("HitBool", false);
            playerBody.GetComponent<Animator>().SetBool("JumpBool", false);
        }
        else if (!isCrouching && !isJumped) 
        {
            if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d") && !isJumped)
            {
                if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")))
                {
                    speed = 7f;
                    playerBody.GetComponent<Animator>().SetBool("CrouchDownBool", false);
                    playerBody.GetComponent<Animator>().SetBool("CrouchStayBool", false);
                    playerBody.GetComponent<Animator>().SetBool("CrouchWalkBool", false);
                    playerBody.GetComponent<Animator>().SetBool("CrouchUpBool", false);
                    playerBody.GetComponent<Animator>().SetBool("StayBool", false);
                    playerBody.GetComponent<Animator>().SetBool("WalkBool", false);
                    playerBody.GetComponent<Animator>().SetBool("RunBool", true);
                    playerBody.GetComponent<Animator>().SetBool("HitBool", false);
                    playerBody.GetComponent<Animator>().SetBool("JumpBool", false);
                }
                else
                {
                    speed = 3f;
                    playerBody.GetComponent<Animator>().SetBool("CrouchDownBool", false);
                    playerBody.GetComponent<Animator>().SetBool("CrouchStayBool", false);
                    playerBody.GetComponent<Animator>().SetBool("CrouchWalkBool", false);
                    playerBody.GetComponent<Animator>().SetBool("CrouchUpBool", false);
                    playerBody.GetComponent<Animator>().SetBool("StayBool", false);
                    playerBody.GetComponent<Animator>().SetBool("WalkBool", true);
                    playerBody.GetComponent<Animator>().SetBool("RunBool", false);
                    playerBody.GetComponent<Animator>().SetBool("HitBool", false);
                    playerBody.GetComponent<Animator>().SetBool("JumpBool", false);
                }
            }
            else if (!isCrouching && !isJumped && isGrounded)
            {

                playerBody.GetComponent<Animator>().SetBool("CrouchUpBool", false);
                playerBody.GetComponent<Animator>().SetBool("StayBool", true);
                playerBody.GetComponent<Animator>().SetBool("WalkBool", false);
                playerBody.GetComponent<Animator>().SetBool("RunBool", false);
                playerBody.GetComponent<Animator>().SetBool("HitBool", false);
                playerBody.GetComponent<Animator>().SetBool("JumpBool", false);


            }
        }

    }
}