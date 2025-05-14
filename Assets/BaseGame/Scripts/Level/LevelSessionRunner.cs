using System.Linq;
using UnityEngine;
using BaseGame.Scripts.Figure;
using BaseGame.Scripts.UI;

namespace BaseGame.Scripts.Level
{
    public class LevelSessionRunner : MonoBehaviour
    {
        [SerializeField] private ResettableSpawner _spawner;
        [SerializeField] private ActionBarView _actionBarView;
        
        public int ActiveOnField => _spawner.ActiveCount;

        private void Start()
        {
            BeginLevel(_spawner.MaxFigures);
        }
        
        public void ResetLevel(int spawnCount)
        {
            _spawner.Stop();
            
            BeginLevel(spawnCount);
        }
        
        public void RemoveFigure(FigureBehaviour figure)
        {
            if (figure == null)
                return;
            
            figure.PrepareForRemoval();
            _spawner.ReturnToPool(figure);
        }
        
        private void BeginLevel(int count)
        {
            _spawner.Stop();
            
            foreach (FigureBehaviour figure in GetComponentsInChildren<FigureBehaviour>(true).Where(figure => figure.IsActive))
            {
                figure.PrepareForRemoval();
                _spawner.ReturnToPool(figure);
            }
            
            _spawner.Spawn(count, _actionBarView.Model);
        }
    }
}