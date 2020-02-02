using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoSingleton<GameplayController>
{
    [SerializeField]
    Image TimeBar;

    public BlowManager[] BlowManagers;
    const int AnimationDelay = 4;                    // 開場動畫4秒
    const int MaxTime = 60;

    [SerializeField]
    float _timeCounter;

    private void Awake()
    {
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
        GameManager.Instance.ChangeState(GameState.Result);
        foreach (var manager in BlowManagers)
        {
            manager.TimeOut();
        }
    }
}
