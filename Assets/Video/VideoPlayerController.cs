using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class VideoPlayerController : MonoBehaviour
{
   public VideoPlayer videoPlayer;
   
   void Start()
   {
       videoPlayer.url = "https://docs.google.com/uc?export=download&id=1iyP5JYftGTQJP0ss6xXyhvE2QNhN2Slr";
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
 