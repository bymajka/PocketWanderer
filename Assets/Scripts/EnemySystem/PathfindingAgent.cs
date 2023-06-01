using System;
using System.Collections;
using Pathfinding;
using UnityEngine;

namespace EnemySystem
{
    [RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PathfindingAgent : MonoBehaviour
    {
        public Transform Target
        {
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                Reset();
                _target = value;
                _coroutine = StartCoroutine(SearchPathCoroutine());
            }
        }

        [SerializeField] private float _pathUpdateTime;
        
        private Seeker _seeker;
        private Rigidbody2D _enemyRigidbody;
        private Coroutine _coroutine;

        private Vector2 _previousPosition;
        private Vector3 _previousTargetPosition;
        private Vector3 _destination;
        private Path _currentPath;
        private int _currentWayPoint;

        private Transform _target;

        private void Awake()
        {
            _seeker = GetComponent<Seeker>();
            _enemyRigidbody = GetComponent<Rigidbody2D>();
        }

        public Vector2 GetNextPosition()
        {
            if (_currentPath == null || _currentWayPoint >= _currentPath.vectorPath.Count) return _previousPosition + new Vector2(0.01f,0.01f);

            var currentPosition = _enemyRigidbody.position;
            var nextPosition = _currentPath.vectorPath[_currentWayPoint];
            _previousPosition = nextPosition;
            
            if (Vector2.Distance(nextPosition, currentPosition) < 0.05f)
            {
                _currentWayPoint++;
            }

            return nextPosition;
        }
        
        public void Reset()
        {
            if (_coroutine != null) StopCoroutine(_coroutine);
            _target = null;
            _currentPath = null;
        }

        private IEnumerator SearchPathCoroutine()
        {
            while (_target != null)
            {
                _destination = _target.position;
                if (_destination != _previousTargetPosition)
                {
                    _previousTargetPosition = _destination;
                    _seeker.StartPath(_enemyRigidbody.position, _destination, OnPathCompleted);
                }
                yield return new WaitForSeconds(_pathUpdateTime);
            }
        }

        private void OnPathCompleted(Path path)
        {
            if (path.error) return;
            _currentPath = path;
            _currentWayPoint = 0;
        }
    }
}