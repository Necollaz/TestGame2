using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using BaseGame.Scripts.Data;
using BaseGame.Scripts.Figure.SpecialFigure;
using BaseGame.Scripts.UI;

namespace BaseGame.Scripts.Figure
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(FigureColliderSetup))]
    [RequireComponent(typeof(SpecialFigureAssigner))]
    public class FigureBehaviour : MonoBehaviour, IPointerClickHandler
    {
        private FigureData _data;
        private FigureColliderSetup _figureColliderSetup;
        private SpecialFigureAssigner _special;
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
            _special = GetComponent<SpecialFigureAssigner>();
        }
        
        private void FixedUpdate()
        {
            if (_rigidbody.linearVelocity.y < 0f)
                _special.HandleFall(this);
        }

        public void Initialize(FigureData data, ActionBarModel barModel)
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
            
            _special.Apply(data.SpecialType, this, barModel);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            bool canClick = _special.HandleClick(this);
            
            if (!canClick)
                return;
            
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
        
        public void NotifyMatched(List<FigureBehaviour> barContents) => _special.HandleMatched(this, barContents);
    }
}