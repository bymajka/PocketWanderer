using System.Collections;
using Core.Animation;
using ItemSystem;
using UnityEngine;

namespace EnemyController
{
    public class EnemyDeathState : EnemyBaseState, IDroppable
    {
        public EnemyDeathState(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory)
        {
        }

        public override void OnEnterState()
        {
            Debug.Log("Enemy entered in DEATH state.");
            Context.EnemyEntity.Animator.SetAnimationType(AnimationType.Dead);
            Context.EnemyEntity.Animator.PlayAnimation();
            DropItems();
            Context.RunCoroutine(DeathRoutine());
        }

        public override void OnUpdateState()
        {
        }

        public override void OnFixedUpdateState()
        {
        }

        public override void OnExitState()
        {
            Debug.Log("Enemy exited from DEATH state.");
        }

        public override void CheckSwitchStates()
        {
        }
        public void DropItems()
        {
            foreach (var item in Context.Inventory.Items)
            {
                GameObject newItem = new GameObject();
                newItem.AddComponent<Item>().ItemData = item.ItemData;
                newItem.AddComponent<SpriteRenderer>().sprite = item.ItemData.Icon;
                newItem.GetComponent<SpriteRenderer>().sortingOrder = 1;
                newItem.AddComponent<CircleCollider2D>().isTrigger = true;
                newItem.transform.localScale *= 1.5f;
                var position = Context.transform.position;
                newItem.transform.position = new Vector3(position.x + Random.Range(-Context.DropItemsRange, Context.DropItemsRange), 
                    position.y + Random.Range(-Context.DropItemsRange, Context.DropItemsRange), position.z);
            }
        }
        IEnumerator DeathRoutine()
        {
            yield return new WaitForSecondsRealtime(3f);
            Object.Destroy(Context.gameObject);
        }
    }
}