using UnityEngine;
using UnityEngine.UI;
using BaseGame.Scripts.Figure;
using BaseGame.Scripts.Level;
using BaseGame.Scripts.UI;
using BaseGame.Scripts.WinLose;

namespace BaseGame.Scripts.Gameplay
{
    public class FigureCollectionSession : MonoBehaviour
    {
        [Header("Game Components")]
        [SerializeField] private FigureSpawner _spawner;
        [SerializeField] private LevelSessionRunner _levelSessionRunner;
        [SerializeField] private ActionBarView _actionBarView;
        [SerializeField] private Button _resetButton;
        [SerializeField] private WinLoseEvaluator _winLoseEvaluator;

        private readonly int _figuresPerGroup = 3;
        
        private int _collectedCount;
        
        private void Awake()
        {
            _spawner.FigureSpawned += OnFigureSpawned;
            _actionBarView.Full += _winLoseEvaluator.OnLose;
            
            _resetButton.onClick.AddListener(OnResetPressed);
        }

        private void OnDestroy()
        {
            _spawner.FigureSpawned -= OnFigureSpawned;
            _actionBarView.Full -= _winLoseEvaluator.OnLose;
            
            _resetButton.onClick.RemoveListener(OnResetPressed);
        }

        private void OnFigureClicked(FigureBehaviour figureBehaviour)
        {
            if (!_actionBarView.AddItem(figureBehaviour))
                return;

            figureBehaviour.Clicked -= OnFigureClicked;
            
            _levelSessionRunner.RemoveFigure(figureBehaviour);
            _collectedCount++;
            
            if (_collectedCount >= _spawner.TotalRequested)
            {
                _winLoseEvaluator.ShowWinImmediate();
                return;
            }
            
            _winLoseEvaluator.OnFigureRemoved();
        }

        private void OnFigureSpawned(FigureBehaviour figureBehaviour)
        {
            figureBehaviour.Clicked += OnFigureClicked;
        }

        private void OnResetPressed()
        {
            _spawner.Stop();
            _actionBarView.Reset();
            _winLoseEvaluator.ResetState();
            _collectedCount = 0;

            int remainingOnField = _levelSessionRunner.ActiveOnField;
            int groups = Mathf.Max(1, remainingOnField / _figuresPerGroup);
            int toSpawn = groups * _figuresPerGroup;
            _collectedCount = 0;
            
            _levelSessionRunner.ResetLevel(toSpawn);
        }
    }
}