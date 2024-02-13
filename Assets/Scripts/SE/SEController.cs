using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEController : MonoBehaviour
{
    public AudioClip takeDamageSE;
    public AudioClip takeHealSE;
    public AudioClip destroyBossSE;
    public AudioClip clickSE;
    public AudioClip menuSE;

    public void TakeDamageSEPlay()
    {
        GetComponent<AudioSource>().PlayOneShot(takeDamageSE);
    }

    public void TakeHealSEPlay()
    {
        GetComponent<AudioSource>().PlayOneShot(takeHealSE);
    }

    public void DestroyBossSEPlay()
    {
        GetComponent<AudioSource>().PlayOneShot(destroyBossSE);
    }

    public void ClickSEPlay()
    {
        GetComponent<AudioSource>().PlayOneShot(clickSE);
    }

    public void MenuSEPlay()
    {
        GetComponent<AudioSource>().PlayOneShot(menuSE);
    }
}
