using UnityEngine;

namespace BaseGame.Scripts.Figure.SpecialFigure
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class HeavySpecial : SpecialBehavior
    {
        [SerializeField, Min(0f)] private float _gravityScale = 5f;
        
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        public override void OnSpawn(FigureBehaviour figure)
        {
            _rigidbody.gravityScale = _gravityScale;
        }
    }
}