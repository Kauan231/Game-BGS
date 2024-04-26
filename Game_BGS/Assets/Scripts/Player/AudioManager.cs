using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        private AudioSource audio;
        [SerializeField] private AudioClip Walk;
        bool isWalking = false;

        private void Move() {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            if(horizontal != 0 || vertical != 0) {
                if(!isWalking) {
                    audio.clip = Walk;
                    audio.Play();
                    audio.loop = true;
                    isWalking = true;
                }
            }
            else {
                audio.loop = false;
                audio.clip = null;
                isWalking = false;
                audio.Stop();
            }
        }

        private void Awake() {
            audio  = GetComponent<AudioSource>();
        }

        void Update()
        {
            Move();
        }
    }

}
