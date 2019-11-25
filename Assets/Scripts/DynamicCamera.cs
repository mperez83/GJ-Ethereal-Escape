using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    public Transform playerOne;
    public Transform playerTwo;
    public Vector3 offset;
    public float smoothTime;
    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;
    Vector3 velocity;

    Camera sceneCamera;



    void Start()
    {
        sceneCamera = Camera.main;
    }

    void FixedUpdate()
    {
        Vector3 direction = playerOne.position - playerTwo.position;
        Vector3 offset = direction.normalized * (direction.magnitude / 2f);

        Vector3 newPos = playerOne.position - offset;
        newPos.y = 7;

        newPos += (5 * Vector3.Cross(direction, Vector3.up).normalized) + (0.5f * Vector3.Cross(direction, Vector3.up));
        transform.LookAt(GetCenterPoint());

        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);
    }



    Vector3 GetCenterPoint()
    {
        Bounds bounds = new Bounds(playerOne.position, Vector3.zero);
        bounds.Encapsulate(playerOne.position);
        bounds.Encapsulate(playerTwo.position);
        return bounds.center;
    }
}