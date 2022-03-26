using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChristGame
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 1f;

        private Vector2 _originPosition;
        private Vector2 _directionToHero;

        [SerializeField]
        private float _ressurectTime = 1f;

        private Collider2D _collider2D;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _collider2D = GetComponent<Collider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originPosition = transform.position;
        }

        private void FixedUpdate()
        {
            _directionToHero = PlayerGlobal.PlayerPosition - (Vector2)transform.position;
            transform.Translate(_directionToHero * _speed * Time.fixedDeltaTime);
        }

        public void Damage()
        {
            _collider2D.enabled = false;
            _spriteRenderer.enabled = false;
            StartCoroutine(Ressurect());
        }

        private IEnumerator Ressurect()
        {
            yield return new WaitForSeconds(_ressurectTime);
            _collider2D.enabled = true;
            _spriteRenderer.enabled = true;
            transform.position = _originPosition;
        }
    }
}
