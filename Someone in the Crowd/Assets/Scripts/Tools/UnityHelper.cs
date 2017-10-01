using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        public static void SetSprite(this SpriteRenderer renderer, Sprite sprite)
        {
            if (renderer != null)
            {
                renderer.sprite = sprite;
            }
        }
        
        public static void FlipX(this SpriteRenderer renderer, bool toggle)
        {
            if (renderer != null)
            {
                renderer.flipX = toggle;
            }
        }

        public static void SetColor(this SpriteRenderer renderer, Color color)
        {
            if (renderer != null)
            {
                renderer.color = color;
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

        #region Text
        public static void SetText(this Text text, string label)
        {
            if (text != null)
            {
                text.text = label;
            }
        }
        #endregion Text
    }
}