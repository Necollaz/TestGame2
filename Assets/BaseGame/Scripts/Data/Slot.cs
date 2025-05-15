using System;
using UnityEngine;
using UnityEngine.UI;

namespace BaseGame.Scripts.Data
{
    [Serializable]
    public struct Slot
    {
        [SerializeField] private Image _background;
        [SerializeField] private Image _foreground;

        public Image Background => _background;
        public Image Foreground => _foreground;
    }
}