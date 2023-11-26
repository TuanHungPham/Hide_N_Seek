using System.Collections.Generic;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

public class TestFunctionSystem : MonoBehaviour
{
    [Header("Setting for Seeker's Vision")] [SerializeField]
    private Text _rangeValue;

    [SerializeField] private Text _heightValue;
    [SerializeField] private Text _intensityValue;
    [SerializeField] private Slider _rangeSlider;
    [SerializeField] private Slider _heightSlider;
    [SerializeField] private Slider _intensitySlider;
    [SerializeField] private eVisionType _visionType;
    [SerializeField] private Button _frontBtn;
    [SerializeField] private Button _circleBtn;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _rangeValue.text = _rangeSlider.value.ToString();
        _heightValue.text = _heightSlider.value.ToString();
        _intensityValue.text = _intensitySlider.value.ToString();
    }

    public void OnResetQuest()
    {
        IngameDataManager.Instance.GetQuestDataManager().RandomDailyQuest();
        EventManager.EmitEvent(EventID.QUEST_RESETTING);
    }

    public void OnEndLevel()
    {
        GameplaySystem.Instance.SetGameplayTimer(0);
    }

    public void SetRangeValue()
    {
        var value = _rangeSlider.value;
        _rangeValue.text = value.ToString();

        List<Transform> seekerList = GameplaySystem.Instance.GetSeekerList();

        foreach (var seeker in seekerList)
        {
            Controller controller = seeker.GetComponent<Controller>();

            controller.SetLightRange(_visionType, value);
        }
    }

    public void SetHeightValue()
    {
        var value = _heightSlider.value;
        _heightValue.text = value.ToString();

        List<Transform> seekerList = GameplaySystem.Instance.GetSeekerList();

        foreach (var seeker in seekerList)
        {
            Controller controller = seeker.GetComponent<Controller>();

            controller.SetLightHeight(_visionType, value);
        }
    }

    public void SetIntensityValue()
    {
        var value = _intensitySlider.value;
        _intensityValue.text = value.ToString();

        List<Transform> seekerList = GameplaySystem.Instance.GetSeekerList();

        foreach (var seeker in seekerList)
        {
            Controller controller = seeker.GetComponent<Controller>();

            controller.SetLightIntensity(_visionType, value);
        }
    }

    public void SetFrontVisionType()
    {
        _visionType = eVisionType.FRONT_VISION;

        _circleBtn.image.color = Color.white;
        _frontBtn.image.color = Color.green;
    }

    public void SetCircleVisionType()
    {
        _visionType = eVisionType.CIRCLE_VISION;

        _frontBtn.image.color = Color.white;
        _circleBtn.image.color = Color.green;
    }
}