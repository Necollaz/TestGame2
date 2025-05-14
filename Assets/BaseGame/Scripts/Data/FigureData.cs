using UnityEngine;

namespace BaseGame.Scripts.Data
{
    [CreateAssetMenu(fileName = "FigureData", menuName = "Game/FigureData")]
    public class FigureData : ScriptableObject
    {
        [SerializeField] private ShapeType _shapeType;
        [SerializeField] private BaseColor _color;
        [SerializeField] private SweetnessCategory _sweetnessCategory;
        [SerializeField] private SpecialFigureType _specialType;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private PhysicsMaterial2D _physicsMaterial;
        [SerializeField, Min(0.01f)] private float _mass = 1f;
        
        public ShapeType ShapeType => _shapeType;
        public BaseColor Color => _color;
        public SweetnessCategory SweetnessCategory => _sweetnessCategory;
        public SpecialFigureType SpecialType => _specialType;
        public Sprite Sprite => _sprite;
        public PhysicsMaterial2D PhysicsMaterial => _physicsMaterial;
        public float Mass => _mass;
    }
}