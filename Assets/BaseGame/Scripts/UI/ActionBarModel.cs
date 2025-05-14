using System;
using System.Collections.Generic;
using System.Linq;
using BaseGame.Scripts.Figure;

namespace BaseGame.Scripts.UI
{
    public class ActionBarModel
    {
        private readonly List<FigureBehaviour> _items = new List<FigureBehaviour>();
        private readonly int _capacity;
    
        public int Count => _items.Count;
        
        public event Action Full;
        public event Action TripleRemoved;
        public event Action<List<FigureBehaviour>> ItemsChanged;

        public ActionBarModel(int capacity)
        {
            _capacity = capacity;
        }

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

        public void Clear()
        {
            _items.Clear();
            
            ItemsChanged?.Invoke(_items);
        }

        private void CheckTriples()
        {
            var group = _items.GroupBy(figure => (figure.Data.Color, figure.Data.SweetnessCategory)).FirstOrDefault(group => group.Count() >= 3);

            if (group != null)
            {
                List<FigureBehaviour> toRemove = group.Take(3).ToList();
                
                foreach (FigureBehaviour figure in toRemove)
                    _items.Remove(figure);

                ItemsChanged?.Invoke(_items);
                TripleRemoved?.Invoke();
            }
        }
    }
}