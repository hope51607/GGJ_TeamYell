using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultScene : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKey)
        {
            GameManager.Instance.ChangeState(GameState.Replay);
        }
    }
}
