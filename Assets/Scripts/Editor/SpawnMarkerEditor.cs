using Assets.Scripts.Logic.EnemySpawners;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor
{
    [CustomEditor(typeof(SpawnMarker))]
    public class SpawnMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RandomCustomGizmo(SpawnMarker spawner, GizmoType gizmo)
        {
            Gizmos.color = new Color(255, 0, 0, 1);
            Gizmos.DrawSphere(spawner.transform.position, 0.5f);
        }
    }
}
