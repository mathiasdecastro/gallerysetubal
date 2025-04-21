using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.InputSystem;

public class VRKeyboardController : MonoBehaviour
{
    public XROrigin xROrigin;
    public float moveSpeed = 2f;
    public float rotateSpeed = 50f;
    private Vector2 movementInput;

    void Update()
    {
        movementInput = Vector2.zero;

        if (Keyboard.current.upArrowKey.isPressed) movementInput.y += 1;
        if (Keyboard.current.downArrowKey.isPressed) movementInput.y -= 1;
        if (Keyboard.current.leftArrowKey.isPressed) movementInput.x -= 1;
        if (Keyboard.current.rightArrowKey.isPressed) movementInput.x += 1;

        Vector3 forward = xROrigin.Camera.transform.forward;
        Vector3 right = xROrigin.Camera.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 move = (forward * movementInput.y + right * movementInput.x) * moveSpeed * Time.deltaTime;
        xROrigin.Camera.transform.position += move;

        if (Input.GetKey(KeyCode.Q))
            xROrigin.Camera.transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            xROrigin.Camera.transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
