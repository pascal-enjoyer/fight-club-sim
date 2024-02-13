using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    public Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isCrouchStayingHash;
    int isCrouchWalkingHash;
    int isJumpingHash;
    int isInAirHash;

    int isRightWalkingHash;
    int isLeftWalkingHash;
    int isBackWalkingHash;
    int isBackCrouchWalkingHash;
    int isBackRunningHash;

    public Transform GroundCheck;
    public LayerMask GroundMask;
    // Start is called before the first frame update
    void Start()
    {
        // улучшение производительности???? (increases perfomance)
        isWalkingHash = Animator.StringToHash("WalkBool");
        isRunningHash = Animator.StringToHash("RunBool");
        isCrouchStayingHash = Animator.StringToHash("CrouchStayBool");
        isCrouchWalkingHash = Animator.StringToHash("CrouchWalkBool");
        isJumpingHash = Animator.StringToHash("JumpBool");
        isInAirHash = Animator.StringToHash("InAirBool");

        isRightWalkingHash = Animator.StringToHash("RightWalkBool");
        isLeftWalkingHash = Animator.StringToHash("LeftWalkBool");
        isBackWalkingHash = Animator.StringToHash("BackWalkBool");
        isBackRunningHash = Animator.StringToHash("BackRunBool");

    }

    // Update is called once per frame
    void Update()
    {

        //Получение информации, проигрывается анимация или нет
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isCrouchStaying = animator.GetBool(isCrouchStayingHash);
        bool isCrouchWalking = animator.GetBool(isCrouchWalkingHash);
        bool isInAir = animator.GetBool(isInAirHash);
        bool isJumping = animator.GetBool(isJumpingHash);
        bool isRightWalking = animator.GetBool(isRightWalkingHash);
        bool isLeftWalking = animator.GetBool(isLeftWalkingHash);
        bool isBackWalking = animator.GetBool(isBackWalkingHash);
        bool isBackRunning = animator.GetBool(isBackRunningHash);

        //Получение информации о нажитии кнопок
        bool OnGround = Physics.CheckSphere(GroundCheck.position, 0.4f, GroundMask);
        //Проверка падения
        bool jumpPressed = Input.GetKeyDown(KeyCode.Space);
        bool runPressed = Input.GetKey("left shift");
        bool crouchPressed = Input.GetKey("left ctrl");
        bool forwardPressed = Input.GetKey("w");
        bool rightPressed = Input.GetKey("d");
        bool leftPressed = Input.GetKey("a");
        bool backPressed = Input.GetKey("s");
        

        #region Ходьба
        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }

        if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if (!isBackWalking && backPressed)
        {
            animator.SetBool(isBackWalkingHash, true);
        }

        if(isBackWalking && !backPressed)
        {
            animator.SetBool(isBackWalkingHash, false);
        }

        if (!isRightWalking && rightPressed)
        {
            animator.SetBool(isRightWalkingHash, true);
        }

        if (isRightWalking && !rightPressed)
        {
            animator.SetBool(isRightWalkingHash, false);
        }

        if (!isLeftWalking && leftPressed)
        {
            animator.SetBool(isLeftWalkingHash, true);
        }

        if (isLeftWalking && !leftPressed)
        {
            animator.SetBool(isLeftWalkingHash, false);
        }

        #endregion

        #region Бег
        if (!isRunning && (forwardPressed || rightPressed || leftPressed ) && runPressed)
        {
            animator.SetBool(isRunningHash, true);
        }

        if (isRunning && ((!forwardPressed && !rightPressed && !leftPressed )|| !runPressed))
        {
            animator.SetBool(isRunningHash, false);
        }
        
        if (!isBackRunning && backPressed && runPressed)
        {
            animator.SetBool(isBackRunningHash, true);
        }

        if (isBackRunning && (!backPressed || !runPressed))
        {
            animator.SetBool(isBackRunningHash, false);
        }
        #endregion

        #region Присяд
        if (!isCrouchStaying && crouchPressed && ((!forwardPressed && !runPressed) || (!forwardPressed && runPressed)))
        {
            animator.SetBool(isCrouchStayingHash, true);
        }
        if (isCrouchStaying && !crouchPressed && ((forwardPressed || runPressed) || (!forwardPressed && !runPressed)))
        {
            animator.SetBool(isCrouchStayingHash, false);
        }
        #endregion

        #region Ходьба в приседе
        
        if (!isCrouchWalking && crouchPressed && ((leftPressed || (leftPressed && runPressed))
            || (rightPressed || (rightPressed && runPressed))
            || (backPressed || (backPressed && runPressed))
            || (forwardPressed || (forwardPressed && runPressed))))
        {
            
            animator.SetBool(isCrouchWalkingHash, true);
            if (backPressed)
            {
                animator.SetFloat("CrouchAnimationSpeed", -1);
            }
        }
        if (isCrouchWalking && (!crouchPressed || (crouchPressed && ((!rightPressed && !leftPressed && !forwardPressed && !backPressed) 
            || (!rightPressed && !leftPressed && !forwardPressed && !backPressed && runPressed)))))
        {
            animator.SetBool(isCrouchWalkingHash, false);
            if (animator.GetFloat("CrouchAnimationSpeed") == -1)
            {
                animator.SetFloat("CrouchAnimationSpeed", 1);
            }
        }

        #endregion

        #region Прыжок и падение

        if (!isJumping && jumpPressed && !crouchPressed)
        {
            animator.SetBool(isJumpingHash, true);
        }
        if (isJumping && !jumpPressed)
        {
            animator.SetBool(isJumpingHash, false);
        }

        #endregion 

        #region В воздухе

        if (!OnGround && !isInAir)
        {
            animator.SetBool(isInAirHash, true);
        }
        if (isInAir && OnGround)
        {
            animator.SetBool(isInAirHash, false);
        }

        #endregion

    }
}