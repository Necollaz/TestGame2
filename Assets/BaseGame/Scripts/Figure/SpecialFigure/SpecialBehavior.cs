using System.Collections.Generic;
using UnityEngine;

namespace BaseGame.Scripts.Figure.SpecialFigure
{
    public abstract class SpecialBehavior : MonoBehaviour
    {
        public virtual void OnSpawn(FigureBehaviour figure)
        {
        }

        public virtual void OnFall(FigureBehaviour figure)
        {
        }

        public virtual void OnMatched(FigureBehaviour figure, List<FigureBehaviour> barContents)
        {
        }

        public virtual void OnClickAttempt(FigureBehaviour figure,ref bool canClick)
        {
        }
    }
}