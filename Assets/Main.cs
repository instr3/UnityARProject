using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{

    public SensorShower[] ShowList;
    public Transform FollowerTransform;
    private Vector3 PreviousAcc = new Vector3();
    private Vector3 Speed = new Vector3();
    void Start()
    {
        Input.gyro.enabled = true;
        Input.compass.enabled = true;
    }
    void Update()
    {
        ShowList[0].Value = Input.gyro.userAcceleration * 10;
        ShowList[0].SetTitle("Gyro.UserAcceleration");
        ShowList[1].Value = Input.acceleration;
        ShowList[1].SetTitle("Acceleration");
        ShowList[2].Value = Input.gyro.gravity;
        ShowList[2].SetTitle("Gyro.Gravity");
        ShowList[3].Value = Input.gyro.rotationRate;
        ShowList[3].SetTitle("Gyro.RotationRate");
        //ShowList[4].Value = new Vector3(Input.GetAxis("Vertical"), Input.GetAxis("Jump"), Input.GetAxis("Horizontal"));
        //ShowList[4].SetTitle("Key");
        ShowList[5].Value = new Vector3(Input.compass.trueHeading / 180f, 0, 0);
        ShowList[5].SetTitle("Compass.trueHeading");
        ShowList[4].Value = Input.gyro.attitude.eulerAngles/180f;
        ShowList[4].SetTitle("gyro.attitude");
        //Camera.main.transform.rotation = Input.gyro.attitude;
        //PreviousAcc = Vector3.Lerp(PreviousAcc, Input.gyro.userAcceleration, 0.02f);
        //FollowerTransform.position =
        //Camera.main.transform.TransformVector(-PreviousAcc * 2);
        //Speed += Camera.main.transform.TransformVector(Input.gyro.userAcceleration);
        //Speed += Vector3.ClampMagnitude(-FollowerTransform.position, 5f) * 0.1f;
        //Speed *= 0.9f;
        //FollowerTransform.position += Speed * Time.deltaTime;
    }
    void FixedUpdate()
    {
        PreviousAcc = Vector3.Lerp(PreviousAcc, Input.gyro.userAcceleration, 0.02f);
        FollowerTransform.position = Camera.main.transform.TransformVector(-PreviousAcc*10);

    }
}
