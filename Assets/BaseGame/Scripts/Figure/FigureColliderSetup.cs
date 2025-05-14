using UnityEngine;
using BaseGame.Scripts.Data;

namespace BaseGame.Scripts.Figure
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(PolygonCollider2D))]
    public class FigureColliderSetup : MonoBehaviour
    {
        [SerializeField, Range(0.1f,1f)] private float _colliderScale = 0.9f;
        
        private CircleCollider2D _circle;
        private BoxCollider2D _box;
        private PolygonCollider2D _poly;
        private Collider2D _activeCollider;

        private SpriteRenderer _renderer;

        private void Awake()
        {
            _circle = GetComponent<CircleCollider2D>();
            _box = GetComponent<BoxCollider2D>();
            _poly = GetComponent<PolygonCollider2D>();
            _renderer = GetComponent<SpriteRenderer>();
            
            _circle.enabled = false;
            _box.enabled = false;
            _poly.enabled = false;
        }
        
        public void InitializeCollider(ShapeType shapeType)
        {
            if(_activeCollider != null)
                _activeCollider.enabled = false;
            
            switch(shapeType)
            {
                case ShapeType.Circle:
                    _circle.radius = _renderer.sprite.bounds.extents.x * _colliderScale;
                    _circle.enabled = true;
                    _activeCollider = _circle;
                    
                    break;

                case ShapeType.Square:
                    Vector2 sizeSq = _renderer.sprite.bounds.size * _colliderScale;
                    _box.size = sizeSq;
                    _box.enabled = true;
                    _activeCollider = _box;
                    
                    break;

                case ShapeType.Triangle:
                    var size = _renderer.sprite.bounds.size * _colliderScale;
                    float w = size.x;
                    float h = size.y;
                    
                    _poly.SetPath(0, new Vector2[] { new Vector2(0, h * 0.5f), new Vector2(-w * 0.5f, -h * 0.5f), new Vector2( w * 0.5f, -h * 0.5f), });
                    _poly.enabled = true;
                    _activeCollider = _poly;
                    
                    break;
            }
        }
        
        public void DisableActiveCollider()
        {
            if (_activeCollider != null)
                _activeCollider.enabled = false;
        }
    }
}