using UnityEngine;
using BaseGame.Scripts.Level;
using BaseGame.Scripts.UI;

namespace BaseGame.Scripts.WinLose
{
    public class WinLoseEvaluator : MonoBehaviour
    {
        [Header("Game Components")]
        [SerializeField] private ResettableSpawner _spawner;
        [SerializeField] private LevelSessionRunner _levelSessionRunner;
        
        [Header("End Screens")]
        [SerializeField] private EndGamePopup _winPopup;
        [SerializeField] private EndGamePopup _losePopup;
        
        private bool _isGameOver;

        private void Awake()
        {
            _spawner.SpawnCompleted += OnSpawnCompleted;
        }

        private void OnDestroy()
        {
            _spawner.SpawnCompleted -= OnSpawnCompleted;
        }

        public void ResetState()
        {
            _isGameOver = false;
            
            _winPopup.Hide();
            _losePopup.Hide();
        }
        
        public void ShowWinImmediate()
        {
            if (_isGameOver)
                return;
            
            _winPopup.Show();
            _isGameOver = true;
        }
        
        public void OnLose()
        {
            if(_isGameOver)
                return;
            
            _losePopup.Show();
            _isGameOver = true;
        }
        
        public void OnFigureRemoved()
        {
            if (_isGameOver)
                return;
            
            int remaining = _levelSessionRunner.ActiveOnField;
            
            if (remaining == 0)
            {
                _winPopup.Show();
                _isGameOver = true;
            }
        }
        
        private void OnSpawnCompleted()
        {
            OnFigureRemoved();
        }
    }
}