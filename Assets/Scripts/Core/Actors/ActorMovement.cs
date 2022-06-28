using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Christ.Core
{
    public class ActorMovement : MonoBehaviour
    {
        public float Speed
        {
            get => _speed;
            set
            {
                float clampedValue = Mathf.Clamp(value, 0, _maxSpeed);
                _speed = clampedValue;
            }
        }

        [SerializeField] private float _speed;
        [SerializeField] private float _maxSpeed;

        [SerializeField] private bool _heroFollowing = false;

        [Tooltip("You can keep this field empty.")]
        [SerializeField] private Transform _target;


        private Rigidbody2D _rigidbody2D;


        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }

}