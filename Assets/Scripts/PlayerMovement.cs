/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using static EventManager;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpForce;
    [SerializeField] private float sensitivity;
    [SerializeField] private Transform feetTransform;
    [SerializeField] private LayerMask floorMask;

    private Vector3 playerMovementInput;
    private Vector2 playerMouseInput;

    private Rigidbody rb;
    public float gravityScale = 5;
    public float turnSmoothTime = 0.1f;
    private bool isGrounded;
    float turnSmoothVelocity;
    public Transform cam;

    float horizontal;
    float vertical;

    private bool canJump, goesBack = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");


        isGrounded = Physics.CheckSphere(feetTransform.position, 0.1f, floorMask);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            canJump = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            goesBack = true;
        }
/*
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.D))
        {
            rotateMove = true;
        }*//*
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "goal")
        {
            OnTimerStop();
            OnFinishSuccess();
        }

        if (collision.gameObject.tag == "ChooseDoor")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }
    private void MovePlayer()
    {
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);

        playerMovementInput = new Vector3(horizontal, 0f, vertical);


        if (goesBack)
        {
            Vector3 moveDirection = transform.TransformDirection(playerMovementInput) * speed;
            rb.velocity = moveDirection.normalized * speed;
            goesBack = false;
        }
        else if (playerMovementInput.magnitude >= 0.1f)
        {/*
            if (rotateMove)
            {
                transform.Rotate(0f, playerMovementInput.x * sensitivity, 0f);
            }*//*
            float targetAngle = Mathf.Atan2(playerMovementInput.x, playerMovementInput.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * speed;
            rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
        }

        if (canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }
    }
    /*
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}*/