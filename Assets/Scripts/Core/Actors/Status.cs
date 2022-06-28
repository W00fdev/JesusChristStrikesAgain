using System.Collections;
using UnityEngine;

namespace Christ.Core
{
    public class Status : MonoBehaviour
    {
        public float TimeTick = 1f;
        public Operation EffectOperation;

        private Coroutine _tickCoroutine;
        private bool _coroutineWorking;

        public void StartStatus()
        {
            // EffectOperation.Reserved;

            if (Mathf.Approximately(TimeTick, 0f) == false)
            {
                _tickCoroutine = StartCoroutine(TickCoroutine());
            }
        }

        public void StopStatus()
        {
            if (Mathf.Approximately(TimeTick, 0f) == false)
            {
                _coroutineWorking = false;
                StopCoroutine(_tickCoroutine);
            }

            // EffectOperation.Reserved;
        }

        private IEnumerator TickCoroutine()
        {
            _coroutineWorking = true;

            while(_coroutineWorking)
            {
                yield return new WaitForSeconds(TimeTick);
            }
        }
    }
}
