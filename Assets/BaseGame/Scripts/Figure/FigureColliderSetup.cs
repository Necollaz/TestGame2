using UnityEngine;
using BaseGame.Scripts.Data;

namespace BaseGame.Scripts.Figure
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class FigureColliderSetup : MonoBehaviour
    {
        private CircleCollider2D _circle;
        private BoxCollider2D _box;
        private Collider2D _activeCollider;

        private void Awake()
        {
            _circle = GetComponent<CircleCollider2D>();
            _box = GetComponent<BoxCollider2D>();

            _circle.enabled = false;
            _box.enabled = false;
        }

        public void InitializeCollider(ShapeType shapeType)
        {
            if (_activeCollider != null)
                _activeCollider.enabled = false;

            switch (shapeType)
            {
                case ShapeType.Circle:
                case ShapeType.Triangle:
                    _circle.enabled = true;
                    _activeCollider = _circle;

                    break;

                case ShapeType.Square:
                    _box.enabled = true;
                    _activeCollider = _box;

                    break;
            }
        }

        public void DisableActiveCollider()
        {
            if (_activeCollider != null)
                _activeCollider.enabled = false;
            
            _circle.enabled = false;
            _box.enabled = false;

            _activeCollider = null;
        }
    }
}
