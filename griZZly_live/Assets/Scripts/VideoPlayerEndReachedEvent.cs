using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class VideoPlayerEndReachedEvent : MonoBehaviour {
    public UnityEvent OnEndReached;
    public VideoPlayer videoPlayer;


    void Start() {
        AudioListener.volume = PlayerPrefs.GetFloat("VolumeValue", 100f) / 100f;
        videoPlayer.loopPointReached += VideoPlayer_loopPointReached;

        GameObject.Find("Intro").SetActive(true);
    }

    private void VideoPlayer_loopPointReached(VideoPlayer source) {
        OnEndReached.Invoke();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            OnEndReached.Invoke();
        }
    }
}