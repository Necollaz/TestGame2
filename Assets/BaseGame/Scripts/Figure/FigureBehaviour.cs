using System;
using UnityEngine;
using UnityEngine.EventSystems;
using BaseGame.Scripts.Data;

namespace BaseGame.Scripts.Figure
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(FigureColliderSetup))]
    public class FigureBehaviour : MonoBehaviour, IPointerClickHandler
    {
        private FigureData _data;
        private FigureColliderSetup _figureColliderSetup;
        private SpriteRenderer _renderer;
        private Rigidbody2D _rigidbody;
        private Collider2D _collider;
        
        public FigureData Data => _data;
        public bool IsActive { get; private set; }
        
        public event Action<FigureBehaviour> Clicked;
        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _figureColliderSetup = GetComponent<FigureColliderSetup>();
        }

        public void Initialize(FigureData data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
            _renderer.sprite = data.Sprite;
            _renderer.enabled = true;
            _rigidbody.mass = data.Mass;
            
            if (data.PhysicsMaterial != null)
                _rigidbody.sharedMaterial = data.PhysicsMaterial;
            
            _rigidbody.simulated = true;
            
            _figureColliderSetup.InitializeCollider(data.ShapeType);
            IsActive = true;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke(this);
        }
        
        public void PrepareForRemoval()
        {
            _figureColliderSetup.DisableActiveCollider();
            _rigidbody.simulated = false;
            _renderer.enabled = false;
            IsActive = false;
            Clicked = null;
        }
    }
}