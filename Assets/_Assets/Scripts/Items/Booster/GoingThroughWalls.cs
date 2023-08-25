using UnityEngine;

public class GoingThroughWalls : IBoosterAbility
{
    private const int MAIN_PLAYER_LAYER = 10;
    private const int WALL_LAYER = 9;

    public void DoAbility(GameObject gameObject)
    {
        Physics.IgnoreLayerCollision(MAIN_PLAYER_LAYER, WALL_LAYER, true);
    }

    public void DisableAbility(GameObject gameObject)
    {
    }
}