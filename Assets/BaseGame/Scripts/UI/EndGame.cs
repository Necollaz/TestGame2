using System;
using System.Collections;
using UnityEngine;

namespace BaseGame.Scripts.UI
{
    public class EndGame : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration = 0.5f;

        public event Action Shown;
        private void Awake()
        {
            if (_canvasGroup != null)
            {
                _canvasGroup.alpha = 0f;
                _canvasGroup.interactable = false;
                _canvasGroup.blocksRaycasts = false;
            }
        }
        
        public void Show()
        {
            if (_canvasGroup == null)
                return;
            
            gameObject.SetActive(true);
            
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            float elapsed = 0f;
            
            while (elapsed < _fadeDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                float normalizedTime = Mathf.Clamp01(elapsed / _fadeDuration);
                _canvasGroup.alpha = normalizedTime;
                
                yield return null;
            }
            
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            
            Shown?.Invoke();
        }
        
        public void Hide()
        {
            if (_canvasGroup == null)
                return;
            
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            gameObject.SetActive(false);
        }
    }
}