using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public class Math
    {
        public static Vector3 GetPosition(Vector3 position, Vector3 direction, float angle, float distance)
        {
            Vector3 dir = Quaternion.Euler(0, angle, 0) * direction;
            dir *= distance;
            Vector3 newPos = position + dir;
            return newPos;
        }
    }
}
