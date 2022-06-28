using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Christ.Core
{
    public class Healthable : MonoBehaviour
    {
        #nullable enable
        [SerializeField] private RectTransform? _healthBar = null;
        [SerializeField] private Animator? _animator = null;

        [SerializeField] protected string _damagedTrigger = "Damaged";

        [field: SerializeField] 
        public float MaxHp
        {
            get;
            private set;
        }

        [field: SerializeField]
        public float Hp
        {
            get;
            private set;
        }


        private void Start()
        {
            Hp = MaxHp;
            _healthBar.IfNotNull(UpdateHealthBar);
            _animator ??= GetComponent<Animator>();

            // Default initialization. No need in scenes switching yet.
        }

        // Can be a parameter to add modificator of attack
        public void Damage(float damage)
        {
            float healingMax = Hp - MaxHp;

            damage = Mathf.Clamp(damage, healingMax, MaxHp);
            Hp -= damage;

            _healthBar.IfNotNull(UpdateHealthBar);
            _animator.IfNotNull(PlayDamagedAnimation);

            if (Hp <= 0)
                Die();
        }

        public void Heal(float healing) => Damage(-healing);

        protected void UpdateHealthBar(RectTransform? healthBarTransform)
        {
            healthBarTransform!.anchoredPosition = new Vector2(-healthBarTransform.rect.width + (Hp / MaxHp) * healthBarTransform.rect.width, 50f);
        }

        protected void PlayDamagedAnimation(Animator? animator)
        {
            animator!.SetTrigger(_damagedTrigger);
        }

        protected virtual void Die()
        {
            // Destroy(gameObject);
        }


        #nullable disable
    }
}
