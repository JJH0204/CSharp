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
            FadeIn();
        }
    }

    public void ChangeBGM()
    {
        if (audioSource.isPlaying)
        {
            nCurrentIndex++;
            if (audioClips.Length <= nCurrentIndex)
            {
                nCurrentIndex = 0;
            }
            PlayBGM();
        }
    }

    public void StopBGM()
    {
        FadeOut();
        audioSource.Stop();
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }

    private void FadeOut()
    {
        StartCoroutine(CoroutineFadeOut());
    }

    private IEnumerator CoroutineFadeOut()
    {
        float fTime = 0;

        while (true)
        {
            fTime += Time.deltaTime;

            if (fTime > 3f)
            {
                audioSource.volume = 0;
                break;
            }

            audioSource.volume -= 0.005f;

            yield return null;
        }
    }

    private void FadeIn()
    {
        StartCoroutine(CoroutineFadeIn());
    }

    private IEnumerator CoroutineFadeIn()
    {
        float fTime = 0;

        while (true)
        {
            fTime += Time.deltaTime;

            if (fTime > 3f)
            {
                audioSource.volume = 1;
                break;
            }

            audioSource.volume += 0.005f;

            yield return null;
        }
    }
}
