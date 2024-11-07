using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class Movement : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 0.8f;

    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;

    private InputAction move;
    private InputAction jump;
    // private InputAction shoot;

    // Start is called before the first frame update
    void Start()
   
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        move = playerInput.actions["Move"];
        jump = playerInput.actions["Jump"];
        // shoot = playerInput.actions["Shoot"];

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = move.ReadValue<Vector2>();
        Vector3 moveAction = new Vector3(input.x, 0, input.y);

        moveAction = moveAction.x * cameraTransform.right.normalized +
               moveAction.z * cameraTransform.forward.normalized;
        moveAction.y = 0f;

        if (jump.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        controller.Move(moveAction * Time.deltaTime * playerSpeed);

        /*
        if (input.x != 0 || input.y != 0)
        {
            float targetAngle = cameraTransform.eulerAngles.y;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        */
    }
}
