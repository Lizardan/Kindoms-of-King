using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float moveSpeedShift = 4f;
    [SerializeField] private float acceleration = 4f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float jumpForceShift = 2f;
    [SerializeField] private float gravity = 8f;
    [SerializeField] private float gravityShift = 5f;
    [SerializeField] private TrailRenderer trail;

    [SerializeField] private Animator animator;

    private CharacterController _controller;
    private float rotationSpeed;
    private float _verticalVelocity;
    private Vector3 currentMove;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {

        // if (Input.GetKeyDown(KeyCode.X))
        // {
        //     bool myresult = await RpcManager.MyAwaiteableGenericRpc("Hello from static RPC");
        //     Debug.Log($"{myresult}");
        // }
        // if (Input.GetKeyDown(KeyCode.C))
        // {
        //     bool myresult = await RpcManager.MyAwaiteableGenericRpc(320);
        //     Debug.Log($"{myresult}");
        // }



        if (Input.GetKey(KeyCode.LeftShift))
            trail.emitting = true;
        else
            trail.emitting = false;

        Vector2 input = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 targetMove = new Vector3(input.x, 0, input.y).normalized;

        currentMove = Vector3.Lerp(currentMove, targetMove, acceleration * Time.deltaTime);

        if (_controller.isGrounded)
            _verticalVelocity = -(trail.emitting ? gravityShift : gravity) * Time.deltaTime;
        else
            _verticalVelocity -= (trail.emitting ? gravityShift : gravity) * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && _controller.isGrounded)
            _verticalVelocity = trail.emitting ? jumpForceShift : jumpForce;

        currentMove.y = _verticalVelocity;

        var actualMoveSpeed = currentMove * (trail.emitting ? moveSpeedShift : moveSpeed);

        _controller.Move(actualMoveSpeed * Time.deltaTime);

        var speed = new Vector3(actualMoveSpeed.x, 0, actualMoveSpeed.z).magnitude;
        animator.SetFloat("Speed", speed);
        //animator.SetBool("IsGrounded", _controller.isGrounded);

        animator.SetFloat("AnimationSpeed", speed / 3);

        if (input != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(currentMove.x, currentMove.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
}