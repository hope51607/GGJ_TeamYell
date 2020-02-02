using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class VideoPlayerController : MonoBehaviour
{
   public VideoPlayer videoPlayer;
   void Awake()
   {
        videoPlayer.url = Application.streamingAssetsPath + "/" + "opening.mp4";
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
}
 