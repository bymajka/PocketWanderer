using EnemySystem.StateMachine;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyMover
    {
        private static readonly int LastHorizontal = Animator.StringToHash("LastHorizontal");
        private static readonly int LastVertical = Animator.StringToHash("LastVertical");
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");

        public Vector2 LastMoveDirection { get; private set; }

        private EnemyStateMachine _context;
        private Rigidbody2D _rigidbody;
        private PathfindingAgent _pathfindingAgent;
        private Vector2 _direction;
        private Vector2 _nextPosition;
        private float _speed;

        public void Initialize(EnemyStateMachine context)
        {
            _context = context;
            _rigidbody = context.EnemyRigidbody;
            _pathfindingAgent = context.PathfindingAgent;
            _speed = context.MovementSpeed;
        }

        public void Move()
        {
            _nextPosition = _pathfindingAgent.GetNextPosition();
            var currentPosition = _rigidbody.position;
            _direction = (_nextPosition - currentPosition).normalized;

            currentPosition += _direction * (_speed * Time.deltaTime);
            _rigidbody.position = currentPosition;
        }

        public void Stop()
        {
            _pathfindingAgent.Reset();
        }
        
        public void CheckDirection()
        {
            if (_direction is { x: 0f, y: 0f })
            {
                _context.Animator.SetFloat(LastHorizontal, LastMoveDirection.x);
                _context.Animator.SetFloat(LastVertical, LastMoveDirection.y);
            }
            else
            {
                LastMoveDirection = _direction;
            }
            _context.Animator.SetFloat(Horizontal, _direction.x);
            _context.Animator.SetFloat(Vertical, _direction.y);
        }
    }
}