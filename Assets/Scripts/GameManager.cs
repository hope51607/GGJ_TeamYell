using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    None = 0,
    StartAnim,
    Game,
    Result,
    Replay,
}

public class GameManager : MonoSingleton<GameManager>
{
    private GameState m_current = GameState.None;

    private bool m_isEntering;

    private void Start()
    {
        ChangeState(GameState.StartAnim);
    }

    private void Update()
    {
        Tick();
    }

    private void Tick()
    {
        switch (m_current)
        {
            case GameState.StartAnim:
                {
                    if (m_isEntering)
                    {
                        // Play Animation
                        // Animation CB : Game
                    }
                    break;
                }
            case GameState.Game:
                {
                    if(m_isEntering)
                    {
                        // Play ReadyGo => GameStart
                        // In Game Cycle => Result
                    }
                    break;
                }
            case GameState.Result:
                {
                    if(m_isEntering)
                    {
                        // Play Result.
                        // In Game Cycle => Wait click Replay
                    }
                    break;
                }
            case GameState.Replay:
                {
                    if(m_isEntering)
                    {
                        // Go Load game scene.
                    }
                    break;
                }
        }

        if (m_isEntering)
            m_isEntering = false;
    }

    public void ChangeState(GameState gameState)
    {
        m_current = gameState;
        m_isEntering = true;
    }

}
