using UnityEngine;
using System.Collections;
using Vuforia;

public class MainController : MonoBehaviour {
    private static MainController instance;
    public GameObject[] trackablePrefabs;
    void Start()
    {
        instance = this;
        trackableObjects = new TrackableObject[trackablePrefabs.Length];
        for(int i=0;i< trackablePrefabs.Length;++i)
        {
            GameObject prefabObj = trackablePrefabs[i];
            TrackableObject trackObj = new TrackableObject(prefabObj);
            trackObj.Instantiate();
            trackableObjects[i] = trackObj;
        }
    }

    public static MainController GetInstance()
    {
        return instance;
    }
    public TrackableObject[] trackableObjects;
    public static TrackableObject[] TrackableObjects
    {
        get { return instance.trackableObjects; }
    }
    public GameObject arCamera;
    public static GameObject ARCamera
    {
        get { return instance.arCamera; }
    }

}
