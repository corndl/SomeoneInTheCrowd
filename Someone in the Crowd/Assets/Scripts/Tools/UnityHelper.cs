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
    }
}