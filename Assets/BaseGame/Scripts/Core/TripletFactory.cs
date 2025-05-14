using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BaseGame.Scripts.Data;

namespace BaseGame.Scripts.Core
{
    public class TripletFactory
    {
        private readonly RandomProvider _random;
        private readonly List<FigureData> _templates;
        private readonly int _defaultTotalCount;

        public TripletFactory(IEnumerable<FigureData> templates, RandomProvider random, int totalCount)
        {
            _templates = templates?.ToList() ?? throw new ArgumentNullException(nameof(templates));
            _random = random ?? throw new ArgumentNullException(nameof(random));
            _defaultTotalCount = totalCount - (totalCount % 3);
        }

        public IReadOnlyList<FigureData> PrepareFigureList(int desiredCount)
        {
            int mod = desiredCount % 3;
            
            if (mod == 1)
            {
                desiredCount += 2;
            }
            else if (mod == 2)
            {
                desiredCount += 1;
            }
            
            desiredCount = Mathf.Clamp(desiredCount, 0, _defaultTotalCount);
            
            if (desiredCount > 0 && desiredCount < 3)
                desiredCount = 3;
            
            int triplets = desiredCount / 3;
            List<FigureData> list = new List<FigureData>(desiredCount);
            
            for (int i = 0; i < triplets; i++)
            {
                FigureData template = _templates[_random.Range(0, _templates.Count)];
                
                list.Add(template);
                list.Add(template);
                list.Add(template);
            }

            return list;
        }
    }
}