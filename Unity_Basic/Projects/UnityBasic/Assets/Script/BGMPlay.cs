using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class BGMPlay : MonoBehaviour
{
    private AudioSource audioSource; // cache
    private int nCurrentIndex = 0; // 현재 인덱스
    // private bool bIsPlaying = false; // 재생 중인지 확인
    [SerializeField] private AudioClip[] audioClips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBGM()
    {
        if (audioClips.Length > nCurrentIndex)
        {
            audioSource.clip = audioClips[nCurrentIndex];
            audioSource.Play();
        }
    }

    public void ChangeBGM()
    {
        nCurrentIndex++;
        if (audioClips.Length <= nCurrentIndex)
        {
            nCurrentIndex = 0;
        }
        PlayBGM();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }
}
