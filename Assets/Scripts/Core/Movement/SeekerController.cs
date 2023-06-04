using Pathfinding;
using UnityEngine;

namespace Core.Movement
{
    public class SeekerController
    {
        private readonly Seeker _seeker;
        private readonly Rigidbody2D _movable;

        private Path _currentPath;
        private int _currentWayPoint;

        private Vector2 _currentDirection;

        public SeekerController(Seeker seeker, Rigidbody2D movable)
        {
            _seeker = seeker;
            _movable = movable;
        }

        public void CalculatePath(Vector2 destination)
        {
            Vector2 currentPosition = _movable.transform.position;
            _currentDirection = destination - currentPosition;
            _seeker.StartPath(currentPosition, destination, OnPathCalculated);
        }
        
        public bool TryGetMoveVector(out Vector2 moveVector)
        {
            moveVector = _movable.transform.position;
            
            if (_currentPath == null || _currentWayPoint >= _currentPath.vectorPath.Count
                                     || !TryGetMovementData(moveVector, out var waypointPosition, out var waypointDirection))
                return false;
            
            _currentDirection = waypointDirection;

            moveVector = waypointPosition;

            return true;
        }
        
        private void OnPathCalculated(Path path)
        {
            if (path.error)
                return;

            _currentPath = path;
            _currentWayPoint = 0;
        }
        
        private bool IsNextPointValid(Vector2 direction, Vector2 currentWaypointPosition)
        {
            if ((direction.x > 0 && _currentDirection.x > 0) || (direction.x < 0 && _currentDirection.x < 0) || 
                (direction.y > 0 && _currentDirection.y > 0) || (direction.y < 0 && _currentDirection.y < 0))
                return true;

            var nextWaypoint = _currentWayPoint + 1;
            if (nextWaypoint >= _currentPath.vectorPath.Count)
                return false;
            
            var nextWaypointPosition = (Vector2)_currentPath.vectorPath[nextWaypoint];
            var nextDirection = nextWaypointPosition - currentWaypointPosition;
            return (nextDirection.x < 0 && direction.x < 0) || ( nextDirection.x > 0 && direction.x > 0);
        }

        private bool TryGetMovementData(Vector2 currentPosition, out Vector2 waypointPosition, out Vector2 waypointDirection)
        {
            waypointDirection = Vector2.zero;
            waypointPosition = Vector2.zero;
            while(_currentWayPoint < _currentPath.vectorPath.Count)
            {
                waypointPosition = _currentPath.vectorPath[_currentWayPoint];
                waypointDirection = waypointPosition - currentPosition;
                if (Vector2.Distance(waypointPosition, currentPosition) > 0.05f && 
                    IsNextPointValid(waypointDirection, waypointPosition)) 
                    return true;

                _currentWayPoint++;
            }

            return false;
        }
    }
}