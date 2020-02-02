using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CheckConection : MonoBehaviour
{
    [SerializeField]
    InputField _p1NameField, _p2NameField;

    [SerializeField]
    Button _startButton;

    void Awake()
    {
        AirConsole.instance.onConnect += (int deviceId) => {
            AirConsole.instance.SetActivePlayers(2);
            int playerNumber =AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceId);

            if (playerNumber == 0)
            {
                _p1NameField.interactable = true;
                _startButton.interactable = true;
            }
            else if (playerNumber == 1)
            {
                _p2NameField.interactable = true;
            }
            else
            {
                Debug.Log("Player number != 0 or 1");
            }
        };

        AirConsole.instance.onDisconnect += (int deviceId)=>
        {
            AirConsole.instance.SetActivePlayers(0);
        };
    }

    public void LoadGame()
    {
        AirConsole.instance.Broadcast(JToken.Parse("{\"adjustment\":\"1\"}"));

        GameManager.Instance.ChangeState(GameState.LoadGame);
    }
}
