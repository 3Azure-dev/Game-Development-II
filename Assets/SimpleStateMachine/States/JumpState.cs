using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.StatePattern
{
    public class JumpState : IState
    {
        private Player player;

        // pass in any parameters you need in the constructors
        public JumpState(Player player)
        {
            this.player = player;
        }

        public void Enter()
        {
            // code that runs when we first enter the state
            Debug.Log("Entering Jump State");

            // launch upward once, and only if this is a real jump off the
            // ground (entering because we walked off a ledge should just fall)
            if (player.isGrounded)
                player.rb.linearVelocityY = player.jumpSpeed;
        }

        // per-frame logic, include condition to transition to a new state
        public void Execute()
        {
            // air control: horizontal follows live input
            float moveValue = Input.GetAxis("Horizontal");
            player.rb.linearVelocityX = moveValue * player.speed;

            // land only once we're actually descending and back on the ground,
            // so the takeoff frame doesn't bounce us straight back out
            if (player.isGrounded && player.rb.linearVelocity.y <= 0.01f) {
                player.sm.TransitionTo(Mathf.Abs(moveValue) < 0.01f ? player.sm.idleState : player.sm.walkState);
            }
            //Debug.Log("Updating Jump State");

            //if (player.IsGrounded)
            //{
            //    if (Mathf.Abs(player.CharController.velocity.x) > 0.1f || Mathf.Abs(player.CharController.velocity.z) > 0.1f)
            //    {
            //        player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.idleState);
            //    }
            //    else
            //    {
            //        player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.walkState);
            //    }
            //}
        }

        public void Exit()
        {
            // code that runs when we exit the state
            //Debug.Log("Exiting Jump State");
        }

    }
}
