using UnityEngine;

namespace BaseGame.Scripts.Data
{
    [CreateAssetMenu(fileName = "ShapeData", menuName = "Game/ShapeData")]
    public class ShapeData : ScriptableObject
    {
        [SerializeField] private ShapeType _shapeType;
        [SerializeField] private BaseColor _color;
        [SerializeField] private Sprite _shapeSprite;

        public ShapeType ShapeType => _shapeType;
        public BaseColor Color => _color;
        public Sprite ShapeSprite => _shapeSprite;
        
        public Color GetUnityColor()
        {
            switch (_color)
            {
                case BaseColor.Purple:
                    return new Color(0.5f, 0f, 0.5f);
                
                case BaseColor.Green:
                    return UnityEngine.Color.green;
                
                case BaseColor.Blue:
                    return UnityEngine.Color.blue;
                
                case BaseColor.Yellow:
                    return UnityEngine.Color.yellow;
                
                default:
                    return UnityEngine.Color.white;
            }
        }
    }
}