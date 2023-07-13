using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource src;
    public AudioClip sfx1;
    // Start is called before the first frame update

    public void Buttom1()
    {
        src.clip = sfx1;
        src.Play();
    }

}
