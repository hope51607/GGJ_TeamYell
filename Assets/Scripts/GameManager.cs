using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    None = 0,
    StartAnim,
    LoadGame,
    Game,
    Result,
    Replay,
}

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private GameState m_current = GameState.None;

    private bool m_isEntering;

    public int[] points={0,1};

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

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
                        
                        // Animation CB : LoadGame
                        ChangeState(GameState.LoadGame);
                    }
                    break;
                }
            case GameState.LoadGame:
                {
                    if (m_isEntering)
                    {
                        SceneManager.LoadScene("Game");
                        ChangeState(GameState.Game);
                    }
                    break;
                }
            case GameState.Game:
                {
                    if (m_isEntering)
                    {
                        // Play ReadyGo => GameStart
                        // In Game Cycle => Result
                    }
                    break;
                }
            case GameState.Result:
                {
                    if (m_isEntering)
                    {
                        // Play Result.
                        // In Game Cycle => Wait click Replay
                        SceneManager.LoadScene("Result");
                    }
                    break;
                }
            case GameState.Replay:
                {
                    if (m_isEntering)
                    {
                        // Go Load game scene.
                        SceneManager.LoadScene("WaitForConnect");
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
