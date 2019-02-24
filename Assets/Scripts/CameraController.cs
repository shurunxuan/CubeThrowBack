using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraController : MonoBehaviour
{

    public List<Transform> Tracking;

    private Vector3 center;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 viewportBBMin = new Vector2(Single.MaxValue, Single.MaxValue);
        Vector2 viewportBBMax = new Vector2(Single.MinValue, Single.MinValue);
        foreach (var transform1 in Tracking)
        {
            Vector3 screenPosition = Camera.main.WorldToViewportPoint(transform1.position);
            if (screenPosition.x < viewportBBMin.x) viewportBBMin.x = screenPosition.x;
            if (screenPosition.y < viewportBBMin.y) viewportBBMin.y = screenPosition.y;
            if (screenPosition.x > viewportBBMax.x) viewportBBMax.x = screenPosition.x;
            if (screenPosition.y > viewportBBMax.y) viewportBBMax.y = screenPosition.y;
            center += transform1.position;
        }

        center /= Tracking.Count;
        float newSize = (viewportBBMax - viewportBBMin).magnitude;
        float ratio = newSize * 2.0f;

        Camera.main.orthographicSize *= ratio;


        transform.position = center - transform.forward * 500.0f;

    }
}
