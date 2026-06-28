using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction moveAction;

    [SerializeField] float speed = 5;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
{
    Vector2 input = moveAction.ReadValue<Vector2>();

    // Get camera reference
    Transform cam = Camera.main.transform;

    // Camera forward/right, flattened (ignore vertical tilt)
    Vector3 forward = cam.forward;
    Vector3 right = cam.right;

    forward.y = 0;
    right.y = 0;

    forward.Normalize();
    right.Normalize();

    // Convert input into world direction relative to camera
    Vector3 moveDir = forward * input.y + right * input.x;

    transform.position += moveDir * speed * Time.deltaTime;
}
}