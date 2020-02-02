using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[SerializeField]
public enum GameState
{
    None = 0,
    StartAnim,
    WaitForConnect,
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

    public float[] micThresholds;

    public int[] points;

    public string[] PlayerNames = { "ovvo", "ovvo2" };

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        micThresholds = new float[2];
        points = new int[2];
    }

    private void Start()
    {
     //   ChangeState(GameState.WaitForConnect);
    }

    private void Update()
    {
        Tick();
    }

    private void Tick()
    {
        var entering = m_isEntering;
        if (m_isEntering)
            m_isEntering = false;

        switch (m_current)
        {
            case GameState.StartAnim:
                {
                    if (entering)
                    {
                        // Play Animation

                        // Animation CB : LoadGame
                        ChangeState(GameState.WaitForConnect);
                    }
                    break;
                }
            case GameState.WaitForConnect:
                {
                    if (entering)
                    {
                        SceneManager.LoadScene("WaitForConnect");
                    }
                    break;
                }
            case GameState.LoadGame:
                {
                    if (entering)
                    {
                        SceneManager.LoadScene("Game");
                        ChangeState(GameState.Game);
                    }
                    break;
                }
            case GameState.Game:
                {
                    if (entering)
                    {
                        // Play ReadyGo => GameStart
                        // In Game Cycle => Result
                        Debug.Log("Entering");
                        AudioManager.Instance.SwitchMusic("GamePlay");
                    }
                    break;
                }
            case GameState.Result:
                {
                    if (entering)
                    {
                        // Play Result.
                        // In Game Cycle => Wait click Replay
                        SceneManager.LoadScene("Result");
                    }
                    break;
                }
            case GameState.Replay:
                {
                    if (entering)
                    {
                        // Go Load game scene.
                        SceneManager.LoadScene("WaitForConnect");
                    }
                    break;
                }
        }

        
    }

    public void ChangeState(GameState gameState)
    {
        m_current = gameState;
        m_isEntering = true;
    }

}
