using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
   [Header("Time")]
   [Tooltip("How long a full 24-hour cycle lasts in real seconds")]
   [SerializeField] private float dayDuration = 120f;

   [Tooltip("Current time of day (0 - 24)")]
   [Range(0f, 24f)]
   [SerializeField] private float currentTime = 12f;

   [Header("Lighting")]
   [SerializeField] private Light sun;

   private float timeMultiplier;

   private void Awake()
   {
        if (sun == null)
        {
            Debug.LogWarning("No sun assigned.");
        }
   }

   private void Start()
   {
        timeMultiplier = 24f / dayDuration;
   }

   private void Update()
   {
        AdvanceTime();
        UpdateSun();
   }

   private void AdvanceTime()
   {
        currentTime += timeMultiplier * Time.deltaTime;

        currentTime %= 24f;
   }

   public bool IsNight()
   {
        return currentTime >=18f || currentTime < 6f;
   }

   public float GetCurrentTime()
   {
        return currentTime;
   }

   private void UpdateSun()
   {
        if (sun == null) return;

        float sunAngle = (currentTime / 24f) * 360f;

        sun.transform.rotation = Quaternion.Euler(sunAngle - 90f, 170f, 0f);

        float dot = Vector3.Dot(sun.transform.forward, Vector3.down);
        sun.intensity = Mathf.Clamp01(dot);
   }
}
