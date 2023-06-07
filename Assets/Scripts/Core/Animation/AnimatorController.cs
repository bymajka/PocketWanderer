using UnityEngine;

namespace Core.Animation
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorController : MonoBehaviour
    {
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Moving = Animator.StringToHash("Moving");
        private static readonly int Dead = Animator.StringToHash("Dead");
        private static readonly int SpellCast = Animator.StringToHash("SpellCast");
        private static readonly int LastHorizontal = Animator.StringToHash("LastHorizontal");
        private static readonly int LastVertical = Animator.StringToHash("LastVertical");
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");

        private Animator _animator;
        private int _currentAnimationType;

        public void Initialize() => _animator = GetComponent<Animator>();

        public void SetAnimationType(AnimationType animationType)
        {
            switch (animationType)
            {
                case AnimationType.Attack:
                    _currentAnimationType = Attack;
                    break;
                case AnimationType.Dead:
                    _currentAnimationType = Dead;
                    break;
                case AnimationType.SpellCast:
                    _currentAnimationType = SpellCast;
                    break;
                default:
                    _currentAnimationType = Moving;
                    break;
            }
        }

        public void PlayAnimation() => _animator.SetBool(_currentAnimationType, true);

        public void StopAnimation() => _animator.SetBool(_currentAnimationType, false);

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