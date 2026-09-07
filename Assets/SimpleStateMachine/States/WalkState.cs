using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.StatePattern
{
    public class WalkState : IState
    {
        private Player player;

        // pass in any parameters you need in the constructors
        public WalkState(Player player)
        {
            this.player = player;
        }

        public void Enter()
        {
            // code that runs when we first enter the state
            Debug.Log("Entering Walk State");
        }

        // per-frame logic, include condition to transition to a new state
        public void Execute()
        {
            float moveValue = Input.GetAxis("Horizontal");
            player.rb.linearVelocityX = moveValue * player.speed;

            if (Input.GetButtonDown("Jump") || !player.isGrounded)
            {
                player.sm.TransitionTo(player.sm.jumpState);
                return;
            }


            if (Mathf.Abs(moveValue) < 0.01f)
                player.sm.TransitionTo(player.sm.idleState);
        }

        public void Exit()
        {
            // code that runs when we exit the state
            //Debug.Log("Exiting Walk State");
        }

    }
}
