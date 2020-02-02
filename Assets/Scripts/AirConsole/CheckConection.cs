using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CheckConection : MonoBehaviour {
    [SerializeField] InputField _p1NameField, _p2NameField;

    [SerializeField] Button _startButton;

    int _waitingForPlayerIndex;

    void Awake() {
        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onConnect += (int deviceId) => {
            AirConsole.instance.SetActivePlayers(2);
            int playerNumber = AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceId);

            if (playerNumber == 0) {
                _p1NameField.interactable = true;
                _waitingForPlayerIndex = 0;
            }
            else if (playerNumber == 1) {
                _startButton.interactable = false;

                _p2NameField.interactable = true;
                _waitingForPlayerIndex = 1;
            }
            else {
                Debug.Log("Player number != 0 or 1");
            }
        };

        AirConsole.instance.onDisconnect += (int deviceId) => { AirConsole.instance.SetActivePlayers(0); };
    }

    public void LoadGame() {
        AirConsole.instance.Broadcast(JToken.Parse("{\"adjustment\":\"1\"}"));

        GameManager.Instance.ChangeState(GameState.LoadGame);
    }

    void OnMessage(int from, JToken data) {
        int playerNumber = AirConsole.instance.ConvertDeviceIdToPlayerNumber(from);

        if (data["government_threshold"] != null) {
            print("government_threshold: " + data["government_threshold"]);

            GameManager.Instance.micThresholds[playerNumber] = (float)data["government_threshold"];

            if (_waitingForPlayerIndex == playerNumber)
                _startButton.interactable = true;
        }
    }
}
