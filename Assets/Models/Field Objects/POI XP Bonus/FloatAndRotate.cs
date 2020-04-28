﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Floating and Rotating the 3D text
public class FloatAndRotate : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 50.0f;
    [SerializeField] private float floatAmplitude = 2.0f;
    [SerializeField] private float floatFrequency = 0.5f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position; //Getting the position of the object attached to
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);

        Vector3 tempPos = startPosition;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * floatFrequency) * floatAmplitude;

        transform.position = tempPos;
    }
}
