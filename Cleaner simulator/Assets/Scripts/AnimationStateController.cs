using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    public Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isCrouchStayingHash;
    int isCrouchWalkingHash;
    // Start is called before the first frame update
    void Start()
    {
        // улучшение производительности???? (increases perfomance)
        isWalkingHash = Animator.StringToHash("WalkBool");
        isRunningHash = Animator.StringToHash("RunBool");
        isCrouchStayingHash = Animator.StringToHash("CrouchStayBool");
        isCrouchWalkingHash = Animator.StringToHash("CrouchWalkBool");

    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isCrouchStaying = animator.GetBool(isCrouchStayingHash);
        bool isCrouchWalking = animator.GetBool(isCrouchWalkingHash);
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");
        bool crouchPressed = Input.GetKey("left ctrl");

        #region Ходьба
        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }

        if (isWalking && !forwardPressed) 
        { 
            animator.SetBool(isWalkingHash, false);
        }
        #endregion

        #region Бег
        if (!isRunning && forwardPressed && runPressed)
        {
            animator.SetBool(isRunningHash, true);
        }

        if(isRunning && (!forwardPressed || !runPressed))
        {
            animator.SetBool(isRunningHash, false);
        }
        #endregion

        #region Присяд
        if (!isCrouchStaying && crouchPressed && !forwardPressed && !runPressed)
        {
            animator.SetBool(isCrouchStayingHash, true);
        }
        if(isCrouchStaying && !crouchPressed && ((forwardPressed || runPressed) || (!forwardPressed && !runPressed)))
        {
            animator.SetBool(isCrouchStayingHash, false);
        }
        #endregion

        #region Ходьба в приседе

        if (!isCrouchWalking && crouchPressed && (forwardPressed || (forwardPressed && runPressed))))
        {
            animator.SetBool(isCrouchWalkingHash, true);
        }
        if(isCrouchWalking && ((crouchPressed && (!forwardPressed && !runPressed)) || !crouchPressed))
        {
            animator.SetBool(isCrouchWalkingHash, false);
        }

        #endregion
    }
}
