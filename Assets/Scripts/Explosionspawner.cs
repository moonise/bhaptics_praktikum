using UnityEngine;

public class ExplosionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;

    void Start()
    {
        InvokeRepeating(nameof(SpawnExplosion), 0f, 2f);
    }

    void SpawnExplosion()
    {
        Transform player = Camera.main.transform;

        // Random point on a ring 1-3 m around the player
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float radius = Random.Range(1f, 3f);

        Vector3 position = new Vector3(
            player.position.x + Mathf.Cos(angle) * radius,
            Random.Range(0f, 2f),
            player.position.z + Mathf.Sin(angle) * radius);

        Instantiate(explosionPrefab, position, Quaternion.identity);
    }
}