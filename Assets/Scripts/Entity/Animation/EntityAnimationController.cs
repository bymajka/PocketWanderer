using UnityEngine;

namespace Entity.Animation
{
    public class EntityAnimationController
    {
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Moving = Animator.StringToHash("Moving");
        private static readonly int Dead = Animator.StringToHash("Dead");
        private static readonly int LastHorizontal = Animator.StringToHash("LastHorizontal");
        private static readonly int LastVertical = Animator.StringToHash("LastVertical");
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");

        private readonly Animator _animator;

        public EntityAnimationController(Animator animator)
        {
            _animator = animator;
        }

        public void PlayAttackAnimation() => _animator.SetBool(Attack, true);
        public void PlayMovingAnimation() => _animator.SetBool(Moving, true);
        public void PlayDeathAnimation() => _animator.SetBool(Dead, true);
        public void StopAttackAnimation() => _animator.SetBool(Attack, false);
        public void StopMovingAnimation() => _animator.SetBool(Moving, false);

        public void SetDirection(Vector2 direction)
        {
            _animator.SetFloat(Horizontal, direction.x);
            _animator.SetFloat(Vertical, direction.y);
        }

        public void SetLastDirection(Vector2 direction)
        {
            _animator.SetFloat(LastHorizontal, direction.x);
            _animator.SetFloat(LastVertical, direction.y);
        }
    }
}