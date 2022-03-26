using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChristGame
{
    public class Bullet : MonoBehaviour
    {
        public Vector2 Direction 
        {
            set
            {
                _direction = value;
                _rayOriginOffset = _direction * SpriteOffset;
            }
        }
        public float Speed {
            set
            {
                _speed = value;
            }
        }

        public float SpriteOffset = 0.035f;

        public bool RigidbodyBased;

        private Rigidbody2D _rigidbody2D;

        private RaycastHit2D _hit2D;

        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _rayCheckDistance;

        [SerializeField]
        private Vector2 _velocity;
        [SerializeField]
        private Vector2 _direction;

        private Vector2 _rayOriginOffset;

        [SerializeField]
        private float _speed;

        private bool isAlive;

        private void Start()
        {
            isAlive = true;

            if (RigidbodyBased)
                _rigidbody2D = GetComponent<Rigidbody2D>();

            //if (!RigidbodyBased)
                //StartCoroutine(RayCheckDelay());
        }

        private void FixedUpdate()
        {
            if (RigidbodyBased)
            {
                if (_direction != Vector2.zero && !Mathf.Approximately(_speed, 0f))
                {
                    _velocity = _direction * _speed * Time.fixedDeltaTime;
                    _rigidbody2D.MovePosition((Vector2)transform.position + _velocity);
                }
            }

        }

        private void Update()
        {
            if (!RigidbodyBased)
            {
                if (_direction != Vector2.zero && !Mathf.Approximately(_speed, 0f))
                {
                    _velocity = _direction * _speed * Time.deltaTime;
                    transform.Translate(_velocity);
                    Raycheck();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                enemy.Damage();
            }

            isAlive = false;
            Destroy(gameObject);
        }
        private void RestartBullet()
        {
            //transform.position = _originPosition;
        }

        private IEnumerator RayCheckDelay()
        {
            while (isAlive)
            {
                yield return new WaitForSeconds(Time.deltaTime);
                Raycheck();
            }

        }
        private void Raycheck()
        {
            // Может добавить Enemy.isAlive, if Enemy.Distance < rayLenght ?
            _hit2D = Physics2D.Raycast((Vector2)transform.position + _rayOriginOffset, 
                _direction, _rayCheckDistance, _layerMask);

            if (_hit2D)
            {
                if (_hit2D.transform.TryGetComponent(out Enemy enemy))
                {
                    enemy.Damage();
                }

                isAlive = false;
                Destroy(gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay((Vector2)transform.position + _rayOriginOffset, _direction * _rayCheckDistance, Color.red);
        }
    }
}
