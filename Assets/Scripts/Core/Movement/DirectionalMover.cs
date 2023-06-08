using UnityEngine;

namespace Core.Movement
{
    public abstract class DirectionalMover
    {
        protected readonly Rigidbody2D Rigidbody;
        
        private Vector2 _direction;
        
        public Vector2 LastMovementDirection { get; set; }

        protected DirectionalMover(Rigidbody2D rigidbody)
        {
            Rigidbody = rigidbody;
        }

        public abstract void Move(Vector2 direction, float distanceDelta);

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