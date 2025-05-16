using UnityEngine;
using UnityEngine.UI;
using BaseGame.Scripts.Level;
using BaseGame.Scripts.UI;

namespace BaseGame.Scripts.WinLose
{
    public class WinLoseEvaluator : MonoBehaviour
    {
        [Header("Game Components")]
        [SerializeField] private FigureSpawner _spawner;
        [SerializeField] private LevelSessionRunner _levelSessionRunner;
        
        [Header("End Screens")]
        [SerializeField] private EndGame _win;
        [SerializeField] private EndGame _lose;
        [SerializeField] private Button _restartLevelButton;
        
        private RestartOnGameOver _restartOnGameOver;
        private bool _isGameOver;
        
        private void Awake()
        {
            _restartOnGameOver = new RestartOnGameOver(_win, _lose, _restartLevelButton);
            
            _spawner.SpawnCompleted += OnSpawnCompleted;
        }

        private void OnDestroy()
        {
            _spawner.SpawnCompleted -= OnSpawnCompleted;
        }

        public void ResetState()
        {
            _isGameOver = false;
            
            _win.Hide();
            _lose.Hide();
        }
        
        public void ShowWinImmediate()
        {
            if (_isGameOver)
                return;
            
            _win.Show();
            _isGameOver = true;
        }
        
        public void OnLose()
        {
            if(_isGameOver)
                return;
            
            _lose.Show();
            _isGameOver = true;
        }
        
        public void OnFigureRemoved()
        {
            if (_isGameOver)
                return;
            
            int remaining = _levelSessionRunner.ActiveOnField;
            
            if (remaining == 0)
            {
                _win.Show();
                _isGameOver = true;
            }
        }
        
        private void OnSpawnCompleted()
        {
            OnFigureRemoved();
        }
    }
}