using TigerForge;
using UnityEngine;

public class Model : MonoBehaviour
{
    #region private

    [SerializeField] private float _seekerSizeMultiple;

    [Space(20)] [SerializeField] private Controller _controller;
    [SerializeField] private Transform _characterModel;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private Material _modelMaterial;

    #endregion

    private void Awake()
    {
        LoadComponents();
        ListenEvent();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _controller = GetComponentInParent<Controller>();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.SETTED_UP_GAMEPLAY, SetupModelSize);
    }

    private void Update()
    {
        // HandleInvisibleModelInRuntime();
    }

    private void HandleInvisibleModelInRuntime()
    {
        if (!GameplaySystem.Instance.IsSeekerGameplay() || _controller.GetInGameState().IsSeeker()) return;

        if (_controller.GetInGameState().IsCaught() || GameplaySystem.Instance.IsInHidingTimer())
        {
            _skinnedMeshRenderer.enabled = true;
            return;
        }

        _skinnedMeshRenderer.enabled = false;
    }

    private void SetupModelSize()
    {
        if (!_controller.GetInGameState().IsSeeker()) return;

        _characterModel.localScale *= _seekerSizeMultiple;
    }

    public void SetModelMaterial(Material material)
    {
        _modelMaterial = material;
        _skinnedMeshRenderer.material = material;
    }
}