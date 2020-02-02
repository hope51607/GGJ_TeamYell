using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForConnectScene : MonoBehaviour
{
    public void LoadGame()
    {
        GameManager.Instance.ChangeState(GameState.LoadGame);
    }
}
