using UnityEditor;
using UnityEngine;

public class SeekerVision : MonoBehaviour
{
    #region private

    [SerializeField] private Transform _thisPlayer;

    [SerializeField] private CircleVision _circleVision;
    [SerializeField] private FrontVision _frontVision;

    private float _visionCircleRadius => GameplaySystem.Instance.GetSeekerCircleVisionRadius();

    #endregion

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _circleVision = GetComponentInChildren<CircleVision>();
        _frontVision = GetComponentInChildren<FrontVision>();
    }

    private void Update()
    {
        CheckCollidingAsSeekerVision();
    }

    private void CheckCollidingAsSeekerVision()
    {
        if (!CanCheckColliding()) return;

        foreach (var player in GameplaySystem.Instance.GetHiderList())
        {
            Controller playerController = player.GetComponent<Controller>();
            if (playerController.GetInGameState().IsCaught()) continue;

            if (_circleVision.IsObjInSeekerVision(player))
            {
                CatchTarget(playerController);
            }
        }
    }

    private void CatchTarget(Controller targetController)
    {
        targetController.SetCaughtState(true);
        Debug.Log("Catching.....................");
    }

    private bool CanCheckColliding()
    {
        if (GameplaySystem.Instance.IsInHidingTimer()) return false;

        return true;
    }

    private void OnDrawGizmos()
    {
        // Gizmos.DrawSphere(_inGamePlayer.position, visionCircleRadius);
        Handles.DrawWireDisc(_thisPlayer.position, new Vector3(0, 1, 0), _visionCircleRadius);
    }

    public Transform GetThisPlayerTransform()
    {
        return _thisPlayer;
    }
}