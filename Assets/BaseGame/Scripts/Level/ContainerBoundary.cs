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
            float halfW = _worldWidth  * 0.5f;
            float halfH = _worldHeight * 0.5f;
            
            Vector2 tl = new Vector2(-halfW + _leftMargin   * _worldWidth, halfH - _topMargin    * _worldHeight);
            Vector2 bl = new Vector2(-halfW + _leftMargin   * _worldWidth, -halfH + _bottomMargin * _worldHeight);
            Vector2 br = new Vector2( halfW - _rightMargin  * _worldWidth, -halfH + _bottomMargin * _worldHeight);
            Vector2 tr = new Vector2( halfW - _rightMargin  * _worldWidth, halfH - _topMargin    * _worldHeight);
            
            _edge.points = new[] { tl, bl, br, tr };
        }
    }
}