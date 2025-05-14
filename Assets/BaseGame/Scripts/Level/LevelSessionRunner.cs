using System.Linq;
using UnityEngine;
using BaseGame.Scripts.Figure;

namespace BaseGame.Scripts.Level
{
    public class LevelSessionRunner : MonoBehaviour
    {
        [SerializeField] private ResettableSpawner _spawner;

        public int ActiveOnField => _spawner.ActiveCount;

        private void Start()
        {
            BeginLevel(_spawner.MaxFigures);
        }
        
        public void BeginLevel(int count)
        {
            ClearField();
            
            _spawner.Spawn(count);
        }
        
        public void ResetLevel(int spawnCount)
        {
            _spawner.Stop();
            
            BeginLevel(spawnCount);
        }
        
        public void RemoveFigure(FigureBehaviour figureBehaviour)
        {
            if (figureBehaviour == null)
                return;
            
            figureBehaviour.PrepareForRemoval();
            _spawner.ReturnToPool(figureBehaviour);
        }

        private void ClearField()
        {
            _spawner.Stop();
            foreach (FigureBehaviour figure in GetComponentsInChildren<FigureBehaviour>(true).Where(f => f.IsActive))
            {
                figure.PrepareForRemoval();
                _spawner.ReturnToPool(figure);
            }
        }
    }
}