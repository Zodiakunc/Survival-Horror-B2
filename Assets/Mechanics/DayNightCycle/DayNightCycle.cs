using UnityEngine;

using Unity.Cinemachine;
 
public class DayNightCycle : MonoBehaviour

{

    [Header("Time")]

    [Tooltip("How long a full 24-hour cycle lasts in real seconds.")]

    [SerializeField] private float dayDuration = 120f;
 
    [Tooltip("Current time of day (0 - 24)")]

    [Range(0f, 24f)]

    [SerializeField] private float currentTime = 12f;
 
    [Header("Day/Night Settings")]

    [SerializeField] private float dayStartTime = 6f;

    [SerializeField] private float nightStartTime = 18f;
 
    [Header("Lighting")]

    [SerializeField] private Light sun;
 
    [Header("Cameras")]

    [SerializeField] private CinemachineCamera firstPersonCam;

    [SerializeField] private CinemachineCamera thirdPersonCam;
 
    private float timeMultiplier;

    private bool isCurrentlyNight;
 
    // Unity Methods

          private void Awake()

           {

          if (sun == null)

            Debug.LogWarning("No sun assigned.");
 
           if (firstPersonCam == null || thirdPersonCam == null)

            Debug.LogWarning("Cameras not assigned.");

           }
 
           private void Start()

            {

                timeMultiplier = 24f / dayDuration;
 
               // Set initial camera state

               isCurrentlyNight = IsNight();

                ApplyCameraState();

           }
 
          private void Update()

          {

                AdvanceTime();

               UpdateSun();

                UpdateCamera();

           }
 
          // Time Logic

          private void AdvanceTime()

           {

                currentTime += timeMultiplier * Time.deltaTime;

                currentTime %= 24f;

           }
 
           public bool IsNight()

            {

               return currentTime >= nightStartTime || currentTime < dayStartTime;

            }
 
          public float GetCurrentTime()

          {

               return currentTime;

           }
 
          // Sun Logic

          private void UpdateSun()

          {

               if (sun == null) return;
 
               float sunAngle = (currentTime / 24f) * 360f;

               sun.transform.rotation = Quaternion.Euler(sunAngle - 90f, 170f, 0f);
 
               float dot = Vector3.Dot(sun.transform.forward, Vector3.down);

               sun.intensity = Mathf.Clamp01(dot);

           }
 
          // Camera Logic

          private void UpdateCamera()

          {

               bool night = IsNight();
 
               if (night != isCurrentlyNight)

                {

                     isCurrentlyNight = night;

                    ApplyCameraState();

                }

          }
 
    private void ApplyCameraState()

    {

        if (isCurrentlyNight)

        {

            // Night → First Person

            firstPersonCam.Priority = 10;

            thirdPersonCam.Priority = 20;

        }

        else

        {

            // Day → Third Person

            firstPersonCam.Priority = 20;

            thirdPersonCam.Priority = 10
            ;

        }

    }

}
 