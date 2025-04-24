using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class TargetMovement : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 1, 2);

    private void Update()
    {

        double scale = 2 / (3 - Math.Cos(2 * Time.time));
        double x = scale * Math.Cos(Time.time);
        double y = scale * Math.Sin(2 * Time.time) / 2;

        transform.position = new Vector3((float)x, (float)y, 0) + offset;
    }
}
