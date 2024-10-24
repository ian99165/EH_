using UnityEngine;
using UnityEngine.Video;

public class End : MonoBehaviour
{
    public VideoPlayer videoPlayer; // 影片播放器
    public GameObject videoCanvas; // 用來顯示影片的UI Canvas

    private void Start()
    {
        videoCanvas.SetActive(false); // 開始時隱藏Canvas
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 確保是角色
        {
            PlayVideo();
        }
    }

    private void PlayVideo()
    {
        videoCanvas.SetActive(true); // 顯示Canvas
        videoPlayer.Play(); // 播放影片
    }

    private void Update()
    {
        // 當影片播放結束時隱藏Canvas
        if (videoPlayer.isPlaying == false && videoCanvas.activeSelf)
        {
            videoCanvas.SetActive(false);
        }
    }
}
