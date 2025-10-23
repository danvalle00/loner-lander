using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{

    public InputSystem_Actions playerController;

    private Rigidbody2D rb;
    private Animator animator;

    [Header("Movement Settings")]
    [SerializeField] private float thrustForce = 5f;
    [SerializeField] private float rotationStepAngle = 15f; // 15 graus na rotacao qnd apertar o input
    [Header("Fuel Settings")]
    [SerializeField] private float fuelConsumptionRate = 3f;

    [Header("Input Debug")]
    [SerializeField] private float thrustInput;
    [SerializeField] private float rotationInput;

    [Header("Speed")]
    [SerializeField] private string horizontalSpeed;
    [SerializeField] private string verticalSpeed;

    private bool stepReady = true;
    private bool inputsEnabled = true;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = new InputSystem_Actions();
        animator = GetComponent<Animator>();

    }
    void Start()
    {
        if (GravityManager.Instance == null)
        {
            return;
        }

        float gravityScale = GravityManager.Instance.GetCurrentGravityScale();
        rb.gravityScale = gravityScale;
    }
    void OnEnable()
    {
        if (inputsEnabled)
        {
            playerController.Player.Enable();
        }

    }
    void OnDisable()
    {
        playerController.Player.Disable();
    }
    void Update()
    {
        HandlerAnimator();
    }
    void FixedUpdate()
    {
        if (!inputsEnabled)
        {
            rotationInput = 0f;
            thrustInput = 0f;
            return;
        }
        rotationInput = playerController.Player.Rotate.ReadValue<float>();
        thrustInput = playerController.Player.Thrust.ReadValue<float>();
        HandlerThrust();
        HandlerRotation();
        UpdateSpeed();
    }

    private void HandlerRotation()
    {
        if (rotationInput != 0f && stepReady)
        {
            // angulo atual + 15 graus pra direita ou esquerda
            rb.MoveRotation(rb.rotation + (-rotationInput * rotationStepAngle));
            stepReady = false;
        }
        else if (rotationInput == 0f)
        {
            stepReady = true;
        }
    }

    private void HandlerThrust()
    {
        if (thrustInput > 0f && FuelManager.Instance.GetRemainingFuel() > 0f) // thrustInput se n for 0 vai ser 1 entao
        {
            Vector2 force = transform.up * thrustForce * Time.fixedDeltaTime;
            rb.AddForce(force, ForceMode2D.Force);
            float fuelConsumption = thrustInput * Time.fixedDeltaTime * fuelConsumptionRate;
            FuelManager.Instance.ConsumeFuel(fuelConsumption);
        }
    }


    private void HandlerAnimator()
    {
        animator.SetFloat("thrustInput", thrustInput);

    }
    private void UpdateSpeed()
    {
        horizontalSpeed = string.Format("{0:F1}", Mathf.Abs(rb.linearVelocityX) * 100);
        verticalSpeed = string.Format("{0:F1}", Mathf.Abs(rb.linearVelocityY) * 100);
    }

    public string GetHorizontalSpeed()
    {
        return horizontalSpeed;
    }
    public string GetVerticalSpeed()
    {
        return verticalSpeed;
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            GameManager.Instance.Lose();
        }
    }

    public void DisableInputs()
    {
        inputsEnabled = false;
        playerController.Player.Disable();
        rotationInput = 0f;
        thrustInput = 0f;
    }
    public void EnableInputs()
    {
        inputsEnabled = true;
        playerController.Player.Enable();
    }
}


