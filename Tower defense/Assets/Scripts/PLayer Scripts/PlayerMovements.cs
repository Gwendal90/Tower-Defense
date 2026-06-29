using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private Transform cam;

    [SerializeField] private float speed = 5f;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");

        cam = Camera.main.transform;
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();

        if (cam == null) return;

        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = forward * input.y + right * input.x;

        transform.position += moveDir * speed * Time.deltaTime;
    }
}