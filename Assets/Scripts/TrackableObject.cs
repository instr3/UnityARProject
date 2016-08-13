using UnityEngine;
using System.Collections;
using Vuforia;

public class TrackableObject
{
    public ITrackableEventHandler trackableEventHandeler;
    private GameObject prefabObject;
    private GameObject sceneObject;
    public TrackableObject() { }

    public TrackableObject(GameObject obj)
    {
        prefabObject = obj;
        trackableEventHandeler = obj.GetComponent<ITrackableEventHandler>();
    }
    
    public void Instantiate()
    {
        sceneObject = Object.Instantiate(prefabObject);
    }

}
