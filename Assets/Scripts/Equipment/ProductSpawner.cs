using UnityEngine;

public class ProductSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject productPrefab;
    [SerializeField] private Transform spawnPoint;

    [Header("Spawning")]
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private int maximumProducts = 10;

    private float spawnTimer;

    private void Update()
    {
        if (ConveyorTwinData.Instance == null ||
            !ConveyorTwinData.Instance.IsConveyorOperational())
        {
            return;
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            SpawnProduct();
        }
    }

    private void SpawnProduct()
    {
        if (productPrefab == null || spawnPoint == null)
        {
            Debug.LogWarning("Product prefab or spawn point is missing.");
            return;
        }

        GameObject[] existingProducts =
            GameObject.FindGameObjectsWithTag("Product");

        if (existingProducts.Length >= maximumProducts)
        {
            return;
        }

        Instantiate(
            productPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );
    }
}