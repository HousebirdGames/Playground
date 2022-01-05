using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BatchUpdater : MonoBehaviour
{
    public UnityEvent[] updateEvents;
    [HideInInspector] public bool isFinished = false;
    private float lastFrameTime;
    public float controllerDeltaTime;
    public float deltaTime;
    public float timeNeededForThisBatch = 0f;
    BatchUpdateController batchUpdateController;
    [HideInInspector] public float lastUpdatedTime = 0f;

    private void OnEnable()
    {
        batchUpdateController = FindObjectOfType<BatchUpdateController>();

        bool shouldFetch = true;
        foreach(BatchUpdater batchUpdater in batchUpdateController.batchUpdaters)
        {
            if(batchUpdater == this)
            {
                shouldFetch = false;
            }
        }

        if (shouldFetch)
        {
            batchUpdateController.FetchBatchUpdaters();
        }
    }

    private void OnDisable()
    {
        if (batchUpdateController != null)
        {
            batchUpdateController.FetchBatchUpdaters();
            isFinished = false;
        }
    }

    public void UpdateBatch(float parentDeltaTime)
    {
        controllerDeltaTime = parentDeltaTime;
        lastUpdatedTime = Time.time;
        lastFrameTime = Time.time;
        isFinished = false;
        StartCoroutine(DoUpdateBatch());
    }

    public void StopBatchUpdate()
    {
        isFinished = false;
        StopCoroutine(DoUpdateBatch());
    }

    IEnumerator DoUpdateBatch()
    {
        foreach (UnityEvent updateEvent in updateEvents)
        {
            yield return new WaitForEndOfFrame();
            deltaTime = controllerDeltaTime + (Time.time - lastFrameTime) * Time.deltaTime;
            updateEvent.Invoke();

            if (!batchUpdateController.updateBatchesSimultaneously)
            {
                lastFrameTime = Time.time;
                yield return null;
            }
        }

        isFinished = true;
        yield return null;
    }
}
