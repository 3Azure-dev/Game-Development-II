using UnityEngine;

namespace DesignPatterns.StatePattern
{
    public class AttackState : IState
    {
        private Player player;

        public AttackState(Player player)
        {
            this.player = player;
        }

        public void Enter()
        {
            Debug.Log("Entering Attack State");

            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = 0f;
            Vector2 aimDirection = (mouseWorld - player.transform.position).normalized;

            player.Fire(aimDirection);
        }

        public void Execute()
        {
            // one-shot: fire once on Enter, then drop straight back to Idle
            player.sm.TransitionTo(player.sm.idleState);
        }

        public void Exit() { }
    }
}