using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound {
        public AudioClip audio;
        public string title;
    }

    public List<Sound> sfx = new List<Sound>();
    [SerializeField] private AudioSource audioPlayer;

    public void ActivateSfx(string SfxName) {
        AudioClip toPlay = sfx.Find(x => x.title == SfxName).audio;
        audioPlayer.clip = toPlay;
        audioPlayer.Play();
    }
}
