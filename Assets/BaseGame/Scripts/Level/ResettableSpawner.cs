using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseGame.Scripts.Core;
using BaseGame.Scripts.Data;
using BaseGame.Scripts.Figure;

namespace BaseGame.Scripts.Level
{
    public class ResettableSpawner : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private FigureBehaviour _figureBehaviourPrefab;
        [SerializeField] private List<FigureData> _templates;

        [Header("Settings")]
        [SerializeField, Min(1)] private int _maxFigures = 60;
        [SerializeField, Range(0.01f, 0.5f)] private float _spawnInterval = 0.05f;
        [SerializeField] private float _horizontalRange = 5f;

        private TripletFactory _factory;
        private ObjectPool<FigureBehaviour> _pool;
        private RandomProvider _random;
        private Coroutine _spawnRoutine;
        private IReadOnlyList<FigureData> _spawnList;
        
        private int _spawnSessionId; 
        private int _totalRequested;
        private bool _isSpawning;
        
        public int ActiveCount { get; private set; }
        public int TotalRequested => _totalRequested;
        public int MaxFigures => _maxFigures;
        
        public event Action SpawnCompleted;
        public event Action<FigureBehaviour> FigureSpawned;
        
        private void Awake()
        {
            _random = new RandomProvider();
            _factory = new TripletFactory(_templates, _random, _maxFigures);
            _pool = new ObjectPool<FigureBehaviour>(_figureBehaviourPrefab, _maxFigures, transform);
        }

        public void Spawn(int desiredCount)
        {
            _spawnSessionId++;
            
            if(_spawnRoutine != null)
            {
                StopCoroutine(_spawnRoutine);
                
                _isSpawning = false;
            }

            _spawnList = _factory.PrepareFigureList(desiredCount);
            _totalRequested = _spawnList.Count;
            ActiveCount = 0;
            _isSpawning = true;
            
            Debug.Log($"[Spawner] Prepared {_totalRequested} figures in session #{_spawnSessionId}.");
            
            _spawnRoutine = StartCoroutine(SpawnCoroutine(_spawnSessionId));
        }

        public void Stop()
        {
            _isSpawning = false;

            if(_spawnRoutine != null)
            {
                StopCoroutine(_spawnRoutine);
                _spawnRoutine = null;
            }
        }

        private IEnumerator SpawnCoroutine(int sessionId)
        {
            foreach(FigureData data in _spawnList)
            {
                if (!_isSpawning || sessionId != _spawnSessionId)
                    yield break;

                FigureBehaviour figureBehaviour = _pool.Get();
                figureBehaviour.transform.position = GetRandomPosition();
                figureBehaviour.Initialize(data);

                ActiveCount++;
                
                FigureSpawned?.Invoke(figureBehaviour);

                yield return new WaitForSeconds(_spawnInterval);
            }
            
            if (sessionId != _spawnSessionId)
                yield break;
            
            _isSpawning = false;
            _spawnRoutine = null;
            
            Debug.Log($"[Spawner] SpawnCompleted ({ActiveCount}) in session #{sessionId}.");
            
            SpawnCompleted?.Invoke();
        }

        private Vector3 GetRandomPosition()
        {
            Vector3 basePosition = _spawnPoint.position;
            float x = _random.Range(-_horizontalRange, _horizontalRange);

            return new Vector3(basePosition.x + x, basePosition.y, basePosition.z);
        }

        public void ReturnToPool(FigureBehaviour figureBehaviour)
        {
            int capturedSession = _spawnSessionId;
            
            StartCoroutine(ReturnLater(capturedSession, figureBehaviour));
        }
        
        private IEnumerator ReturnLater(int sessionId, FigureBehaviour figureBehaviour)
        {
            yield return new WaitForEndOfFrame();
            
            if (sessionId != _spawnSessionId)
                yield break;

            _pool.Return(figureBehaviour);
            ActiveCount = Mathf.Max(0, ActiveCount - 1);
            
            Debug.Log($"[Spawner] Returned one in session #{sessionId}, now {ActiveCount}.");
        }
    }
}