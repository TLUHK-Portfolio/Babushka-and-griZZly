using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class VideoPlayerEndReachedEvent : MonoBehaviour
{
    public UnityEvent OnEndReached;
    public VideoPlayer videoPlayer;
 
    
    void Start()
    {
        videoPlayer.loopPointReached += VideoPlayer_loopPointReached;
    }
 
    private void VideoPlayer_loopPointReached(VideoPlayer source)
    {
        OnEndReached.Invoke();
    }
}
