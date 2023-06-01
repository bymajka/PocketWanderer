using UnityEngine;

namespace EnemySystem.StateMachine
{
    public class EnemyStateController
    {
        private Transform _target;
        private Transform _enemy;
        private LayerMask _layerMask;
        private float _visionDistance;
        private float _triggeredDistance;
        private float _fov;
        private float _attackDistance;

        public void Initialize(EnemyStateMachine context)
        {
            _target = context.Target;
            _enemy = context.EnemyTransform;
            _layerMask = context.VisibleLayers;
            _visionDistance = context.VisionDistance;
            _triggeredDistance = context.TriggeredDistance;
            _fov = context.FOV;
            _attackDistance = context.AttackDistance;
        }

        public bool CheckTargetVisibility(Vector2 enemyDirection)
        {
            float distanceToTarget = Vector2.Distance(_enemy.position, _target.position);
            if (distanceToTarget > _visionDistance) return false;
            Vector2 directionToTarget = (_target.position - _enemy.position).normalized;
            if (Vector2.Angle(enemyDirection, directionToTarget) > _fov / 2f &&
                distanceToTarget > _triggeredDistance) return false;
            var raycastHit2D = Physics2D.Raycast(_enemy.position, directionToTarget, _visionDistance, _layerMask);
            if (raycastHit2D.collider == null) return false;
            return raycastHit2D.collider.gameObject.CompareTag("Player");
        }

        public bool CheckChasePossibility()
        {
            return Vector2.Distance(_enemy.position, _target.position) <= _visionDistance;
        }

        public bool CheckTargetAttackAbility()
        {
            return Vector2.Distance(_enemy.position, _target.position) <= _attackDistance;
        }
    }
}