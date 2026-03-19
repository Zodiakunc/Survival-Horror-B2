using UnityEngine;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    [Header("Cameras")]
    public CinemachineCamera firstPersonCam;
    public CinemachineCamera thirdPersonCam;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    [Header("Time")]
    public DayNightCycle dayNightCycle; // Reference your day-night manager
    public Transform cameraPivot;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        SwitchCameraByTime();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        if (firstPersonCam.Priority > thirdPersonCam.Priority)
        {
            // First-person: rotate camera + player
            firstPersonCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
        else
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -40f, 80f);
            
            cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    void SwitchCameraByTime()
    {
        if (dayNightCycle == null) return;

        if (!dayNightCycle.IsNight())
        {
            // Daytime: first-person active
            firstPersonCam.Priority = 10;
            thirdPersonCam.Priority = 5;
        }
        else
        {
            // Nighttime: third-person active
            firstPersonCam.Priority = 5;
            thirdPersonCam.Priority = 10;
        }
    }
}