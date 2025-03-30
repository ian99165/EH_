using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource audioSource;
    
//SoundManager.Instance.PlaySound(SoundManager.Instance.clickSound);
    [Header("系統")]
    public AudioClip click;
    public AudioClip pickUp;
    [Header("按鈕")]
    public AudioClip pickDown;
    [Header("門")]
    public AudioClip lockDoor;
    public AudioClip openDoor;
    public AudioClip closeDoor;
    [Header("櫃子")]
    public AudioClip lockCabinet;
    public AudioClip openCabinet;
    public AudioClip closeCabinet;
    public AudioClip lockCabinetDrawer;
    public AudioClip openCabinetDrawer;
    public AudioClip closeCabinetDrawer;
    [Header("抽屜")]
    public AudioClip lockDrawer;
    public AudioClip openDrawer;
    public AudioClip closeDrawer;
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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