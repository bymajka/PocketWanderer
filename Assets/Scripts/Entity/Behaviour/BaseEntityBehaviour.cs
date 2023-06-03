using Entity.Animation;
using Entity.Movement;
using UnityEngine;

namespace Entity.Behaviour
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class BaseEntityBehaviour : MonoBehaviour
    {
        protected Rigidbody2D Rigidbody;
        public EntityDirectionalMover DirectionalMover { get; protected set; }
        protected EntityAnimationController Animator;
        
        public virtual void Initialize()
        {
            Animator = new EntityAnimationController(GetComponent<Animator>());
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        public virtual void Move(Vector2 direction) => DirectionalMover.Move(direction);

        public void PlayMovingAnimation() => Animator.PlayMovingAnimation();
        public void PlayAttackAnimation() => Animator.PlayAttackAnimation();
        public void PlayDeathAnimation() => Animator.PlayDeathAnimation();
        public void StopMovingAnimation() => Animator.StopMovingAnimation();
        public void StopAttackAnimation() => Animator.StopAttackAnimation();
    }
}