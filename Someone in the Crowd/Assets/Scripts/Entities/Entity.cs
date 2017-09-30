﻿using SITC.Tools;
using UnityEngine;

namespace SITC
{
    public class Entity : SitcBehaviour
    {
        #region API
        public void Move(Vector3 translation)
        {
            translation = translation.normalized;
            translation *= EntityConfiguration.EntitySpeed;

            transform.position += translation;
        }
        #endregion API
    }
}