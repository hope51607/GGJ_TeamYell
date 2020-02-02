using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;
public class VideoPlayerController : MonoBehaviour
{

   void Awake()
   {
       AirConsole.instance.onMessage += OnMessage;
   }
   void Start()
   {
       PlayVideo();
   }
   
   void EndReached(UnityEngine.Video.VideoPlayer vp) 
    {
        GameManager.Instance.ChangeState(GameState.WaitForConnect);
    }
    public void Skip()
    {
        GameManager.Instance.ChangeState(GameState.WaitForConnect);
    }
    public void PlayVideo()
    {
         AudioManager.Instance.SwitchMusic("Plot");
    }

    void OnMessage(int from, JToken data)
    {
        int playerNumber = AirConsole.instance.ConvertDeviceIdToPlayerNumber(from);
        if (playerNumber < 0)
            return;
        if (data["blow"] != null) {
            Skip();
        }
    }

    private void OnDestroy()
    {
        AirConsole.instance.onMessage -= OnMessage;
    }
}
 