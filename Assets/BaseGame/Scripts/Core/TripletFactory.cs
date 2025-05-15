using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using BaseGame.Scripts.Data;

namespace BaseGame.Scripts.Core
{
    public class TripletFactory
    {
        private readonly List<FigureData> _templates;
        private readonly int _figuresPerGroup = 3;
        private readonly int _maxGroups;
        
        public TripletFactory(IEnumerable<FigureData> templates, int maxTotalFigures)
        {
            _templates = templates?.ToList() ?? throw new ArgumentNullException(nameof(templates));
            _maxGroups = Mathf.Max(1, maxTotalFigures / _figuresPerGroup);
        }
        
        public IReadOnlyList<FigureData> CreateByGroupCount(int groupCount)
        {
            groupCount = Mathf.Clamp(groupCount, 1, _maxGroups);
        
            List<FigureData> list = new List<FigureData>(groupCount * _figuresPerGroup);
            
            for (int i = 0; i < groupCount; i++)
            {
                FigureData template = _templates[Random.Range(0, _templates.Count)];
                
                list.Add(template);
                list.Add(template);
                list.Add(template);
            }
            
            return list;
        }
    }
}