using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Christ.Core
{
    public class ZoneFriendly : Zone
    {
        private void Start()
        {

        }

        public override void CollideWithHero()
        {
            // Speed up
        }

        public override void CollideWithEnemy()
        {
            // Slow down
        }
    }

}
