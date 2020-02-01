using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    Rigidbody _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        BlowManager.Instance.ApplyBlowForce += ApplyBlowForce;
    }

    private void FixedUpdate()
    {
        _rigid.velocity -= Vector3.up * 0.98f;
    }

    private void OnDisable()
    {
        if (BlowManager.Instance != null)
        {
            BlowManager.Instance.ApplyBlowForce -= ApplyBlowForce;
        }
    }

    void ApplyBlowForce()
    {
        float _distanceRatio = Mathf.Abs(BlowManager.Instance.CassetteTransform.position.x - transform.position.x) / BlowManager.CassetteHalfLength;        // 除以卡帶半長正規化
        _distanceRatio = Mathf.Clamp01(_distanceRatio);
        Vector3 _force = BlowManager.Instance.BlowDirection
                         * BlowManager.Instance.BlowForce;
        _force *= (1 - Mathf.Pow(_distanceRatio, 2));


        _rigid.AddForce(_force, ForceMode.Impulse);
    }
}
