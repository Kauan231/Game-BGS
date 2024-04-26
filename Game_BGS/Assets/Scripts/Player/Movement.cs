using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        [SerializeField]
        private float speed = 3f;

        [SerializeField] private bool getSavedPosition;

        private Rigidbody2D rb;
        private Vector2 movementDirection;
        
        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
            if(getSavedPosition) {
                Vector3 SpawnpointFound = GameObject.Find(PlayerPrefs.GetString("Spawnpoint", "DefaultSpawn")).transform.position;
                transform.position = new Vector3(SpawnpointFound.x, SpawnpointFound.y, transform.position.z);
            }
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
