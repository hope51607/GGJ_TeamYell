using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowManager : MonoSingleton<BlowManager>
{
    public delegate void ApplyBlowForceDelegate();
    public ApplyBlowForceDelegate ApplyBlowForce;
    public Transform TapeTransform;

    public float BlowForce { get; private set; }
    public Vector3 BlowDirection { get; private set; }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ApplyBlowForce?.Invoke();
        }
    }
}
