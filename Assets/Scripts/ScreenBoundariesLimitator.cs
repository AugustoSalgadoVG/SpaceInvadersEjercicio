using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundariesLimitator : MonoBehaviour
{
    private Vector2 screenLimits;
    private float objectDistanceToCenter;
    private void Start()
    {
        SetScreenLimits();
        SetObjectWidth();
    }
    void LateUpdate()
    {
        KeepObjectBetweenScreenLimits();
    }
    void SetScreenLimits()
    {
        screenLimits = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));
    }

    void SetObjectWidth()
    {
        objectDistanceToCenter = GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }
    void KeepObjectBetweenScreenLimits()
    {
        Vector3 finalPosition = transform.position;
        finalPosition.x = Mathf.Clamp(transform.position.x, screenLimits.x * -1 + objectDistanceToCenter, screenLimits.x - objectDistanceToCenter);
        
        transform.position = finalPosition;
    }
}
