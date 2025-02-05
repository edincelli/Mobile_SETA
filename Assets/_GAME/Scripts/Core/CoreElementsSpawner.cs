using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreElementsSpawner : GameSystemComponent
{
    [SerializeField] private CoreElementsPreset coreElements;
    [SerializeField] private CoreElementsPreset devElements;

    public CoreElementsPreset CoreElements { get => coreElements; }
    public CoreElementsPreset DevElements { get => devElements; }

    private void Awake()
    {
        SpawnCoreElements();
        SpawnDevElements();
    }

    private void SpawnCoreElements()
    {
        if (coreElements != null)
        {
            for (int i = 0; i < coreElements.coreElements.Count; i++)
            {
                Instantiate(coreElements.coreElements[i], transform).name = coreElements.coreElements[i].name;
            }
        }
        else
        {
            throw new NullReferenceException("CoreElements object is null");
        }
    }

    private void SpawnDevElements()
    {
        if (SceneManager.GetActiveScene().name.StartsWith("dev", StringComparison.CurrentCultureIgnoreCase) == false)
            return;
        
        if (devElements == null)
            return;

        Transform devParent = new GameObject("DevTools").transform;
        devParent.SetParent(transform);

        for (int i = 0; i < devElements.coreElements.Count; i++)
        {
            Instantiate(devElements.coreElements[i], devParent).name = devElements.coreElements[i].name;
        }
    }
}
