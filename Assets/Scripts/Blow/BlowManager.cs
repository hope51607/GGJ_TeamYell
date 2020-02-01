using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlowManager : MonoBehaviour
{
    [SerializeField]
    MMSimpleObjectPooler _dustPool;

    [SerializeField]
    BoxCollider _cassetteTopCollider;

    [SerializeField]
    Image _progressBarImage;

    [SerializeField]
    Text _clearCountText;

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
    const float MinDustScale = 0.02f, MaxDustScale = 0.2f;
    const float MinDustSpawnPosition = -1.5f, MaxDustSpawnPosition = 1.5f;
    const int AmountOfDust = 10;

    [SerializeField]
    int _remainingDustAmount;

    int _clearCount;
    bool _timeOut;

    private void Start()
    {
        StartCoroutine(FillDust());
    }

    public void SetBlowForce(float micInput)
    {
        //Debug.Log(micInput);
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

    public void InactiveDust()
    {
        if (_timeOut)
            return;

        _remainingDustAmount--;

        if (_remainingDustAmount == 0)
        {
            StartCoroutine(FillDust());
            _clearCount++;
            _clearCountText.text = _clearCount.ToString();
        }

        _progressBarImage.fillAmount = GetRemainingDustRatio();
    }

    public void TimeOut()
    {
        _timeOut = true;
        StopAllCoroutines();
    }

    float GetRemainingDustRatio()
    {
        return ((float)_remainingDustAmount / AmountOfDust);
    }

    void Blow()
    {
        BlowDirection = Vector3.ProjectOnPlane(CassetteTransform.position - transform.position, CassetteTransform.up).normalized;
        BlowDirection += UpForceMultiplier * Vector3.Project(CassetteTransform.position - transform.position, CassetteTransform.up).normalized;
        BlowDirection *= -1;

        ApplyBlowForce?.Invoke();
    }

    IEnumerator FillDust()
    {
        yield return null;
        
        Vector3 _pos = Vector3.zero;
        float _scale;
        for (int i = 0; i < AmountOfDust; i++)
        {
            var dustObj = _dustPool.GetPooledGameObject();

            Dust _dustScript = dustObj.GetComponent<Dust>();
            _dustScript.AttachedBlowManager = this;

            _pos.z = Random.Range(MinDustSpawnPosition, MaxDustSpawnPosition);

            dustObj.transform.localPosition = _pos;

            _scale = Random.Range(MinDustScale, MaxDustScale);
            dustObj.transform.localScale = new Vector3(_scale, _scale, _scale);

            dustObj.SetActive(true);
        }
        _remainingDustAmount = AmountOfDust;
        _progressBarImage.fillAmount = 1;
    }
}
