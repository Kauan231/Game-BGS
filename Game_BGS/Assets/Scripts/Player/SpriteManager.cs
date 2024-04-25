using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(Animator))]
    public class SpriteManager : MonoBehaviour
    {
        private Animator anim;
        private void Awake() {
            anim = GetComponent<Animator>();
        }

        private void Move() {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            anim.SetInteger("X", (int)horizontal);
            anim.SetInteger("Y", (int)vertical);
        }

        private void Update() {
            Move();
        }
    }

}
