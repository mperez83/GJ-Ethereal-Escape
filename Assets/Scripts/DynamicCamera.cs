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

    void FixedUpdate()
    {
        Vector3 direction = targets[0].position - targets[1].position;
        Vector3 offset = direction.normalized * (direction.magnitude / 2f);

        Vector3 newPos = targets[0].position - offset;
        newPos.y = 6;

        newPos += (5 * Vector3.Cross(direction, Vector3.up).normalized) + (0.5f * Vector3.Cross(direction, Vector3.up));
        transform.LookAt(GetCenterPoint());

        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);
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