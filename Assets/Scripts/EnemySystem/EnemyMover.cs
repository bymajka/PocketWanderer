using System;
using System.Collections;
using Pathfinding;
using UnityEngine;
using EnemySystem.StateMachine;

namespace EnemySystem
{
    public class EnemyMover
    {
        public Transform Target
        {
            get => _target;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                ResetMovement();
                _target = value;
                _coroutine = _context.RunCoroutine(SearchPathCoroutine());
            }
        }

        private readonly EnemyStateMachine _context;
        private readonly Seeker _seeker;
        private readonly Transform _enemyTransform;
        private readonly Rigidbody2D _enemyRigidbody;
        private Coroutine _coroutine;

        private Vector3 _previousTargetPosition;
        private Vector3 _destination;
        private Path _currentPath;
        private int _currentWayPoint;
        private float _stoppingDistance;

        private Transform _target;

        public EnemyMover(EnemyStateMachine context)
        {
            _context = context;
            _seeker = context.Seeker;
            _enemyRigidbody = _context.EnemyRigidbody;
            _enemyTransform = _context.EnemyTransform;
        }

        public void Move()
        {
            if (_currentPath == null || _currentWayPoint >= _currentPath.vectorPath.Count) return;

            var currentPosition = (Vector3)_enemyRigidbody.position;
            var waypointPosition = _currentPath.vectorPath[_currentWayPoint];
            //var waypointDirection = waypointPosition - currentPosition;
            
            if (Vector2.Distance(waypointPosition, currentPosition) < 0.05f)
            {
                _currentWayPoint++;
                return;
            }
            
            _enemyTransform.position = Vector2.MoveTowards(currentPosition, waypointPosition,
                _context.MovementSpeed * Time.deltaTime);
        }
        
        public void ResetMovement()
        {
            if (_coroutine != null) _context.KillCoroutine(_coroutine);

            _target = null;
            _currentPath = null;
        }

        private IEnumerator SearchPathCoroutine()
        {
            while (_target != null)
            {
                if (_target.position != _previousTargetPosition)
                {
                    _destination = _target.position;
                    _previousTargetPosition = _destination;
                    _seeker.StartPath(_enemyRigidbody.position, _destination, OnPathCompleted);
                }
                yield return new WaitForSeconds(_context.PathUpdateTime);
            }
            ResetMovement();
        }

        private void OnPathCompleted(Path path)
        {
            if (path.error) return;
            _currentPath = path;
            _currentWayPoint = 0;
        }
    }
}