using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI{
    public class NinjaConfiguration : MonoBehaviour
    {
        public readonly float existantialReward = .05f;
        public readonly float collisionPunishment = -1f;
        public readonly float minSpawnRange = 0;
        public readonly float maxSpawnRange = 4;
    }
}