﻿using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class CheckConection : MonoSingleton<CheckConection> {
    public int WaitingForPlayerIndex = -1;

    public delegate void OnGetMicInputDelegate(int index);
    public OnGetMicInputDelegate OnGetMicInput;

    public delegate void OnConnectCountChangeDelegate(int count);
    public OnConnectCountChangeDelegate OnConnectCountChange;

    public delegate void OnClickLoadGameDelegate();
    public OnClickLoadGameDelegate OnClickLoadGame;

    void Awake() {
        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onConnect += OnConnect;
        AirConsole.instance.onDisconnect += OnDisconnect;

        DontDestroyOnLoad(this);
    }

    private void OnDestroy()
    {
        AirConsole.instance.onMessage -= OnMessage;
        AirConsole.instance.onConnect -= OnConnect;
        AirConsole.instance.onDisconnect -= OnDisconnect;
    }

    void OnConnect(int deviceId)
    {
        AirConsole.instance.SetActivePlayers(2);
        WaitingForPlayerIndex = AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceId);

        OnConnectCountChange?.Invoke(WaitingForPlayerIndex);
    }

    void OnDisconnect(int deviceId)
    {
        AirConsole.instance.SetActivePlayers(0);
    }

    void OnMessage(int from, JToken data) {
        int playerNumber = AirConsole.instance.ConvertDeviceIdToPlayerNumber(from);
        if (playerNumber < 0)
            return;

        if (data["government_threshold"] != null) {
            print("government_threshold: " + data["government_threshold"]);

            float _inputThreshold = (float)data["government_threshold"];
            if (_inputThreshold > 0.01f)
                GameManager.Instance.micThresholds[playerNumber] = (float)data["government_threshold"];
            OnGetMicInput?.Invoke(playerNumber);
        }

        if (data["blow"] != null) {
            OnClickLoadGame?.Invoke();
        }
    }
}
