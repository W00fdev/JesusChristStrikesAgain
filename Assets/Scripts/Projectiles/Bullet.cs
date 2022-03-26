using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChristGame
{
    public class Bullet : MonoBehaviour
    {
        public Vector2 Direction { set => _direction = value; }
        public float Speed { set => _speed = value; }

        private Rigidbody2D _rigidbody2D;

        [SerializeField]
        private Vector2 _velocity;
        [SerializeField]
        private Vector2 _direction;

        [SerializeField]
        private float _speed;

        //[SerializeField]
        private Vector2 _originPosition;

        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _originPosition = transform.position;
        }

        private void FixedUpdate()
        {
            if (_direction != Vector2.zero && !Mathf.Approximately(_speed, 0f))
            {
                _velocity = _direction * _speed * Time.fixedDeltaTime;
                _rigidbody2D.MovePosition((Vector2)transform.position + _velocity);
            }    
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //Debug.Log("Collides");
            if (other.TryGetComponent(out Enemy enemy))
            {
                enemy.Damage();
            }

            Destroy(gameObject);
            //RestartBullet();
        }
        private void RestartBullet()
        {
            transform.position = _originPosition;
        }
    }
}
