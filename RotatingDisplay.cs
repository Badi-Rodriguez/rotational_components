using UnityEngine;

public class RotatingDisplay : MonoBehaviour
{
    [Header("Idle Rotation Settings")]
    public float idleRotationSpeed = 10f; // degrees per second

    [Header("User Rotation Settings")]
    public float userRotationSpeed = 100f; // degrees per second when rotating by user input

    private float targetRotationX = 0f; // vertical rotation (up/down)
    private float targetRotationY = 0f; // horizontal rotation (left/right)

    private float currentRotationX = 0f;
    private float currentRotationY = 0f;

    private bool rotatingUp, rotatingDown, rotatingLeft, rotatingRight;

    void Update()
    {
        // Idle rotation around Y when no user input on horizontal
        if (!rotatingLeft && !rotatingRight)
        {
            targetRotationY += idleRotationSpeed * Time.deltaTime;
        }

        // Apply user input rotation
        if (rotatingUp)
            targetRotationX -= userRotationSpeed * Time.deltaTime;
        if (rotatingDown)
            targetRotationX += userRotationSpeed * Time.deltaTime;
        if (rotatingLeft)
            targetRotationY -= userRotationSpeed * Time.deltaTime;
        if (rotatingRight)
            targetRotationY += userRotationSpeed * Time.deltaTime;

        // Clamp vertical rotation to avoid flipping (e.g., -60 to 60 degrees)
        targetRotationX = Mathf.Clamp(targetRotationX, -60f, 60f);

        // Smoothly interpolate current rotation to target rotation
        currentRotationX = Mathf.Lerp(currentRotationX, targetRotationX, 5f * Time.deltaTime);
        currentRotationY = Mathf.Lerp(currentRotationY, targetRotationY, 5f * Time.deltaTime);

        // Apply rotation
        transform.rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0f);
    }

    // These functions will be called by the UI buttons on press/release
    public void OnPressUp()    => rotatingUp = true;
    public void OnReleaseUp()  => rotatingUp = false;

    public void OnPressDown()  => rotatingDown = true;
    public void OnReleaseDown()=> rotatingDown = false;

    public void OnPressLeft()  => rotatingLeft = true;
    public void OnReleaseLeft()=> rotatingLeft = false;

    public void OnPressRight() => rotatingRight = true;
    public void OnReleaseRight()=> rotatingRight = false;
}
