using System;
using System.Collections;
using Pathfinding;
using UnityEngine;

namespace EnemySystem
{
    [RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SimpleSmoothModifier))]
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
        
        private IPathFindingListener _listener;
        private Seeker _seeker;
        private SimpleSmoothModifier _simpleSmoothModifier;
        private Rigidbody2D _enemyRigidbody;
        private Coroutine _coroutine;
        private Transform _target;

        private Vector2 _previousPosition;
        private Vector3 _previousTargetPosition;
        private Vector3 _destination;

        private void Awake()
        {
            _seeker = GetComponent<Seeker>();
            _enemyRigidbody = GetComponent<Rigidbody2D>();
            _listener = GetComponent<EnemyBehaviour>();
            _simpleSmoothModifier = GetComponent<SimpleSmoothModifier>();
        }

        public void Reset()
        {
            if (_coroutine != null) StopCoroutine(_coroutine);
            _target = null;
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
            var smoothPath = path;
            smoothPath.vectorPath = _simpleSmoothModifier.SmoothSimple(smoothPath.vectorPath);
            _listener.OnPathCompleted(smoothPath);
        }
    }
}