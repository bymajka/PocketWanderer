using System;
using Entity.Animation;
using UnityEngine;

namespace Entity.Movement
{
    public class EntityDirectionalMover
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly EntityAnimationController _animator;
        private readonly float _movementSpeed;
        private readonly Transform _attackPoint;
        private readonly float _attackPointOffset;
        public Vector2 LastMovementDirection;

        public EntityDirectionalMover(Rigidbody2D rigidbody, EntityAnimationController animator, float movementSpeed,
            Transform attackPoint, float attackPointOffset)
        {
            _rigidbody = rigidbody;
            _animator = animator;
            _movementSpeed = movementSpeed;
            _attackPoint = attackPoint;
            _attackPointOffset = attackPointOffset;
            InitializeDirection();
        }

        public void Move(Vector2 direction)
        {
            CheckDirection(direction);
            _rigidbody.position += direction * (_movementSpeed * Time.deltaTime);
        }

        public void CheckDirection(Vector2 direction)
        {
            if (direction is { x: 0f, y: 0f })
            {
                _animator.SetLastDirection(LastMovementDirection);
            }
            else
            {
                LastMovementDirection = direction;
                _attackPoint.localPosition = new Vector2(direction.x * _attackPointOffset, direction.y * _attackPointOffset + 0.06F);
            }

            _animator.SetDirection(direction);
        }

        private void InitializeDirection()
        {
            LastMovementDirection = _rigidbody.position - Vector2.zero;
            _animator.SetLastDirection(LastMovementDirection);
            _animator.SetDirection(LastMovementDirection);
        }
    }
}