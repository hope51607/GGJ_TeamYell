using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowManager : MonoSingleton<BlowManager>
{
    [SerializeField]
    MMSimpleObjectPooler _dustPool;

    [SerializeField]
    Vector3 MinDustSpawnPosition, MaxDustSpawnPosition;

    public Vector3 BlowDirection { get; private set; }

    public delegate void ApplyBlowForceDelegate();
    public ApplyBlowForceDelegate ApplyBlowForce;
    public Transform CassetteTransform;
    public float BlowForce;

    public const float CassetteHalfLength = 1.5f;
    const float UpForceMultiplier = 2.5f;
    const float AmountOfDust = 15;
    const float MinDustScale = 0.035f, MaxDustScale = 0.07f;

    private void Awake()
    {
        Vector3 _pos;
        float _scale;
        for (int i = 0; i < AmountOfDust; i++)
        {
            var dustObj = _dustPool.GetPooledGameObject();
            _pos.x = Random.Range(MinDustSpawnPosition.x, MaxDustSpawnPosition.x);
            _pos.z = Random.Range(MinDustSpawnPosition.z, MaxDustSpawnPosition.z);
            _scale = Random.Range(MinDustScale, MaxDustScale);
            dustObj.transform.localScale = new Vector3(_scale, _scale, _scale);

            dustObj.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BlowDirection = Vector3.ProjectOnPlane(CassetteTransform.position - transform.position, CassetteTransform.up).normalized;
            BlowDirection -= UpForceMultiplier * Vector3.Project(CassetteTransform.position - transform.position, CassetteTransform.up).normalized;
            Debug.Log(BlowDirection);
            ApplyBlowForce?.Invoke();
        }
    }
}
