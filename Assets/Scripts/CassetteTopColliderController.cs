using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassetteTopColliderController : MonoBehaviour
{
    [SerializeField]
    BoxCollider _topCollider;

    void Update()
    {
        if (transform.eulerAngles.z > 30
            && transform.eulerAngles.z < 150)
        {
            _topCollider.enabled = false;
        }
        else
        {
            _topCollider.enabled = true;
        }
    }
}
