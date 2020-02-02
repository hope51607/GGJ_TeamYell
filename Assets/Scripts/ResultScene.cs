using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultScene : MonoBehaviour
{
    public void Replay()
    {
         GameManager.Instance.ChangeState(GameState.Replay);
    }
}
