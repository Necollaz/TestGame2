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
        private readonly ShapeType[] _allShapes;
        private readonly int _figuresPerGroup = 3;
        private readonly int _maxGroups;
        
        public TripletFactory(IEnumerable<FigureData> templates, int maxTotalFigures)
        {
            _templates = templates?.ToList() ?? throw new ArgumentNullException(nameof(templates));
            _maxGroups = Mathf.Max(1, maxTotalFigures / _figuresPerGroup);
            _allShapes = (ShapeType[])Enum.GetValues(typeof(ShapeType));
        }
        
        public IReadOnlyList<SpawnInfo> CreateByGroupCount(int groupCount)
        {
            groupCount = Mathf.Clamp(groupCount, 1, _maxGroups);
            List<SpawnInfo> list = new List<SpawnInfo>(groupCount * _figuresPerGroup);

            for (int i = 0; i < groupCount; i++)
            {
                FigureData template = _templates[Random.Range(0, _templates.Count)];
                ShapeType shape = _allShapes[Random.Range(0, _allShapes.Length)];
                
                for (int k = 0; k < _figuresPerGroup; k++)
                    list.Add(new SpawnInfo(template, shape));
            }
            
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                SpawnInfo tmp = list[i];
                
                list[i] = list[j];
                list[j] = tmp;
            }

            return list;
        }
    }
}