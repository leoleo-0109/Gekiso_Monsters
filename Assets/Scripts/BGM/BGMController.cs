using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System;

public class BGMController : MonoBehaviour
{
    public AudioClip soundBGM; // èâä˙ÇÃBGM
    [SerializeField] private AudioSource currentBGM; // åªç›ÇÃBGM
    public AudioClip kiwamiBGM;
    public AudioClip ultimateBGM;
    public AudioClip superUltimateBGM;
    public AudioClip bossBGM; // É{ÉXÇÃBGM
    public AudioClip homeBGM;
    public AudioClip titleBGM;

    private void Awake()
    {
        int numMusicPlayers = FindObjectsOfType<BGMController>().Length;
        if (numMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            currentBGM = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {

    }

    public async void ChangeBGM()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(6f)); // ë“ã@èàóù
        currentBGM.clip = bossBGM;
        currentBGM.Play();
    }

    public void ChangeHomeBGM()
    {
        currentBGM.clip = homeBGM;
        currentBGM.Play();
    }

    public void ChangeKiwamiQuestBGM()
    {
        currentBGM.clip = kiwamiBGM;
        currentBGM.Play();
    }

    public void ChangeUltimateQuestBGM()
    {
        currentBGM.clip = ultimateBGM;
        currentBGM.Play();
    }

    public void ChangeSuperUltimateQuestBGM()
    {
        currentBGM.clip = superUltimateBGM;
        currentBGM.Play();
    }
    public void ChangeTitleBGM()
    {
        currentBGM.clip = titleBGM;
        currentBGM.Play();
    }

    public void StopBGM()
    {
        currentBGM.Stop();
    }
}