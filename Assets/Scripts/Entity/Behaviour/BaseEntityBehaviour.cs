using Core.Animation;
using UnityEngine;

namespace Entity.Behaviour
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(AnimatorController))]
    public class BaseEntityBehaviour : MonoBehaviour
    {
        public Rigidbody2D Rigidbody { get; private set; }
        public AnimatorController Animator { get; private set; }

        public virtual void Initialize()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<AnimatorController>();
            Animator.Initialize();
        }
    }
}