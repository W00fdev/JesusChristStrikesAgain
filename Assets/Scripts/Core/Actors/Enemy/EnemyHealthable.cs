using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Christ.Core
{
    public class EnemyHealthable : Healthable
    {
        [SerializeField] private float _ressurectTime;
        // [SerializeField] private bool _ressurectable;

        private Action _dieBehaviour;
        private Action _ressurectBehaviour;

        protected override void Die()
        {
            base.Die();

            if (Mathf.Approximately(_ressurectTime, 0f) == false)
            {
                StartCoroutine(Ressurect());
            }
        }

        private IEnumerator Ressurect()
        {
            yield return new WaitForSeconds(_ressurectTime);
        }
    }

}