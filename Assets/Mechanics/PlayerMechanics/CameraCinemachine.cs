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
    public Transform cameraPivot;

    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        HandleCameraSwitch();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Always rotate player left/right
        playerBody.Rotate(Vector3.up * mouseX);

        
        
    }

    void HandleCameraSwitch()
    {
        if (Input.GetKey(KeyCode.Alpha1)) // first person
        {
            firstPersonCam.Priority = 10;
            thirdPersonCam.Priority = 5;
        }

        if (Input.GetKey(KeyCode.Alpha3)) // third person
        {
            firstPersonCam.Priority = 5;
            thirdPersonCam.Priority = 10;
        }
    }
}