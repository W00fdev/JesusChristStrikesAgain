using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Christ.Core
{
    public class ActorStats : MonoBehaviour
    {
        public float Speed { get => _movement.Speed; }
        public float Hp { get => _healthable.Hp; }
        public float MaxHp { get => _healthable.MaxHp; }

        private Healthable _healthable;
        private ActorMovement _movement;

        private void Awake()
        {
            _healthable = GetComponent<Healthable>();
            _movement = GetComponent<ActorMovement>();
        }

        public void Damage(float damage) => _healthable.Damage(damage);
        public void Heal(float healing) => _healthable.Heal(healing);
        public void Slow(float speed) => _movement.Speed = speed;
    }

}