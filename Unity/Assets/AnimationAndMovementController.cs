using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets.Utility;

public class AnimationAndMovementController : MonoBehaviour
{

    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;

    
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    bool isMovementPressed;
    float rotationFactorPerFrame = 15f;
    float runMultiplier = 3.0f;
    bool isRunPressed;
    public Transform camera;

    float gravity = -9.8f;
    float groundedGravity = -.05f;

    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 4.0f;
    float maxJumpTime = 0.75f;
    bool isJumping = false;
    bool isJumpAnimating = false;
    //int jumpCount = 0;
    //Dictionary<int, float> initialJumpVelocities = new Dictionary<int, float>();
    //Dictionary<int, float> jumpGravities = new Dictionary<int, float>();
    //Coroutine currentJumpResetRoutine = null;


    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;
        playerInput.CharacterControls.Jump.started += onJump;
        playerInput.CharacterControls.Jump.canceled += onJump;


        setupJumpVariables();

    }

    void handleJump()
    {
        if(!isJumping && characterController.isGrounded && isJumpPressed)
        {
            //if (jumpCount < 3 && currentJumpResetRoutine != null)
            //{
            //    StopCoroutine(currentJumpResetRoutine);
            //}
            animator.SetBool(isJumpingHash, true);
            isJumpAnimating = true;
            isJumping = true;
            //jumpCount += 1;
            currentMovement.y = initialJumpVelocity * .5f;
            currentRunMovement.y = initialJumpVelocity * .5f;
        }else if (!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }
    }

    void setupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
        //float secondJumpGravity = (-2 * (maxJumpHeight + 2)) / Mathf.Pow((timeToApex * 1.25f), 2);
        //float secondJumpInitialVelocity = (2 * (maxJumpHeight + 2)) / (timeToApex * 1.25f);
        //float thirdJumpGravity = (-2 * (maxJumpHeight + 4)) / Mathf.Pow((timeToApex * 1.5f), 2);
        //float thirdJumpInitialVelocity = (2 * (maxJumpHeight + 4)) / (timeToApex * 1.5f);

        //initialJumpVelocities.Add(1, initialJumpVelocity);
        //initialJumpVelocities.Add(2, secondJumpInitialVelocity);
        //initialJumpVelocities.Add(3, thirdJumpInitialVelocity);

        //jumpGravities.Add(0, gravity);
        //jumpGravities.Add(1, gravity);
        //jumpGravities.Add(2, secondJumpGravity);
        //jumpGravities.Add(3, thirdJumpGravity);


    }

    //IEnumerator jumpResetRoutine()
    //{
    //    yield return new WaitForSeconds(.5f);
    //    jumpCount = 0;
    //}

    void onJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();

    }

    void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }


    void onMovementInput(InputAction.CallbackContext context)
    {
        Quaternion cam = Quaternion.AngleAxis(camera.rotation.eulerAngles.y, Vector3.up);

        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        currentMovement = cam * currentMovement;
        currentRunMovement.x = currentMovementInput.x * runMultiplier;
        currentRunMovement.z = currentMovementInput.y * runMultiplier;
        currentRunMovement = cam * currentRunMovement;

        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;

    }

    

    void handleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        if (isMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }else if (!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if((isMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }

    }

    void handleRotation()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;
        Quaternion currentRotation = transform.rotation;
        

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }
    

    void handleGravity()
    {
        bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
        float fallMultiplier = 2.0f;

        if (characterController.isGrounded)
        {
            if (isJumpAnimating)
            {
                animator.SetBool(isJumpingHash, false);
                isJumpAnimating = false;
                //currentJumpResetRoutine = StartCoroutine(jumpResetRoutine());

            }
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }else if (isFalling)
        {
            float previousYVelocity = currentMovement.y;
            float newYVelocity = currentMovement.y + (gravity * fallMultiplier * Time.deltaTime);
            float nextYVelocity = Mathf.Max((previousYVelocity + newYVelocity) * .5f, -20.0f);
            currentMovement.y = nextYVelocity;
            currentRunMovement.y = nextYVelocity;
        }
        else
        {
            float previousYVelocity = currentMovement.y;
            float newYVelocity = currentMovement.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            currentMovement.y = nextYVelocity;
            currentRunMovement.y = nextYVelocity;
            
        }
    }
    void Update()
    {

        handleRotation();
        handleAnimation();

        if (isRunPressed)
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }
        handleGravity();
        handleJump();


    }

    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}
