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
    public Transform GroundCheck;
    public LayerMask GroundMask;
    // Start is called before the first frame update
    void Start()
    {
        // ��������� ������������������???? (increases perfomance)
        isWalkingHash = Animator.StringToHash("WalkBool");
        isRunningHash = Animator.StringToHash("RunBool");
        isCrouchStayingHash = Animator.StringToHash("CrouchStayBool");
        isCrouchWalkingHash = Animator.StringToHash("CrouchWalkBool");
        isJumpingHash = Animator.StringToHash("JumpBool");
        isInAirHash = Animator.StringToHash("FeltBool");

    }

    // Update is called once per frame
    void Update()
    {

        //��������� ����������, ������������� �������� ��� ���
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isCrouchStaying = animator.GetBool(isCrouchStayingHash);
        bool isCrouchWalking = animator.GetBool(isCrouchWalkingHash);
        bool isInAir = animator.GetBool(isInAirHash);
        bool isJumping = animator.GetBool(isJumpingHash);


        //��������� ���������� � ������� ������
        bool OnGround = Physics.CheckSphere(GroundCheck.position, 0.4f, GroundMask);
        //�������� �������
        bool jumpPressed = Input.GetKeyDown(KeyCode.Space);
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");
        bool crouchPressed = Input.GetKey("left ctrl");

        #region ������
        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }

        if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }
        #endregion

        #region ���
        if (!isRunning && forwardPressed && runPressed)
        {
            animator.SetBool(isRunningHash, true);
        }

        if (isRunning && (!forwardPressed || !runPressed))
        {
            animator.SetBool(isRunningHash, false);
        }
        #endregion

        #region ������
        if (!isCrouchStaying && crouchPressed && ((!forwardPressed && !runPressed) || (!forwardPressed && runPressed)))
        {
            animator.SetBool(isCrouchStayingHash, true);
        }
        if (isCrouchStaying && !crouchPressed && ((forwardPressed || runPressed) || (!forwardPressed && !runPressed)))
        {
            animator.SetBool(isCrouchStayingHash, false);
        }
        #endregion

        #region ������ � �������

        if (!isCrouchWalking && crouchPressed && (forwardPressed || (forwardPressed && runPressed)))
        {
            animator.SetBool(isCrouchWalkingHash, true);
        }
        if (isCrouchWalking && ((crouchPressed && (!forwardPressed && !runPressed)) || !crouchPressed || (crouchPressed && runPressed && !forwardPressed)))
        {
            animator.SetBool(isCrouchWalkingHash, false);
        }

        #endregion

        #region ������

        if (!isJumping && jumpPressed && !crouchPressed)
        {
            animator.SetBool(isJumpingHash, true);
        }
        if (isJumping && !jumpPressed)
        {
            animator.SetBool(isJumpingHash, false);
        }

        #endregion

        #region �������

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