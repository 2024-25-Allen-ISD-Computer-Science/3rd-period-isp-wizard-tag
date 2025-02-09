using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;


public class playerController : MonoBehaviour
{
    private SpecialControls controls;

    public pauseMenu pauseMenu;

    private float horizontal;
    public float speed;
    public float jumpingPower;
    private bool isFacingRight = true;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBuffer = 0.1f;
    private float jumpBufferCounter;

    private bool canDash = true;
    private bool isDashing;
    public float dashingPower;
    public float dashingTime;
    private float dashingCooldown;

    private bool isWallSliding;
    public float wallSlidingSpeed;

    private bool isWallJumping;
    private float wallJumpingDirrection;
    public float wallJumpingTime;
    private float wallJumpingCounter;
    public float wallJumpingDuration;
    private Vector2 wallJumpingPower = new Vector2(9f, 18f);

    private PlayerInput playerInput; // Reference to Player Input
    private InputActionMap assignedActionMap; // Action map assigned to this player

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private TrailRenderer tr;

    private void Awake()
    {

        playerInput = GetComponent<PlayerInput>();

        // Assign the action map dynamically from PlayerInput
        assignedActionMap = playerInput.currentActionMap;
        assignedActionMap.Enable();

        Debug.Log($"Player {playerInput.playerIndex} joined with action map: {assignedActionMap.name}");


        // Set the appropriate action map
        assignedActionMap.Enable();

        Debug.Log($"Player {playerInput.playerIndex} using action map: {assignedActionMap}");

    }

    void OnEnable()
    {
        assignedActionMap?.Enable();
    }

    void OnDisable()
    {
        assignedActionMap?.Disable();
    }
    void Update()
    {
        // Access static member with the class name
        if (pauseMenu.sharedInstance?.isPaused == true || SpinWheel.isSpinning) return;

        if (pauseMenu.sharedInstance?.justResumed == true)
        {
            jumpBufferCounter = 0f;
            pauseMenu.sharedInstance.justResumed = false;
            return;
        }

        if (isDashing) return;

        if (IsGrounded()) coyoteTimeCounter = coyoteTime;
        else coyoteTimeCounter -= Time.deltaTime;

        if (playerInput.actions["jump"].triggered) jumpBufferCounter = jumpBuffer;
        else jumpBufferCounter -= Time.deltaTime;

        horizontal = playerInput.actions["move"].ReadValue<float>();

        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpBufferCounter = 0f;
        }

        if (playerInput.actions["jump"].phase == InputActionPhase.Canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }

        if (playerInput.actions["dash"].triggered && canDash)
        {
            StartCoroutine(Dash());
        }

        wallSlide();
        wallJump();
        if (!isWallJumping) Flip();
    }


    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private void activateDash()
    {
        if (canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        while (IsGrounded() == false) { 
            yield return null;
        }
        canDash = true;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
    }

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void wallSlide()
    {
        if (isWalled() && !IsGrounded() && horizontal != 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void wallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirrection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(stopWallJumping));
        } 
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (assignedActionMap["jump"].triggered && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirrection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirrection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(stopWallJumping), wallJumpingDuration);
        }

    }
    private void stopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

}
