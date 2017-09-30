using UnityEngine;

namespace SITC.Tools
{
    public static class UnityHelper
    {
        public static Vector3 SetZ(this Vector3 vector, float z)
        {
            vector.z = z;
            return vector;
        }

        public static Vector3 RotateInPlane(this Vector3 vector, float angle)
        {
            float x = vector.x;
            float y = vector.y;

            vector.x = Mathf.Cos(angle * Mathf.Deg2Rad) * x - Mathf.Sin(angle * Mathf.Deg2Rad) * y;
            vector.y = Mathf.Sin(angle * Mathf.Deg2Rad) * x + Mathf.Cos(angle * Mathf.Deg2Rad) * y;

            return vector;
        }
    }
}