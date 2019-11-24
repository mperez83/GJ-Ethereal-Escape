using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    public List<Transform> targets;
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

    void LateUpdate()
    {
        if (targets.Count == 0) return;
        Move();
        Zoom();
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        sceneCamera.fieldOfView = Mathf.Lerp(sceneCamera.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
        foreach (Transform trans in targets)
        {
            bounds.Encapsulate(trans.position);
        }
        return bounds.size.x;
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
        foreach (Transform trans in targets)
        {
            bounds.Encapsulate(trans.position);
        }

        return bounds.center;
    }
}