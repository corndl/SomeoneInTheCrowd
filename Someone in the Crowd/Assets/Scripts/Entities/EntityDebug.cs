using SITC.Tools;
using UnityEngine;

namespace SITC
{
    [RequireComponent(typeof(Entity))]
    public class EntityDebug : SitcBehaviour
    {
        #region Private members
        private Entity _entity = null;
        private Camera _camera = null;
        #endregion Private members

        #region Getters
        private Entity Entity { get { _entity = _entity ?? GetComponent<Entity>(); return _entity; } }
        private Camera Camera { get { _camera = _camera ?? FindObjectOfType<Camera>(); return _camera; } }
        #endregion Getters

        #region Lifecycle
        private void OnGUI()
        {
            base.DoUpdate();

            GUIStyle oki = new GUIStyle();
            string label = "oki";
            GUIContent content = new GUIContent(label);
            Vector2 size = oki.CalcSize(content);
            Vector3 screen = Camera.WorldToScreenPoint(transform.position);

            Rect rect = new Rect(screen.x, Screen.height - screen.y, size.x, size.y);
            //GUI.Box(rect, content);
        }
        #endregion Lifecycle
    }
}