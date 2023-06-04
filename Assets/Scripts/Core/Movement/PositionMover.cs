using UnityEngine;

namespace Core.Movement
{
    public class PositionMover : DirectionalMover
    {
        public PositionMover(Rigidbody2D rigidbody) : base(rigidbody) { }

        public override void Move(Vector2 newPosition, float distanceDelta)
        {
            var currentPosition = Rigidbody.position;
            
            if (newPosition == currentPosition)
                return;
            
            var direction = (newPosition - currentPosition).normalized;
            
            SetDirection(direction);
            Rigidbody.position = Vector2.MoveTowards(currentPosition, newPosition, distanceDelta);
        }
    }
}