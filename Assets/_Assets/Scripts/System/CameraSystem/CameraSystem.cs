using System.Collections;
using TigerForge;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [Header("Camera Setting")] [SerializeField]
    private float _cameraHeight;

    [SerializeField] private float _cameraDepth;

    [Space(10)] [Header("FOV Setting")] [SerializeField]
    private float _defaultFOV;

    [SerializeField] private float _zoomInFOV;
    [SerializeField] private float _menuFOV;
    [SerializeField] private float _fovChangeAmount;

    [Space(10)] [Header("Zoom Setting")] [SerializeField]
    private float _zoomInTime;

    [SerializeField] private float _zoomOutTime;

    [Space(20)] [SerializeField] private Transform mainCharacter;

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
        SetDefaultFOV();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.SETTED_UP_GAMEPLAY, HandleZoomingAtPlayer);
    }

    private void LateUpdate()
    {
        FollowMainCharacter();
    }

    private void SetDefaultFOV()
    {
        if (Camera.main != null)
            _menuFOV = Camera.main.fieldOfView;
    }

    private void HandleZoomingAtPlayer()
    {
        StartCoroutine(ZoomAtStartingGame());
    }

    private IEnumerator ZoomAtStartingGame()
    {
        while (true)
        {
            if (GameplaySystem.Instance.IsGameStarting())
            {
                StartCoroutine(ZoomAtPlayer());
                yield break;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator ZoomAtPlayer()
    {
        float zoomInValue = _zoomInFOV;

        if (!GameplaySystem.Instance.IsSeekerGameplay())
        {
            zoomInValue = _defaultFOV;
        }

        float hidingTime = GameplaySystem.Instance.GetHidingTime();
        int loopTime = Mathf.CeilToInt((_menuFOV - zoomInValue) / _fovChangeAmount);
        float waitingTime = _zoomInTime / loopTime;

        // Zoom In
        for (float i = _menuFOV; i > zoomInValue; i -= _fovChangeAmount)
        {
            // Debug.Log("FOV changing...");
            Camera.main.fieldOfView = i;
            yield return new WaitForSeconds(waitingTime);
        }

        yield return new WaitForSeconds(hidingTime - _zoomInTime);

        waitingTime = _zoomOutTime / loopTime;

        // Zoom Out
        for (float i = zoomInValue; i < _defaultFOV; i += _fovChangeAmount)
        {
            Debug.Log("FOV changing...");
            Camera.main.fieldOfView = i;
            yield return new WaitForSeconds(waitingTime);
        }
    }

    private void FollowMainCharacter()
    {
        var mainCharacterPosition = mainCharacter.position;
        transform.position = new Vector3(mainCharacterPosition.x, mainCharacterPosition.y + _cameraHeight,
            mainCharacterPosition.z - _cameraDepth);
    }
}