using System.Collections.Generic;
using UnityEngine;
using BaseGame.Scripts.Core;
using BaseGame.Scripts.Data;
using BaseGame.Scripts.UI;

namespace BaseGame.Scripts.Figure.SpecialFigure
{
    [RequireComponent(typeof(HeavySpecial))]
    [RequireComponent(typeof(StickySpecial))]
    [RequireComponent(typeof(ExplosiveSpecial))]
    [RequireComponent(typeof(FrozenSpecial))]
    public class SpecialFigureAssigner : MonoBehaviour
    {
        [SerializeField] private HeavySpecial _heavy;
        [SerializeField] private StickySpecial _sticky;
        [SerializeField] private ExplosiveSpecial _explosive;
        [SerializeField] private FrozenSpecial _frozen;

        [Header("Spawn Chance")]
        [SerializeField, Range(0f, 1f)] private float _heavyChance = 0.2f;
        [SerializeField, Range(0f, 1f)] private float _stickyChance = 0.2f;
        [SerializeField, Range(0f, 1f)] private float _explosiveChance = 0.1f;
        [SerializeField, Range(0f, 1f)] private float _frozenChance = 0.1f;

        private SpecialBehavior[] _allBehaviors;
        private SpecialBehavior _current;
        private BehaviorSelector _selector;

        private void Awake()
        {
            _allBehaviors = new SpecialBehavior[] { _heavy, _sticky, _explosive, _frozen };
            _selector = new BehaviorSelector(_heavyChance, _stickyChance, _explosiveChance, _frozenChance);
        }

        public void Apply(SpecialFigureType type, FigureBehaviour figure, ActionBarModel barModel)
        {
            SpecialFigureType chosen = _selector.Select(type);

            foreach (SpecialBehavior behavior in _allBehaviors)
                behavior.enabled = false;

            _current = chosen switch
            {
                SpecialFigureType.Heavy => _heavy,
                SpecialFigureType.Sticky => _sticky,
                SpecialFigureType.Explosive => _explosive,
                SpecialFigureType.Frozen => _frozen,
                _ => null
            };

            if (_current != null)
            {
                _current.enabled = true;
                _current.OnSpawn(figure);

                if (_current is ExplosiveSpecial explosiveFigure)
                    explosiveFigure.SetModel(barModel);

                if (_current is FrozenSpecial frozenFigure)
                    frozenFigure.SetModel(barModel);
            }
        }

        public void HandleFall(FigureBehaviour figure) => _current?.OnFall(figure);

        public void HandleMatched(FigureBehaviour figure, List<FigureBehaviour> bar) => _current?.OnMatched(figure, bar);

        public bool HandleClick(FigureBehaviour figure)
        {
            bool can = true;

            _current?.OnClickAttempt(figure, ref can);

            return can;
        }
    }
}