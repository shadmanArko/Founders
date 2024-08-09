using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class AIPrefabScript : MonoBehaviour
{
    public ComputeShader aiMovementShader;
    public GameObject aiPrefab;
    public int aiCount = 10000;
    public float moveSpeed = 1f;
    public float moveRange = 100f;

    private ComputeBuffer aiPositionsBuffer;
    private Vector3[] aiPositions;
    private List<GameObject> aiGameObjects;

    void Start()
    {
        // Initialize AI positions and game objects
        aiPositions = new Vector3[aiCount];
        aiGameObjects = new List<GameObject>();

        for (int i = 0; i < aiCount; i++)
        {
            aiPositions[i] = new Vector3(Random.Range(-moveRange, moveRange), 0, Random.Range(-moveRange, moveRange));
            GameObject ai = Instantiate(aiPrefab, aiPositions[i], Quaternion.identity);
            aiGameObjects.Add(ai);
        }

        // Create compute buffer
        aiPositionsBuffer = new ComputeBuffer(aiCount, sizeof(float) * 3);
        aiPositionsBuffer.SetData(aiPositions);
        
        // Set up the compute shader
        int kernelIndex = 0; // Since you have only one kernel, its index is 0
        aiMovementShader.SetBuffer(kernelIndex, "aiPositions", aiPositionsBuffer);
        aiMovementShader.SetFloat("moveSpeed", moveSpeed);
        aiMovementShader.SetFloat("moveRange", moveRange);
    }

    // void Update()
    // {
    //     // Set up the compute shader
    //     int kernel = aiMovementShader.FindKernel("UpdatePosition");
    //     aiMovementShader.SetBuffer(kernel, "aiPositions", aiPositionsBuffer);
    //     aiMovementShader.SetFloat("moveSpeed", moveSpeed);
    //     aiMovementShader.SetFloat("moveRange", moveRange);
    //
    //     // Dispatch the compute shader
    //     aiMovementShader.Dispatch(kernel, aiCount, 1, 1);
    //
    //     // Update AI positions
    //     aiPositionsBuffer.GetData(aiPositions);
    //
    //     for (int i = 0; i < aiCount; i++)
    //     {
    //         aiGameObjects[i].transform.position = aiPositions[i];
    //     }
    // }
    
    void Update()
    {
        // Set up the compute shader
        int kernel = aiMovementShader.FindKernel("UpdatePosition");
        aiMovementShader.SetBuffer(kernel, "aiPositions", aiPositionsBuffer);
        aiMovementShader.SetFloat("moveSpeed", moveSpeed);
        aiMovementShader.SetFloat("moveRange", moveRange);

        // Dispatch the compute shader
        aiMovementShader.Dispatch(kernel, aiCount, 1, 1);

        // Update AI positions
        aiPositionsBuffer.GetData(aiPositions);

        // Update game object positions using the Job System
        NativeArray<Vector3> aiPositionsNativeArray = new NativeArray<Vector3>(aiPositions, Allocator.TempJob);
        TransformAccessArray transformAccessArray = new TransformAccessArray(aiGameObjects.ConvertAll(t => t.transform).ToArray());

        UpdateGameObjectPositionsJob updateJob = new UpdateGameObjectPositionsJob
        {
            aiPositions = aiPositionsNativeArray
        };

        JobHandle jobHandle = updateJob.Schedule(transformAccessArray);
        jobHandle.Complete();

        aiPositionsNativeArray.Dispose();
        transformAccessArray.Dispose();
    }

    void OnDestroy()
    {
        // Release the compute buffer
        aiPositionsBuffer.Release();
    }
}