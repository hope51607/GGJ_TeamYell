using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoSingleton<GameplayController>
{
    public Material[] MaterailSet1, MaterailSet2;

    [SerializeField]
    Image TimeBar;

    [SerializeField]
    Animator _finishTextAnimator;

    public BlowManager[] BlowManagers;
    const int AnimationDelay = 4;                    // 開場動畫4秒
    const int MaxTime = 60;

    [SerializeField]
    float _timeCounter;

    private void Awake()
    {
        for (int i = 0; i < BlowManagers.Length; i++)
        {
            BlowManagers[i].MicInputThreshold = GameManager.Instance.micThresholds[i];
            Debug.Log(i + " " + BlowManagers[i].MicInputThreshold);
        }
        StartCoroutine(TimeCounter()); 
    }

    IEnumerator TimeCounter()
    {
        _timeCounter = MaxTime;
        yield return new WaitForSeconds(AnimationDelay);

        while (_timeCounter > 0)
        {
            TimeBar.fillAmount = _timeCounter / MaxTime;
            _timeCounter -= Time.deltaTime;

            yield return null;
        }

        TimeOut();
    }

    void TimeOut()
    {
        for (int i = 0; i < BlowManagers.Length; i++) {
            BlowManagers[i].TimeOut();
            GameManager.Instance.points[i] = BlowManagers[i].ClearCount;
        }
        _finishTextAnimator.SetTrigger("Finish");

        StartCoroutine(ToResultScene());
    }

    IEnumerator ToResultScene()
    {
        yield return new WaitForSeconds(3);
        GameManager.Instance.ChangeState(GameState.Result);
    }
}
