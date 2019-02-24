using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraController : MonoBehaviour
{

    public List<Transform> Tracking;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewportBBMin = new Vector2(Single.MaxValue, Single.MaxValue);
        Vector3 viewportBBMax = new Vector2(Single.MinValue, Single.MinValue);
        foreach (var transform1 in Tracking)
        {
            Vector3 screenPosition = Camera.main.WorldToViewportPoint(transform1.position);
            if (screenPosition.x < viewportBBMin.x) viewportBBMin.x = screenPosition.x;
            if (screenPosition.y < viewportBBMin.y) viewportBBMin.y = screenPosition.y;
            if (screenPosition.z < viewportBBMin.z) viewportBBMin.z = screenPosition.z;
            if (screenPosition.x > viewportBBMax.x) viewportBBMax.x = screenPosition.x;
            if (screenPosition.y > viewportBBMax.y) viewportBBMax.y = screenPosition.y;
            if (screenPosition.z > viewportBBMax.z) viewportBBMax.z = screenPosition.z;
        }

        Vector2 viewportSizeMin = new Vector2(viewportBBMin.x, viewportBBMin.y);
        Vector2 viewportSizeMax = new Vector2(viewportBBMax.x, viewportBBMax.y);
        float newSize = (viewportSizeMin - viewportSizeMax).magnitude;
        Vector3 newViewportCenter = (viewportBBMin + viewportBBMax) / 2.0f;
        Vector3 worldCenter = Camera.main.ViewportToWorldPoint(newViewportCenter);
        float ratio = newSize * 2.0f;

        Camera.main.orthographicSize *= ratio;


        transform.position = worldCenter - transform.forward * 150.0f;

    }
}
