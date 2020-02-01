using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoSingleton<GameplayController>
{
    [SerializeField]
    Text TimeText;

    public BlowManager[] BlowManagers;

    const int MaxTime = 60;
    float _timeCounter;

    private void Awake()
    {
        _timeCounter = MaxTime;
    }

    private void Update()
    {
        TimeText.text = ((int)_timeCounter).ToString();
        _timeCounter -= Time.deltaTime;

        if (_timeCounter <= 0)
        {
            TimeOut();
        }
    }

    void TimeOut()
    {
        GameManager.Instance.ChangeState(GameState.Result);
    }
}
