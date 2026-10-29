using UnityEngine;
using Bhaptics.SDK2;

public class HapticExplosion : MonoBehaviour
{
    private const float MaxDistance = 5f;

    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;

        float distance = Vector3.Distance(cam.position, transform.position);
        float angleX = CalculateAngle();
        float offsetY = CalculateOffsetY();
        float intensity = CalculateIntensity(distance);

        // eventId, startMillis, intensity, duration, angleX, offsetY
        BhapticsLibrary.Play("impact_vest", 0, intensity, 1f, angleX, offsetY);

        Destroy(gameObject, 2f);
    }

    // Angle from the player's viewing direction to the explosion, 0-360
    float CalculateAngle()
    {
        Vector3 forward = Vector3.ProjectOnPlane(cam.forward, Vector3.up);
        Vector3 toExplosion = Vector3.ProjectOnPlane(transform.position - cam.position, Vector3.up);

        float angle = Vector3.SignedAngle(forward, toExplosion, Vector3.up); // -180..180
        return (angle + 360f) % 360f;
    }

    // Height difference between explosion and camera, clamped to the vest's range
    float CalculateOffsetY()
    {
        float diff = transform.position.y - cam.position.y;
        return Mathf.Clamp(diff, -0.5f, 0.5f);
    }

    // 1 right at the player, 0 at 5 m or more
    float CalculateIntensity(float distance)
    {
        return Mathf.Clamp01(1f - distance / MaxDistance);
    }
}