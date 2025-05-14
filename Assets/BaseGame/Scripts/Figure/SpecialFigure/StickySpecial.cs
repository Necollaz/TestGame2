using UnityEngine;

namespace BaseGame.Scripts.Figure.SpecialFigure
{
    public class StickySpecial : SpecialBehavior
    {
        [SerializeField, Min(0f)] private float _stickDistance = 1f;
        
        private bool _stuck;

        public override void OnFall(FigureBehaviour figure)
        {
            if (_stuck)
                return;
            
            var mask = LayerMask.GetMask("Figure");
            
            Collider2D hit = Physics2D.OverlapCircle(figure.transform.position, _stickDistance, mask);
            
            if (hit != null && hit.TryGetComponent(out FigureBehaviour otherFigure) && otherFigure != figure)
            {
                otherFigure.transform.SetParent(figure.transform);
                _stuck = true;
            }
        }
    }
}