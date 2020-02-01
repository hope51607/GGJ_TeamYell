using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowManager : MonoSingleton<BlowManager>
{
    public delegate void ApplyBlowForceDelegate();
    public ApplyBlowForceDelegate ApplyBlowForce;
    public Transform CassetteTransform;
    public const float CassetteHalfLength = 1.5f;

    public float BlowForce;
    public Vector3 BlowDirection { get; private set; }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BlowDirection = Vector3.ProjectOnPlane(CassetteTransform.position - transform.position, CassetteTransform.up).normalized;
            BlowDirection -= Vector3.Project(CassetteTransform.position - transform.position, CassetteTransform.up).normalized;
            Debug.Log(BlowDirection);
            ApplyBlowForce?.Invoke();
        }
    }
}
