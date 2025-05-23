using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : Character
{
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    [Header("Gravity Settings")]
    public float gravity = -15f;
    public float terminalVelocity = 53f;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        base.Update();
    }

    private void HandleMovement()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, -terminalVelocity);
        controller.Move(velocity * Time.deltaTime);
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount * dpsMultiplier);
    }
}

