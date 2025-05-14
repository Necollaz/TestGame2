using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BaseGame.Scripts.Data;
using BaseGame.Scripts.Figure;

namespace BaseGame.Scripts.UI
{
    public class ActionBarView : MonoBehaviour
    {
        [SerializeField] private List<Image> _slots;

        private readonly float _slotSize = 100f;
        
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

            _model = new ActionBarModel(_slots.Count);
            
            _model.ItemsChanged += RefreshUI;
            _model.Full += OnFull;
            _model.TripleRemoved += OnTripleRemoved;
            
            RefreshUI(new List<FigureBehaviour>());
        }

        private void RefreshUI(List<FigureBehaviour> items)
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                int dataIndex = i < items.Count ? i : -1;
                int slotIndex = i;

                if (dataIndex >= 0)
                {
                    FigureData figData = items[dataIndex].Data;
                    Image slot = _slots[slotIndex];

                    slot.sprite = figData.Sprite;
                    slot.color = Color.white;
                    slot.rectTransform.sizeDelta = new Vector2(_slotSize, _slotSize);
                    slot.gameObject.SetActive(true);
                }
                else
                {
                    _slots[slotIndex].gameObject.SetActive(false);
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