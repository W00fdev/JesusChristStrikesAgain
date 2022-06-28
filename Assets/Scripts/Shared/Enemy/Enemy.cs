using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Christ.Shared
{
    public class Enemy : MonoBehaviour
    {
        public bool isAlive;

        [SerializeField]
        private float _speed = 1f;

        private Vector2 _originPosition;
        private Vector2 _directionToHero;

        [SerializeField]
        private float _ressurectTime = 1f;

        //private EnemyHealthable 

        private Collider2D _collider2D;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originPosition = transform.position;
            isAlive = true;
        }

        private void Update()
        {
            if (isAlive)
            {
                _directionToHero = PlayerGlobal.PlayerPosition - (Vector2)transform.position;
                transform.Translate(_directionToHero * _speed * Time.deltaTime);
            }
        }

        private void DeathBehaviour()
        {
            _collider2D.enabled = false;
            _spriteRenderer.enabled = false;
            isAlive = false;
        }

        public void RessurectBehavour()
        {
            _collider2D.enabled = true;
            _spriteRenderer.enabled = true;
            transform.position = _originPosition;
            isAlive = true;
        }

    }
}
