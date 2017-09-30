﻿using System.Collections.Generic;
using UnityEngine;

namespace SITC.Tools
{
    public static class UnityHelper
    {
        #region Vector
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
        #endregion Vector

        #region Sprite
        public static void SetSprite (this SpriteRenderer renderer, Sprite sprite)
        {
            if (renderer != null)
            {
                renderer.sprite = sprite;
            }
        }
        #endregion Sprit

        #region List
        public static T GetRandom<T>(this List<T> list)
        {
            if (list == null
                || list.Count == 0)
            {
                return default(T);
            }

            int rand = Random.Range(0, list.Count);
            return list[rand];
        }
        #endregion List
    }
}