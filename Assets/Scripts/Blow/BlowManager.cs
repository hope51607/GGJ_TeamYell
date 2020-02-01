using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowManager : MonoSingleton<BlowManager>
{
    [SerializeField]
    MMSimpleObjectPooler _dustPool;

    [SerializeField]
    Vector3 MinDustSpawnPosition, MaxDustSpawnPosition;

    [SerializeField]
    float DustPositionY = 0;

    [SerializeField]
    float _micInputMultiplier = 0.5f;

    public Vector3 BlowDirection { get; private set; }

    public delegate void ApplyBlowForceDelegate();
    public ApplyBlowForceDelegate ApplyBlowForce;
    public Transform CassetteTransform;

    public const float CassetteHalfLength = 1.5f;
    const float UpForceMultiplier = 2.5f;
    const float AmountOfDust = 50;
    const float MinDustScale = 0.035f, MaxDustScale = 0.07f;
    const float MicInputThreshold = 0.1f;

    public float BlowForce;

    private void Start()
    {
        FillDust();
    }

    void FixedUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Blow();
        //}

        if (Input.GetKeyDown(KeyCode.Q))
        {
            FillDust();
        }
    }

    public void SetBlowForce(float micInput)
    {
        Debug.Log(micInput);
        if (micInput > MicInputThreshold)
        {
            BlowForce = micInput * _micInputMultiplier;
            Blow();
        }
    }

    void Blow()
    {
        BlowDirection = Vector3.ProjectOnPlane(CassetteTransform.position - transform.position, CassetteTransform.up).normalized;
        BlowDirection -= UpForceMultiplier * Vector3.Project(CassetteTransform.position - transform.position, CassetteTransform.up).normalized;
        Debug.Log(BlowDirection);
        ApplyBlowForce?.Invoke();
    }

    void FillDust()
    {
        Vector3 _pos;
        float _scale;
        for (int i = 0; i < AmountOfDust; i++)
        {
            var dustObj = _dustPool.GetPooledGameObject();

            _pos.x = Random.Range(MinDustSpawnPosition.x, MaxDustSpawnPosition.x);
            _pos.y = DustPositionY;
            _pos.z = Random.Range(MinDustSpawnPosition.z, MaxDustSpawnPosition.z);
            _pos += CassetteTransform.position;
            dustObj.transform.position = _pos;

            _scale = Random.Range(MinDustScale, MaxDustScale);
            dustObj.transform.localScale = new Vector3(_scale, _scale, _scale);

            dustObj.SetActive(true);
        }
    }
}
