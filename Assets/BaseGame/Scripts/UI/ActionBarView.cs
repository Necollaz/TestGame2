using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BaseGame.Scripts.Figure;

namespace BaseGame.Scripts.UI
{
    public class ActionBarView : MonoBehaviour
    {
        [SerializeField] private List<Image> _backgroundSlots;
        [SerializeField] private List<Image> _foregroundSlots;

        private readonly float _slotSize = 20f;
        
        private ActionBarModel _model;
        
        public ActionBarModel Model => _model;
        
        public event Action Full;
        public event Action TripleRemoved;
        
        private void Awake() => InitializeModel();
        
        public bool AddItem(FigureBehaviour figureBehaviour) => _model.AddItem(figureBehaviour);

        public void Reset() => InitializeModel();
        
        private void InitializeModel()
        {
            if (_model != null)
            {
                _model.ItemsChanged -= RefreshUI;
                _model.Full -= OnFull;
                _model.TripleRemoved -= OnTripleRemoved;
            }

            _model = new ActionBarModel(_backgroundSlots.Count);
            
            _model.ItemsChanged += RefreshUI;
            _model.Full += OnFull;
            _model.TripleRemoved += OnTripleRemoved;
            
            RefreshUI(new List<FigureBehaviour>());
        }

        private void RefreshUI(List<FigureBehaviour> items)
        {
            for (int i = 0; i < _backgroundSlots.Count; i++)
            {
                if (i < items.Count)
                {
                    FigureBehaviour figure = items[i];
                    
                    string path = $"Backgrounds/{figure.CurrentShape}_{figure.Data.Color}";
                    _backgroundSlots[i].sprite = Resources.Load<Sprite>(path);
                    _backgroundSlots[i].color  = Color.white;
                    _backgroundSlots[i].gameObject.SetActive(true);
                    
                    _foregroundSlots[i].sprite = figure.Data.Sprite;
                    _foregroundSlots[i].color = Color.white;
                    _foregroundSlots[i].rectTransform.sizeDelta = new Vector2(_slotSize, _slotSize);
                    _foregroundSlots[i].gameObject.SetActive(true);
                }
                else
                {
                    _backgroundSlots[i].gameObject.SetActive(false);
                    _foregroundSlots[i].gameObject.SetActive(false);
                }
            }
        }
        
        private void OnFull()
        {
            Full?.Invoke();
        }

        private void OnTripleRemoved()
        {
            TripleRemoved?.Invoke();
        }
    }
}