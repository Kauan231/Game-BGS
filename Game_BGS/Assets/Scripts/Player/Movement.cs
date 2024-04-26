using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        [SerializeField]
        private float speed = 3f;

        private Rigidbody2D rb;
        private Vector2 movementDirection;
        
        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
        }
        
        void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            movementDirection = new Vector2(horizontal, vertical);  
        }

        private void FixedUpdate()
        {
            Vector3 movePosition = (speed * Time.fixedDeltaTime * movementDirection.normalized) + rb.position;
            rb.MovePosition(movePosition);
        }
    }

}
