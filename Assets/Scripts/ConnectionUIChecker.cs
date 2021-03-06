﻿using NDream.AirConsole;
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
    Text _p1FieldText, _p2FieldText;

    [SerializeField]
    Text _p1FieldPlaceholder, _p2FieldPlaceholder;
    [SerializeField]
    Button _startButton;

    private void Awake()
    {
        OnConnectCountChange(CheckConection.Instance.WaitingForPlayerIndex);

        CheckConection.Instance.OnGetMicInput += OnGetMicInput;
        CheckConection.Instance.OnConnectCountChange += OnConnectCountChange;
        CheckConection.Instance.OnClickLoadGame += LoadGame;
    }

    private void OnDestroy()
    {
        CheckConection.Instance.OnGetMicInput -= OnGetMicInput;
        CheckConection.Instance.OnConnectCountChange -= OnConnectCountChange;
        CheckConection.Instance.OnClickLoadGame -= LoadGame;
    }

    public void LoadGame()
    {
        AirConsole.instance.Broadcast(JToken.Parse("{\"adjustment\":\"1\"}"));
        GameManager.Instance.ChangeState(GameState.LoadGame);

        GameManager.Instance.PlayerNames[0] = _p1FieldText.text;
        GameManager.Instance.PlayerNames[1] = _p2FieldText.text;
    }

    void OnConnectCountChange (int count)
    {
        _startButton.interactable = false;

        if (count >= 0)
        {
            _p1NameField.interactable = true;
            _p1FieldPlaceholder.text = _p1FieldPlaceholder.text.Replace(" Not", "");
        }

        if (count >= 1)
        {
            _p2NameField.interactable = true;
            _p2FieldPlaceholder.text = _p2FieldPlaceholder.text.Replace(" Not", "");
        }
    }

    void OnGetMicInput(int index)
    {
        if (index == CheckConection.Instance.WaitingForPlayerIndex)
            _startButton.interactable = true;
    }
}
