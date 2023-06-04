using System;
using PlayerSystem;
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
        private float _takenDamage;
        private float _attackCooldown;
        private DateTime _lastAttackTime;

        public void Initialize(EnemyStateMachine context)
        {
            _target = context.Target;
            _enemy = context.Enemy.transform;
            _layerMask = context.VisibleLayers;
            _visionDistance = context.Enemy.Stats.VisionDistance;
            _triggeredDistance = context.Enemy.Stats.TriggeredDistance;
            _fov = context.Enemy.Stats.FOV;
            _attackDistance = context.Enemy.Stats.AttackDistance;
            _attackCooldown = context.Enemy.Stats.AttackCooldown;
            _lastAttackTime = DateTime.MinValue;
        }

        public bool CheckIfFindTarget(Vector2 enemyDirection)
        {
            float distanceToTarget = Vector2.Distance(_enemy.position, _target.position);
            if (distanceToTarget > _visionDistance)
                return false;
            
            Vector2 directionToTarget = (_target.position - _enemy.position).normalized;
            if (Vector2.Angle(enemyDirection, directionToTarget) > _fov / 2f && distanceToTarget > _triggeredDistance)
                return false;
            
            var raycastHit2D = Physics2D.Raycast(_enemy.position, directionToTarget, _visionDistance, _layerMask);
            if (raycastHit2D.collider == null)
                return false;
            
            return raycastHit2D.collider.gameObject.GetComponent<PlayerBehaviour>() != null;
        }

        public bool CheckIfCanChase()
        {
            return Vector2.Distance(_enemy.position, _target.position) <= _visionDistance;
        }

        public bool CheckIfCanAttack()
        {
            if ((DateTime.Now - _lastAttackTime).TotalSeconds < _attackCooldown)
            {
                return false;
            }
            
            return Vector2.Distance(_enemy.position, _target.position) <= _attackDistance;
        }

        public void ResetLastAttackTime()
        {
            _lastAttackTime = DateTime.Now;
        }
        
        public void TakeDamage(float statsDamage)
        {
            _takenDamage += statsDamage;
        }

        public bool CheckIfTookDamage(out float damage)
        {
            if (_takenDamage == 0)
            {
                damage = 0;
                return false;
            }

            damage = _takenDamage;
            _takenDamage = 0;
            return true;
        }
    }
}