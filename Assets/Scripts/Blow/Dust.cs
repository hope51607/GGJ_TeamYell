﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    public BlowManager AttachedBlowManager;
    const float MinInactiveYPosThreshold = -1f, MaxInactiveYPosThreshold = 2f;
    Rigidbody _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (transform.localPosition.y < MinInactiveYPosThreshold
            || transform.localPosition.y > MaxInactiveYPosThreshold)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (AttachedBlowManager != null)
        {
            AttachedBlowManager.ApplyBlowForce += ApplyBlowForce;
        }

        _rigid.velocity = Vector3.zero;
        _rigid.mass = transform.localScale.x;
    }

    private void OnDisable()
    {
        if (AttachedBlowManager != null)
        {
            AttachedBlowManager.ApplyBlowForce -= ApplyBlowForce;
            AttachedBlowManager.InactiveDust();
        }
    }

    void ApplyBlowForce()
    {
        float _distanceRatio = Mathf.Abs(AttachedBlowManager.CassetteTransform.position.x - transform.position.x) / BlowManager.CassetteHalfLength;        // 除以卡帶半長正規化
        _distanceRatio = Mathf.Clamp01(_distanceRatio);
        Vector3 _force = AttachedBlowManager.BlowDirection
                         * AttachedBlowManager.BlowForce;
        if (_distanceRatio > 1)
            _force /= _distanceRatio;

        _rigid.AddForce(_force, ForceMode.Impulse);
    }
}
