using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 150f;

    private InputSystem_Actions controls;


    private void Awake()
    {
        controls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }
    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (controls == null) return;
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>();
        float horizontalInput = moveInput.x * -1; // Move left or right
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
