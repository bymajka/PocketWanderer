using Entity.Behaviour;
using Entity.Movement;
using Pathfinding;
using StatsSystem;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyBehaviour : BaseEntityBehaviour
    {
        [SerializeField] public EnemyStats Stats;
        
        public Vector2 Direction { get; private set; }
        public Vector2 LastDirection { get; private set; }

        private Path _currentPath;
        private int _currentWayPoint;

        public Path CurrentPath
        {
            get
            {
                return _currentPath;
            }
            set
            {
                _currentPath = value;
            }
        }

        public int CurrentWayPoint
        {
            get
            {
                return _currentWayPoint;
            }
            set
            {
                _currentWayPoint = value;
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            DirectionalMover = new EntityDirectionalMover(Rigidbody, Animator, Stats.MovementSpeed);
        }

        public void Move()
        {
            if (_currentPath == null || _currentWayPoint >= _currentPath.vectorPath.Count) return;

            var currentPosition = Rigidbody.position;
            var nextPosition = (Vector2) _currentPath.vectorPath[_currentWayPoint];
            var direction = nextPosition - currentPosition;

            if (direction.x != 0)
            {
                if (nextPosition.x > 0 && nextPosition.x < direction.x ||
                    nextPosition.x < 0 && nextPosition.x > direction.x)
                {
                    direction.x = nextPosition.x;
                }
            }
            
            if (direction.y != 0)
            {
                if (nextPosition.y > 0 && nextPosition.y < direction.y ||
                    nextPosition.y < 0 && nextPosition.y > direction.y)
                {
                    direction.y = nextPosition.y;
                }
            }

            Direction = direction.normalized;
            if (Direction != new Vector2(0, 0))
            {
                LastDirection = Direction;
            }
            base.Move(Direction);

            if (Vector2.Distance(nextPosition, currentPosition) < 0.05f)
            {
                _currentWayPoint++;
            }
        }
    }
}