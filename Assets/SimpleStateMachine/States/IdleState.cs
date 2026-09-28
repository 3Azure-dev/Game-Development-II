using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.StatePattern
{
    public class IdleState : IState
    {

        private Player player;

        public IdleState(Player player)
        {
            this.player = player;
        }

        public void Enter()
        {
            // code that runs when we first enter the state
            Debug.Log("Entering Idle State");
            // animation stays centralized in Player.FixedUpdate
        }

        // per-frame logic, include condition to transition to a new state
        public void Execute()
        {
            player.rb.linearVelocityX = 0;

            if (Input.GetButtonDown("Jump") || !player.isGrounded)
            {
                player.sm.TransitionTo(player.sm.jumpState);
                return;
            }

            float moveValue = Input.GetAxis("Horizontal");

            if (Mathf.Abs(moveValue) > 0.01f) player.sm.TransitionTo(player.sm.walkState);

            if (Input.GetMouseButtonDown(0) && player.CanAttack())
            {
                player.sm.TransitionTo(player.sm.attackState);
                return;
            }
        }

        public void Exit()
        {
            // code that runs when we exit the state
            //Debug.Log("Exiting Idle State");
        }
    }
}
