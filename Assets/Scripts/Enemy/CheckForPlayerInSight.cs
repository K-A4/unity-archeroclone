using System;
using UnityEngine;

public class CheckForPlayerInSight
{
    private readonly EnemyFacade enemyFacade;
    private readonly Settings settings;
    private readonly Player player;

    public CheckForPlayerInSight(
        EnemyFacade enemyFacade,
        Settings settings,
        Player player)
    {
        this.enemyFacade = enemyFacade;
        this.settings = settings;
        this.player = player;
    }

    public bool CheckPlayerInSight()
    {
        if (Physics.Raycast(enemyFacade.Transform.position, player.Position - enemyFacade.Transform.position, out RaycastHit raycastHit, settings.SightRange))
        {
            if (raycastHit.transform.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    [Serializable]
    public class Settings
    {
        public float SightRange;
    }
}
