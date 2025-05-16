using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using BaseGame.Scripts.Data;
using BaseGame.Scripts.Figure.SpecialFigure;
using BaseGame.Scripts.UI;

namespace BaseGame.Scripts.Figure
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(FigureColliderSetup))]
    [RequireComponent(typeof(SpecialFigureAssigner))]
    public class FigureBehaviour : MonoBehaviour, IPointerClickHandler
    {
        private FigureData _data;
        private ShapeType _currentShape;
        private FigureColliderSetup _figureColliderSetup;
        private SpecialFigureAssigner _special;
        private SpriteRenderer _backgroundRenderer;
        private SpriteRenderer _foregroundRenderer;
        private Rigidbody2D _rigidbody;
        
        public ShapeType CurrentShape => _currentShape;
        public FigureData Data => _data;
        public bool IsActive { get; private set; }
        
        public event Action<FigureBehaviour> Clicked;
        
        private void Awake()
        {
            SpriteRenderer[] rends = GetComponentsInChildren<SpriteRenderer>();
            _backgroundRenderer = rends.OrderBy(sprite => sprite.sortingOrder).First();
            _foregroundRenderer = rends.OrderBy(sprite => sprite.sortingOrder).Last();
            
            _rigidbody = GetComponent<Rigidbody2D>();
            _figureColliderSetup = GetComponent<FigureColliderSetup>();
            _special = GetComponent<SpecialFigureAssigner>();
        }
        
        private void FixedUpdate()
        {
            if (_rigidbody.linearVelocity.y < 0f)
                _special.HandleFall(this);
        }

        public void Initialize(FigureData data, ActionBarModel barModel, ShapeType overrideShape)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
            _currentShape = overrideShape;
            
            string path = $"Backgrounds/{overrideShape}_{data.Color}";
            _backgroundRenderer.sprite = Resources.Load<Sprite>(path);
            _backgroundRenderer.color = Color.white;
            _backgroundRenderer.enabled = true;
            
            _foregroundRenderer.sprite = data.Sprite;
            _foregroundRenderer.color  = Color.white;
            _foregroundRenderer.enabled = true;
            
            _rigidbody.mass = data.Mass;
            
            if (data.PhysicsMaterial != null)
                _rigidbody.sharedMaterial = data.PhysicsMaterial;
            
            _rigidbody.simulated = true;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            _rigidbody.interpolation = RigidbodyInterpolation2D.Extrapolate;
            
            _figureColliderSetup.InitializeCollider(overrideShape);
            _special.Apply(data.SpecialType, this, barModel);
            IsActive = true;
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
            _backgroundRenderer.enabled = false;
            _foregroundRenderer.enabled = false;
            IsActive = false;
            Clicked = null;
        }
        
        public void NotifyMatched(List<FigureBehaviour> barContents) => _special.HandleMatched(this, barContents);
    }
}