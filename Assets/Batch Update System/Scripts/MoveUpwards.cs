using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpwards : MonoBehaviour
{
    public float speed = 1f;
    public bool useBatchUpdater;
    public BatchUpdater batchUpdater;

    void Update()
    {
        if (!useBatchUpdater)
        {
            MoveUp(Time.deltaTime);
        }
    }

    public void MoveUpwardsUpdate()
    {
        if (useBatchUpdater)
        {
            MoveUp(batchUpdater.deltaTime);
        }
    }

    private void MoveUp(float deltaTime)
    {
        transform.position += new Vector3(0f, speed * deltaTime, 0f);
    }
}
