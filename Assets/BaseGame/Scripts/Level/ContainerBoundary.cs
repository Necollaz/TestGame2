using UnityEngine;

namespace BaseGame.Scripts.Level
{
    [RequireComponent(typeof(EdgeCollider2D))]
    public class ContainerBoundary : MonoBehaviour
    {
        [SerializeField] private float _worldWidth = 6f;
        [SerializeField] private float _worldHeight = 10f;
        [SerializeField, Range(0f, 1f)] private float _leftMargin = 0.05f;
        [SerializeField, Range(0f, 1f)] private float _rightMargin = 0.05f;
        [SerializeField, Range(0f, 1f)] private float _bottomMargin = 0.1f;
        [SerializeField, Range(0f, 1f)] private float _topMargin = 0.1f;

        private readonly float _centerOffset = 0.5f;
        
        private EdgeCollider2D _edge;

        private void Awake()
        {
            _edge = GetComponent<EdgeCollider2D>();

            UpdateCollider();
        }

        private void OnValidate()
        {
            if(_edge == null)
                _edge = GetComponent<EdgeCollider2D>();

            UpdateCollider();
        }

        private void UpdateCollider()
        {
            float halfWidth = _worldWidth * _centerOffset;
            float halfHeight = _worldHeight * _centerOffset;
            
            Vector2 topLeftPoint = new Vector2(-halfWidth + _leftMargin * _worldWidth, halfHeight - _topMargin * _worldHeight);
            Vector2 bottomLeftPoint = new Vector2(-halfWidth + _leftMargin * _worldWidth, -halfHeight + _bottomMargin * _worldHeight);
            Vector2 bottomRightPoint = new Vector2(halfWidth - _rightMargin * _worldWidth, -halfHeight + _bottomMargin * _worldHeight);
            Vector2 topRightPoint = new Vector2(halfWidth - _rightMargin * _worldWidth, halfHeight - _topMargin * _worldHeight);
            
            _edge.points = new[] { topLeftPoint, bottomLeftPoint, bottomRightPoint, topRightPoint };
        }
        
        private void OnDrawGizmosSelected()
        {
            float halfW = _worldWidth  * 0.5f;
            float halfH = _worldHeight * 0.5f;

            Vector3 tl = transform.TransformPoint(new Vector3(-halfW + _leftMargin   * _worldWidth, halfH - _topMargin    * _worldHeight));
            Vector3 bl = transform.TransformPoint(new Vector3(-halfW + _leftMargin   * _worldWidth, -halfH + _bottomMargin * _worldHeight));
            Vector3 br = transform.TransformPoint(new Vector3( halfW - _rightMargin  * _worldWidth, -halfH + _bottomMargin * _worldHeight));
            Vector3 tr = transform.TransformPoint(new Vector3( halfW - _rightMargin  * _worldWidth, halfH - _topMargin    * _worldHeight));

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(tl, bl);
            Gizmos.DrawLine(bl, br);
            Gizmos.DrawLine(br, tr);
        }
    }
}