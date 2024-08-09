using UnityEngine;

public class AIMovementNormal : MonoBehaviour
{
    public float spawningDistance;
    public int numberOfGameObjects;
    public GameObject prefab; // The GameObject prefab to instantiate.
    public Vector3 targetPosition; // The position to which the GameObjects will move.
    public float minSpeed = 1f; // Minimum speed for lerp movement.
    public float maxSpeed = 5f; // Maximum speed for lerp movement.

    private GameObject[] gameObjects; // Array to hold instantiated GameObjects.

    private void Start()
    {
        InstantiateGameObjects();
    }

    private void InstantiateGameObjects()
    {
        gameObjects = new GameObject[numberOfGameObjects];

        for (int i = 0; i < gameObjects.Length; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-spawningDistance, spawningDistance), 0f, Random.Range(-spawningDistance, spawningDistance));
            gameObjects[i] = Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
    }

    private void Update()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            float speed = Random.Range(minSpeed, maxSpeed);
            float step = speed * Time.deltaTime;
            gameObjects[i].transform.position = Vector3.MoveTowards(gameObjects[i].transform.position, targetPosition, step);

            // if (Vector3.Distance(gameObjects[i].transform.position, targetPosition) < 0.01f)
            // {
            //     Destroy(gameObjects[i]);
            // }
        }
    }
}
