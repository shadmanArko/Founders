using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

struct UpdateGameObjectPositionsJob : IJobParallelForTransform
{
    [ReadOnly] public NativeArray<Vector3> aiPositions;


    public void Execute(int index, TransformAccess transform)
    {
        transform.position = aiPositions[index];
    }
}