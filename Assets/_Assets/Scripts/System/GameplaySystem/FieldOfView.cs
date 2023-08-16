using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TigerForge;

public class FieldOfView : MonoBehaviour
{
    #region public

    public float checkingDelayTime;
    public float viewRadius;
    [Range(0, 360)] public float viewAngle;

    [Space(20)] public Transform thisObject;
    public Vector3 normal;
    public LayerMask targetlayerMask;
    public LayerMask obstaclelayerMask;
    public Vector3 from => thisObject.forward;
    public Color _color;

    #endregion

    #region private

    [Space(20)] [SerializeField] private List<Transform> _spottedObjectList = new List<Transform>();

    [SerializeField] private List<Transform> _objectInViewList = new List<Transform>();

    #endregion


    public Vector3 DirFromAngle(float degreesAngle, bool isAngleGlobal)
    {
        if (!isAngleGlobal)
        {
            degreesAngle += thisObject.eulerAngles.y;
        }

        Vector3 direction =
            new Vector3(Mathf.Sin(degreesAngle * Mathf.Deg2Rad), 0, Mathf.Cos(degreesAngle * Mathf.Deg2Rad));

        return direction;
    }

    private void Start()
    {
        StartCoroutine(HandleCheckingTarget());
    }

    IEnumerator HandleCheckingTarget()
    {
        while (true)
        {
            FindTargetInViewRange();
            yield return new WaitForSeconds(checkingDelayTime);
        }
    }

    private void FindTargetInViewRange()
    {
        _spottedObjectList.Clear();
        _objectInViewList.Clear();

        foreach (var player in GameplaySystem.Instance.GetHiderList())
        {
            float distanceToObject = Vector3.Distance(thisObject.position, player.position);
            if (distanceToObject >= viewRadius) continue;

            _objectInViewList.Add(player);
        }

        for (int i = 0; i < _objectInViewList.Count; i++)
        {
            Transform target = _objectInViewList[i].transform;
            CheckTargetIsSpotted(target);
        }
    }

    private void CheckTargetIsSpotted(Transform target)
    {
        if (target == thisObject) return;

        Vector3 directionToTarget = (target.position - thisObject.position).normalized;

        float angleToTarget = Vector3.Angle(directionToTarget, thisObject.forward);

        if (angleToTarget < viewAngle / 2)
        {
            float distanceToTarget = Vector3.Distance(thisObject.position, target.position);

            bool hit = Physics.Raycast(thisObject.position, directionToTarget, distanceToTarget,
                obstaclelayerMask);

            if (!hit)
            {
                Debug.Log($"Spotted {target.name}...");
                _spottedObjectList.Add(target);
                EmitSpottedObjectEvent();
            }
        }
    }

    public List<Transform> GetSpottedObjectList()
    {
        return _spottedObjectList;
    }

    private void EmitSpottedObjectEvent()
    {
        EventManager.EmitEvent(EventID.SPOTTED_OBJECT);
    }
}