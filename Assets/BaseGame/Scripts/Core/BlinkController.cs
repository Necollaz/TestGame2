using System.Collections;
using UnityEngine;

namespace BaseGame.Scripts.Core
{
    public class BlinkController
    {
        private readonly MonoBehaviour _host;
        private readonly SpriteRenderer _renderer;
        
        private readonly float _speed;
        private readonly float _intensity;
        
        private Coroutine _routine;

        public BlinkController(MonoBehaviour host, SpriteRenderer renderer, float speed, float intensity)
        {
            _host = host;
            _renderer = renderer;
            _speed = speed;
            _intensity = intensity;
        }

        public void StartBlink(Color targetColor)
        {
            StopBlink();
            
            _routine = _host.StartCoroutine(BlinkRoutine(targetColor));
        }

        public void StopBlink()
        {
            if (_routine != null)
            {
                _host.StopCoroutine(_routine);
                _routine = null;
                _renderer.color = Color.white;
            }
        }

        private IEnumerator BlinkRoutine(Color target)
        {
            Color baseColor = Color.white;

            while (true)
            {
                float blendFactor = (Mathf.Sin(Time.time * _speed) * 0.5f + 0.5f) * _intensity;
                _renderer.color = Color.Lerp(baseColor, target, blendFactor);

                yield return null;
            }
        }
    }
}