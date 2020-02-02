using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionUIChecker : MonoBehaviour
{
    [SerializeField]
    InputField _p1NameField, _p2NameField;

    [SerializeField]
    Button _startButton;

    private void Awake()
    {
        OnConnectCountChange(CheckConection.Instance.WaitingForPlayerIndex);

        CheckConection.Instance.OnGetMicInput += OnGetMicInput;
        CheckConection.Instance.OnConnectCountChange += OnConnectCountChange;
    }

    private void OnDestroy()
    {
        CheckConection.Instance.OnGetMicInput -= OnGetMicInput;
        CheckConection.Instance.OnConnectCountChange -= OnConnectCountChange;
    }

    public void LoadGame()
    {
        AirConsole.instance.Broadcast(JToken.Parse("{\"adjustment\":\"1\"}"));
        GameManager.Instance.ChangeState(GameState.LoadGame);
    }

    void OnConnectCountChange (int count)
    {
        _startButton.interactable = false;

        if (count >= 0)
        {
            _p1NameField.interactable = true;
        }

        if (count >= 1)
        {
            _p2NameField.interactable = true;
        }
    }

    void OnGetMicInput(int index)
    {
        if (index == CheckConection.Instance.WaitingForPlayerIndex)
            _startButton.interactable = true;
    }
}
