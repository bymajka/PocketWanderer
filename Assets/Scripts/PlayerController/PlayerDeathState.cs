﻿using System.Collections;
using Core.Animation;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerController
{
    public class PlayerDeathState : PlayerBaseState
    {
        public PlayerDeathState(PlayerStateMachine context, PlayerStateFactory factory)
            : base(context, factory)
        {
        }

        public override void EnterState()
        {
            Debug.Log("Player entered in DEATH state.");
            Ctx.PlayerEntity.Animator.SetAnimationType(AnimationType.Dead);
            Ctx.PlayerEntity.Animator.PlayAnimation();
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            Debug.Log("Player exited from DEATH state.");
            Ctx.StartCoroutine(StartDeath());
        }

        public override void CheckSwitchStates()
        {
        }

        public override void InitializeSubState()
        {
        }

        private IEnumerator StartDeath()
        {
            yield return new WaitForSecondsRealtime(3f);
            SceneManager.LoadScene(0);
        }
    }
}