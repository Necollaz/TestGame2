using UnityEngine;
using UnityEngine.UI;

namespace BaseGame.Scripts.UI
{
    [RequireComponent(typeof(Image))]
    public class FullscreenBackground : MonoBehaviour
    {
        [SerializeField] private Sprite _portraitSprite;
        [SerializeField] private Sprite _landscapeSprite;

        private Image _image;
        private AspectRatioFitter _aspectRatio;
        private ScreenOrientation _lastOrientation;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _aspectRatio = GetComponent<AspectRatioFitter>();

            RectTransform rectTransform = _image.rectTransform;
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = rectTransform.offsetMax = Vector2.zero;

            _lastOrientation = Screen.orientation;
            UpdateSpriteAndAspect();
        }

        private void Update()
        {
            if(Screen.orientation != _lastOrientation)
            {
                _lastOrientation = Screen.orientation;
                UpdateSpriteAndAspect();
            }
        }

        private void UpdateSpriteAndAspect()
        {
            bool isPortrait = Screen.height > Screen.width;
            Sprite sprite = isPortrait ? _portraitSprite : _landscapeSprite;

            if(sprite == null)
                return;
            
            _image.sprite = sprite;
            
            float aspectRatio = sprite.rect.width / sprite.rect.height;
            
            _aspectRatio.aspectRatio = aspectRatio;
            _aspectRatio.aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
        }
    }
}