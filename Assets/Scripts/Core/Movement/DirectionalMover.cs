using UnityEngine;

namespace Core.Movement
{
    public class DirectionalMover
    {
        protected readonly Rigidbody2D Rigidbody;
        
        private Vector2 _direction;
        
        public Vector2 LastMovementDirection { get; set; }

        public DirectionalMover(Rigidbody2D rigidbody)
        {
            Rigidbody = rigidbody;
        }

        public virtual void Move(Vector2 direction, float distanceDelta)
        {
            SetDirection(direction);
            Rigidbody.position += _direction * distanceDelta;
        }

        protected void SetDirection(Vector2 newDirection)
        {
            if (newDirection == _direction)
                return;

            _direction = newDirection;

            if (_direction != Vector2.zero)
            {
                LastMovementDirection = _direction;
            }
        }
    }
}