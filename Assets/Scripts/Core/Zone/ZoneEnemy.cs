using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Christ.Core
{
    public class ZoneEnemy : Zone
    {
        private void Start()
        {

        }

        public override void CollideWithHero()
        {
            // Slow down
        }

        public override void CollideWithEnemy()
        {
            // Speed up
        }
    }
}

