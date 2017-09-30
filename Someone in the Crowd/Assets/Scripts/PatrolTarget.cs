using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolTarget : MonoBehaviour {

    public Color color;

    public bool alwaysShowGizmo;


    private void OnDrawGizmos()
    {
        if (alwaysShowGizmo)
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position, 0.1f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (alwaysShowGizmo == false)
        {
            foreach (Transform child in transform.parent)
            {
                Gizmos.color = color;
                Gizmos.DrawSphere(child.position, 0.3f);
            }
        }
        //_positions.ForEach(DrawTarget);
        //Gizmos.color = AiConfiguration.AiTargetLinkGizmoColor;
        //DrawLinks();
    }
}
