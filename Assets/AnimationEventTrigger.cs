using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventTrigger : MonoBehaviour
{
    public VideoPlayerController videoPlayer;
    public void SendSkipMessage()
    {
        videoPlayer.Skip();
    }
}
