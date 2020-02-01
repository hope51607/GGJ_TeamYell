using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowManager : MonoBehaviour
{
    [SerializeField]
    MMSimpleObjectPooler _dustPool;

    [SerializeField]
    BoxCollider _cassetteTopCollider;

    [SerializeField]
    Vector3 MinDustSpawnPosition, MaxDustSpawnPosition;

    [SerializeField]
    float _micInputMultiplier = 5f;

    // 對硬體適應 要拿出來UI改
    [SerializeField]
    float MicInputThreshold = 0.3f;

    public Vector3 BlowDirection { get; private set; }

    public delegate void ApplyBlowForceDelegate();
    public ApplyBlowForceDelegate ApplyBlowForce;
    public Transform CassetteTransform;
    public float BlowForce = 0.05f;

    public const float CassetteHalfLength = 1.5f;
    const float UpForceMultiplier = 2.5f;
    const float AmountOfDust = 10;
    const float MinDustScale = 0.02f, MaxDustScale = 0.2f;

    private void Start()
    {
        FillDust();
    }

    void FixedUpdate()
    {
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
            _cassetteTopCollider.enabled = false;
            BlowForce = micInput * _micInputMultiplier;
            Blow();
        }
        else
        {
            _cassetteTopCollider.enabled = true;
        }
    }

    void Blow()
    {
        BlowDirection = Vector3.ProjectOnPlane(CassetteTransform.position - transform.position, CassetteTransform.up).normalized;
        BlowDirection -= UpForceMultiplier * Vector3.Project(CassetteTransform.position - transform.position, CassetteTransform.up).normalized;
        ApplyBlowForce?.Invoke();
    }

    void FillDust()
    {
        Vector3 _pos;
        float _scale;
        for (int i = 0; i < AmountOfDust; i++)
        {
            var dustObj = _dustPool.GetPooledGameObject();

            Dust _dustScript = dustObj.GetComponent<Dust>();
            _dustScript.AttachedBlowManager = this;

            _pos.x = Random.Range(MinDustSpawnPosition.x, MaxDustSpawnPosition.x);
            _pos.y = 0;
            _pos.z = Random.Range(MinDustSpawnPosition.z, MaxDustSpawnPosition.z);

            dustObj.transform.localPosition = _pos;

            _scale = Random.Range(MinDustScale, MaxDustScale);
            dustObj.transform.localScale = new Vector3(_scale, _scale, _scale);

            dustObj.SetActive(true);
        }
    }
}
