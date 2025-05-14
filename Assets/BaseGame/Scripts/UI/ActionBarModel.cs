using System;
using System.Collections.Generic;
using System.Linq;
using BaseGame.Scripts.Figure;

namespace BaseGame.Scripts.UI
{
    public class ActionBarModel
    {
        private readonly List<FigureBehaviour> _items = new List<FigureBehaviour>();
        private readonly int _figuresPerGroup = 3;
        private readonly int _capacity;
        
        public ActionBarModel(int capacity)
        {
            _capacity = capacity;
        }
        
        public event Action Full;
        public event Action TripleRemoved;
        public event Action<List<FigureBehaviour>> ItemsChanged;

        public bool AddItem(FigureBehaviour figureBehaviour)
        {
            if (_items.Count >= _capacity)
            {
                Full?.Invoke();
                
                return false;
            }

            _items.Add(figureBehaviour);
            
            ItemsChanged?.Invoke(_items);

            CheckTriples();
            
            return true;
        }
        
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _items.Count)
                return;
            
            _items.RemoveAt(index);
            
            ItemsChanged?.Invoke(_items);
        }

        private void CheckTriples()
        {
            var group = _items.GroupBy(figure => (figure.Data.Color, figure.Data.SweetnessCategory)).
                FirstOrDefault(group => group.Count() >= _figuresPerGroup);

            if (group == null)
                return;

            List<FigureBehaviour> toRemove = group.Take(_figuresPerGroup).ToList();
            
            foreach (FigureBehaviour figure in toRemove)
                figure.NotifyMatched(_items);
            
            foreach (FigureBehaviour figure in toRemove)
                _items.Remove(figure);

            ItemsChanged?.Invoke(_items);
            TripleRemoved?.Invoke();
        }
    }
}