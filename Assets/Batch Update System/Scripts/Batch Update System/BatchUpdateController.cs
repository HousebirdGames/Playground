using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatchUpdateController : MonoBehaviour
{
    public BatchUpdater[] batchUpdaters;
    [Tooltip("If this value is 0, Batch Update will happen every frame")]
    public float secondsBetweenBatchUpdates = 0f;
    private int currentBatch = 0;
    private bool currentlyUpdatingABatch;
    private float lastFrameTime;
    public float deltaTime;
    [Tooltip("Updates all batches in every frame. Changing this option at runtime is not recommended.")]
    public bool updateBatchesSimultaneously = false;

    [ContextMenu("Fetch Batch Updaters")]
    public void FetchBatchUpdaters()
    {
        StopCoroutine(DoBatchUpdate());
        currentlyUpdatingABatch = false;
        batchUpdaters = FindObjectsOfType<BatchUpdater>();
        currentBatch = 0;
        StartCoroutine(DoBatchUpdate());
    }

    IEnumerator DoBatchUpdate()
    {
        yield return new WaitForEndOfFrame();

        while (!updateBatchesSimultaneously)
        {
            if (!batchUpdaters[currentBatch].isFinished && !currentlyUpdatingABatch)
            {
                deltaTime = Time.time - batchUpdaters[currentBatch].lastUpdatedTime;
                currentlyUpdatingABatch = true;
                batchUpdaters[currentBatch].UpdateBatch(deltaTime);
            }
            else if (batchUpdaters[currentBatch].isFinished)
            {
                currentlyUpdatingABatch = false;
                batchUpdaters[currentBatch].StopBatchUpdate();

                currentBatch++;
                if (currentBatch > batchUpdaters.Length - 1)
                {
                    currentBatch = 0;
                }
            }

            if(secondsBetweenBatchUpdates == 0)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSecondsRealtime(secondsBetweenBatchUpdates / 2f);
            }
        }

        while (updateBatchesSimultaneously)
        {
            foreach(BatchUpdater batchUpdater in batchUpdaters)
            {
                deltaTime = Time.deltaTime;
                batchUpdater.UpdateBatch(deltaTime);
            }
            yield return null;
        }
    }
}
