using UnityEngine;

namespace BaseGame.Scripts.Figure
{
    [RequireComponent(typeof(Transform))]
    public class FigureSpawnPointFollower : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _offsetY = 0.5f;

        private void LateUpdate()
        {
            Vector3 worldTopCenter = _camera.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0f));
            transform.position = new Vector3(worldTopCenter.x, worldTopCenter.y + _offsetY, transform.position.z);
        }
    }
}