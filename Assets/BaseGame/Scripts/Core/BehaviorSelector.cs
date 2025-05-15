using BaseGame.Scripts.Data;
using UnityEngine;

namespace BaseGame.Scripts.Core
{
    public class BehaviorSelector
    {
        private readonly float _heavyChance;
        private readonly float _stickyChance;
        private readonly float _explosiveChance;
        private readonly float _frozenChance;

        public BehaviorSelector(float heavyChance, float stickyChance, float explosiveChance, float frozenChance)
        {
            _heavyChance = heavyChance;
            _stickyChance = stickyChance;
            _explosiveChance = explosiveChance;
            _frozenChance = frozenChance;
        }

        public SpecialFigureType Select(SpecialFigureType type)
        {
            float roll = Random.Range(0f, 1f);

            return type switch
            {
                SpecialFigureType.Heavy => roll < _heavyChance ? type : SpecialFigureType.None,
                SpecialFigureType.Sticky => roll < _stickyChance ? type : SpecialFigureType.None,
                SpecialFigureType.Explosive => roll < _explosiveChance ? type : SpecialFigureType.None,
                SpecialFigureType.Frozen => roll < _frozenChance ? type : SpecialFigureType.None,
                _ => SpecialFigureType.None
            };
        }
    }
}