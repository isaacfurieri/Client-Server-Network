using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

internal class UnityMainThread : MonoBehaviour
{
    internal static UnityMainThread umt;
    Queue<Action> jobs = new Queue<Action>();

    private void Awake()
    {
        umt = this;
    }

    // Update is called once per frame
    void Update()
    {
        while(jobs.Count > 0)
        {
            jobs.Dequeue().Invoke();
        }
    }

    internal void AddJob(Action newJob) {
        jobs.Enqueue(newJob);
    }
}
