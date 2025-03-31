using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource audioSource;
    /**
    SoundManager.Instance.PlaySound(SoundManager.Instance.moveCabinet);
    **/
    [Header("系統")]
    public AudioClip click;
    public AudioClip pickUp;
    public AudioClip window;
    [Header("電梯")]
    public AudioClip eleButton;
    public AudioClip eleDoor;
    [Header("門")]
    public AudioClip lockDoor;
    public AudioClip openDoor;
    public AudioClip closeDoor;
    [Header("櫃子")]
    public AudioClip lockCabinet;
    public AudioClip openCabinet;
    public AudioClip closeCabinet;
    public AudioClip openCabinetDrawer;
    public AudioClip closeCabinetDrawer;
    public AudioClip moveCabinet;
    [Header("抽屜")]
    public AudioClip openDrawer;
    public AudioClip closeDrawer;
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>(); // 如果沒有 AudioSource，則新增一個
            }
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }


    // 播放單次音效
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}