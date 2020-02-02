using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class VideoPlayerController : MonoBehaviour
{
   public VideoPlayer videoPlayer;
   
   void Start()
   {
       videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,"opening.mp4");
       videoPlayer.Play();
       videoPlayer.loopPointReached += EndReached;
       AudioManager.Instance.SwitchMusic("Plot");
   }
   
   void EndReached(UnityEngine.Video.VideoPlayer vp) 
    {
        GameManager.Instance.ChangeState(GameState.LoadGame);
    }
    public void Skip()
    {
        videoPlayer.loopPointReached -= EndReached;
        GameManager.Instance.ChangeState(GameState.LoadGame);
    }
}
 