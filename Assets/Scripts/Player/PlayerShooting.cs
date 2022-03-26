using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChristGame
{
    public class PlayerShooting : MonoBehaviour
    {
        public Transform bulletPool;

        [SerializeField]
        private GameObject _bulletPrefab;
        [SerializeField]
        private float _bulletSpeed = 7f;

        [Header("Настройки радиуса атаки")]
//        [SerializeField]
//        private float _targetRadiusMax = 2f;
 //       [SerializeField]
//        private float _targetRadiusMedium = 1f;
        [SerializeField]
        private float _targetRadiusStart = 0.8f;
        [SerializeField]
        private float _radiusIncrement = 0.7f;

        [Header("Значения для отладки")]
        [SerializeField]
        private float _targetDistance = Mathf.Infinity;
        [SerializeField]
        private int _targetsCount;

        private int _radiusTier;

        [SerializeField]
        private float _targetSearchTick = 2f;

        [SerializeField]
        private LayerMask _targetLayer;

        private ContactFilter2D _contactFilter2D;
        [SerializeField]
        private Collider2D[] _targetsColliders = new Collider2D[10];

        private Collider2D _playerCollider;
        [SerializeField]
        private Collider2D _targetCollider;

        private void Start()
        {
            _contactFilter2D.ClearDepth();
            _contactFilter2D.ClearNormalAngle();
            _contactFilter2D.SetLayerMask(_targetLayer);

            _playerCollider = GetComponent<Collider2D>();

            StartCoroutine(AcquireTargetCoroutine());
        }

        private void FixedUpdate()
        {
        }

        IEnumerator AcquireTargetCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_targetSearchTick);
                AcquireTarget();
                Shoot();
            }
        }

        private void AcquireTarget()
        {
            float radius = _targetRadiusStart;
            _radiusTier = -1;
            for (int i = 0; i < 3; ++i)
            {
                _targetsCount = Physics2D.OverlapCircle(transform.position,
                    radius + _radiusIncrement * i, _contactFilter2D, _targetsColliders);

                if (_targetsCount > 0)
                {
                    _radiusTier = i + 1;
                    break;
                }
            }

            int targetIndex = -1;
            _targetDistance = Mathf.Infinity;
            for (int i = 0; i < _targetsCount; i++)
            {
                /*float currentDistance = _playerCollider.Distance(_targetsColliders[i]).distance;*/
                float currentDistance = Vector2.Distance(_playerCollider.transform.position,
                    _targetsColliders[i].transform.position);
                if (currentDistance <= _targetDistance)
                {
                    _targetDistance = currentDistance;
                    targetIndex = i;
                }
            }

            if (targetIndex != -1)
                _targetCollider = _targetsColliders[targetIndex];
        }

        private void Shoot()
        {
            Bullet bulletInstance = Instantiate(_bulletPrefab, bulletPool).GetComponent<Bullet>();
            bulletInstance.transform.position = transform.position;
            
            // It is better to have direction, because of auto-targeting bullets
            //bulletInstance.transform.up;

            bulletInstance.Direction = _targetCollider.transform.position - transform.position;
            bulletInstance.Speed = _bulletSpeed;
        }

        private void OnDrawGizmos()
        {
            if (_radiusTier != -1)
            {
                if (_radiusTier == 1)
                    Gizmos.color = Color.green;
                else
                    Gizmos.color = Color.gray;

                Gizmos.DrawWireSphere(transform.position, _targetRadiusStart);

                if (_radiusTier == 2)
                    Gizmos.color = Color.yellow;
                else
                    Gizmos.color = Color.gray;

                Gizmos.DrawWireSphere(transform.position, _targetRadiusStart + _radiusIncrement);

                if (_radiusTier == 3)
                    Gizmos.color = Color.cyan;
                else
                    Gizmos.color = Color.gray;

                Gizmos.DrawWireSphere(transform.position, _targetRadiusStart + _radiusIncrement * 2);
            }

            Gizmos.color = Color.black;
            for (int i = 0; i < _targetsCount; i++)
            {
                if (_targetsColliders[i])
                {
                    if (_targetsColliders[i] == _targetCollider)
                        Gizmos.color = Color.blue;
                    Gizmos.DrawLine(transform.position, _targetsColliders[i].transform.position);
                    if (_targetsColliders[i] == _targetCollider)
                        Gizmos.color = Color.black;
                }
            }
        }
    }
}
