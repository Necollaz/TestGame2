using System.Collections.Generic;
using BaseGame.Scripts.UI;

namespace BaseGame.Scripts.Figure.SpecialFigure
{
    public class ExplosiveSpecial : SpecialBehavior
    {
        private ActionBarModel _barModel;
        
        public void SetModel(ActionBarModel model) => _barModel = model;

        public override void OnMatched(FigureBehaviour figure, List<FigureBehaviour> barContents)
        {
            if (_barModel == null)
                return;

            int index = barContents.IndexOf(figure);
            
            if (index > 0)
                _barModel.RemoveAt(index - 1);
            
            if (index < barContents.Count - 1)
                _barModel.RemoveAt(index);
        }
    }
}