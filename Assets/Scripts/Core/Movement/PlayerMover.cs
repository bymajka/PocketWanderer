using UnityEngine;

namespace Core.Movement
{
    public class PlayerMover : DirectionalMover
    {
        public PlayerMover(Rigidbody2D rigidbody) : base(rigidbody)
        {
        }

        public override void Move(Vector2 direction, float distanceDelta)
        {
            SetDirection(direction);
            Rigidbody.position += direction * distanceDelta;
        }
    }
}