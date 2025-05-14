using UnityEngine;
using BaseGame.Scripts.UI;

namespace BaseGame.Scripts.Figure.SpecialFigure
{
    public class FrozenSpecial : SpecialBehavior
    {
        [SerializeField, Min(1)] private int _requiredUnfreeze = 3;
        
        private int _counter;
        
        public void SetModel(ActionBarModel model) => model.TripleRemoved += () => _counter++;

        public override void OnClickAttempt(FigureBehaviour figure,ref bool canClick)
        {
            if (_counter < _requiredUnfreeze)
                canClick = false;
        }
    }
}