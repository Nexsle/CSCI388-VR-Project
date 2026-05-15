using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MovementCollisionFix : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _gravity = -9.81f;

    private CharacterController _controller;
    private Transform _cameraTransform;
    private float _verticalVelocity;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    void Update()
    {
        Vector2 input = Vector2.zero;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed) input.y += 1;
            if (Keyboard.current.sKey.isPressed) input.y -= 1;
            if (Keyboard.current.dKey.isPressed) input.x += 1;
            if (Keyboard.current.aKey.isPressed) input.x -= 1;
        }

        Vector3 camForward = Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(_cameraTransform.right, Vector3.up).normalized;

        Vector3 moveDirection = camForward * input.y + camRight * input.x;
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f) * _moveSpeed * Time.deltaTime;

        if (_controller.isGrounded)
            _verticalVelocity = -0.5f;
        else
            _verticalVelocity += _gravity * Time.deltaTime;

        moveDirection.y = _verticalVelocity * Time.deltaTime;

        _controller.Move(moveDirection);
    }
}