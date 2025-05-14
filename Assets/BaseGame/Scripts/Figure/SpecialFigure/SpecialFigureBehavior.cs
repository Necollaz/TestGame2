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
    public class SpecialFigureBehavior : MonoBehaviour
    {
        [SerializeField] private HeavySpecial _heavy;
        [SerializeField] private StickySpecial _sticky;
        [SerializeField] private ExplosiveSpecial _explosive;
        [SerializeField] private FrozenSpecial _frozen;

        [Header("Blink Settings")]
        [SerializeField, Min(0f)] private float _blinkSpeed = 2f;
        [SerializeField, Range(0f, 1f)] private float _blinkIntensity = 0.5f;

        [Header("Spawn Chance")]
        [SerializeField, Range(0f, 1f)] private float _heavyChance = 0.2f;
        [SerializeField, Range(0f, 1f)] private float _stickyChance = 0.2f;
        [SerializeField, Range(0f, 1f)] private float _explosiveChance = 0.1f;
        [SerializeField, Range(0f, 1f)] private float _frozenChance = 0.1f;

        private SpecialBehavior[] _allBehaviors;
        private SpecialBehavior _current;
        private BlinkController _blinkController;
        private BehaviorSelector _selector;
        private SpriteRenderer _renderer;
        private RandomProvider _random;

        private void Awake()
        {
            _random = new RandomProvider();
            _allBehaviors = new SpecialBehavior[] { _heavy, _sticky, _explosive, _frozen };
            
            _renderer = GetComponent<SpriteRenderer>();
            
            _selector = new BehaviorSelector(_random, _heavyChance, _stickyChance, _explosiveChance, _frozenChance);
            _blinkController = new BlinkController(this, _renderer, _blinkSpeed, _blinkIntensity);
        }

        public void Apply(SpecialFigureType type, FigureBehaviour figure, ActionBarModel barModel)
        {
            SpecialFigureType chosen = _selector.Select(type);

            foreach (SpecialBehavior behavior in _allBehaviors)
                behavior.enabled = false;

            _blinkController.StopBlink();

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

            Color blinkColor = chosen switch
            {
                SpecialFigureType.Heavy => Color.gray,
                SpecialFigureType.Sticky => Color.yellow,
                SpecialFigureType.Explosive => Color.red,
                SpecialFigureType.Frozen => Color.blue,
                _ => Color.clear
            };

            if (blinkColor != Color.clear)
                _blinkController.StartBlink(blinkColor);
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