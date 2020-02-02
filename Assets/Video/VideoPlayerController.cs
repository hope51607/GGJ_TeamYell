using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Video;
public class VideoPlayerController : MonoBehaviour
{
   public VideoPlayer videoPlayer;
   void Awake()
   {
       AirConsole.instance.onMessage += OnMessage;
        videoPlayer.url = Application.streamingAssetsPath + "/" + "opening.webm";
   }
   void Start()
   {
       videoPlayer.Prepare();
       videoPlayer.prepareCompleted += PlayVideo;
   }
   
   void EndReached(UnityEngine.Video.VideoPlayer vp) 
    {
        GameManager.Instance.ChangeState(GameState.WaitForConnect);
    }
    public void Skip()
    {
        videoPlayer.loopPointReached -= EndReached;
        GameManager.Instance.ChangeState(GameState.WaitForConnect);
    }
    public void PlayVideo(UnityEngine.Video.VideoPlayer vp)
    {
        videoPlayer.Play();
        videoPlayer.loopPointReached += EndReached;
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
 