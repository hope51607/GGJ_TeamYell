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

    private void OnDisable()
    {
        if (BlowManager.Instance != null)
        {
            BlowManager.Instance.ApplyBlowForce -= ApplyBlowForce;
        }
    }

    void ApplyBlowForce()
    {
        Vector3 _positionOffset = BlowManager.Instance.TapeTransform.position - transform.position;
        _positionOffset.y = 0;
        float _distanceToCenter = _positionOffset.magnitude;        // 除以卡帶半長正規化

        Vector3 _force = BlowManager.Instance.BlowDirection
                         * BlowManager.Instance.BlowForce
                         * (1 - Mathf.Pow(_distanceToCenter, 2));
        // 對卡帶的local Y軸反轉 (投影到卡帶的Y軸之後 扣掉兩倍)
    }
}
