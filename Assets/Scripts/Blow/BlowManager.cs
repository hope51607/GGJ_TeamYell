using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BlowManager : MonoBehaviour
{
    [SerializeField]
    MMSimpleObjectPooler _dustPool;

    [SerializeField]
    Image _progressBarImage;

    [SerializeField]
    Text _clearCountText;

    public Vector3 BlowDirection { get; private set; }
    public int ClearCount { get; private set; }

    public delegate void ApplyBlowForceDelegate();
    public ApplyBlowForceDelegate ApplyBlowForce;
    public Transform CassetteTransform;
    public float MicInputThreshold = 0.1f;
    public float BlowForce = 0.3f;

    public const float CassetteHalfLength = 1.5f;
    const float UpForceMultiplier = 2.5f;
    const float MinDustScale = 0.02f, MaxDustScale = 0.2f;
    const float MinDustSpawnPosition = -1.5f, MaxDustSpawnPosition = 1.5f;
    const float ConstBlowForce = 0.3f;
    const int AmountOfDust = 10;

    Vector3 _oriCassettePos;

    [SerializeField]
    MeshRenderer _renderer;
    [SerializeField]
    MeshRenderer _renderer_bottom;

    [SerializeField]
    int _remainingDustAmount;

    bool _timeOut;

    private void Start()
    {
        _oriCassettePos = CassetteTransform.position;
        StartCoroutine(FillDust());
    }

    public void SetBlowForce(float micInput)
    {
        float _inputThresholdRatio = (micInput / MicInputThreshold);
        if (_inputThresholdRatio >= 1)
        {
            BlowForce = _inputThresholdRatio * ConstBlowForce;
            Blow();

            if (_inputThresholdRatio < 1.2f)
            {
                AudioManager.Instance.PlaySE("BlowLow");
            }
            else
            {
                AudioManager.Instance.PlaySE("BlowHigh");
            }
        }
    }

    public void SetBlowForce()
    {
        BlowForce = ConstBlowForce;
        Blow();

        AudioManager.Instance.PlaySE("BlowLow");
    }

    public void InactiveDust()
    {
        if (_timeOut)
            return;

        _remainingDustAmount--;

        if (_remainingDustAmount == 0)
        {
            StartCoroutine(FillDust());
            ClearCount++;
            _clearCountText.text = ClearCount.ToString();

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
        CassetteTransform.DOMoveY(-4, 0.75f);
        yield return new WaitForSeconds(0.75f);
        Vector3 _pos = CassetteTransform.position;
        _pos.y = 12;

        int _target = Random.Range(0, 2);

        if (_target == 0)
        {
            _renderer.materials = GameplayController.Instance.MaterailSet1;
            _renderer_bottom.material = GameplayController.Instance.MaterailSet1[0];
        }
        else
        {
            _renderer.materials = GameplayController.Instance.MaterailSet2;
            _renderer_bottom.material = GameplayController.Instance.MaterailSet2[0];
        }

        CassetteTransform.position = _pos;

        CassetteTransform.DOMove(_oriCassettePos, 0.75f);
        yield return new WaitForSeconds(0.75f);
        
        _pos = Vector3.zero;
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
