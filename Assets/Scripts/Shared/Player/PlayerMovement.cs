using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Christ.Shared
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        private Vector2 _inputMovement;
        private Vector2 _nextPosition;

        [SerializeField] private float _movementSpeed;

        private void Start()
        {
            _inputMovement = new Vector2(0f, 0f);

            //Time.timeScale = 0.1f;
        }

        private void FixedUpdate()
        {
            GetInputs();
            
            if (_inputMovement != Vector2.zero)
            {
                _nextPosition = (Vector2)transform.position + (_inputMovement * _movementSpeed * Time.fixedDeltaTime);
                _rigidbody2D.MovePosition(_nextPosition);
                // transform.GetChild(0).GetComponent<Rigidbody2D>().MovePosition(_nextPosition); // Mode defense circel

                PlayerGlobal.PlayerPosition = transform.position;

            }
        }

        private Vector2 GetInputs()
        {
            _inputMovement.x = Input.GetAxisRaw("Horizontal");
            _inputMovement.y = Input.GetAxisRaw("Vertical");
            _inputMovement = Vector2.ClampMagnitude(_inputMovement, 1f);

            return _inputMovement;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            Debug.Log("Collided with " + collision.name);
        }

    }

}
