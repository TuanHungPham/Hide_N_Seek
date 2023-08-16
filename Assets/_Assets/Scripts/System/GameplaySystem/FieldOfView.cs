using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TigerForge;

public class FieldOfView : MonoBehaviour
{
    public float checkingDelayTime;
    public float viewRadius;

    [Space(20)] public Transform thisObject;
    public Vector3 normal;
    public LayerMask targetlayerMask;
    public LayerMask obstaclelayerMask;
    public Vector3 from => thisObject.forward;
    [Range(0, 360)] public float viewAngle;

    [SerializeField] private List<Transform> _spottedObjectList = new List<Transform>();

    [SerializeField] private List<Collider> _objectInViewList;

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
            // Test();
            yield return new WaitForSeconds(checkingDelayTime);
        }
    }

    private void Test()
    {
        Collider[] targetInViewList = Physics.OverlapSphere(thisObject.position, viewRadius);
        // Debug.Log($"count: {targetInViewList.Length}");
    }

    private void FindTargetInViewRange()
    {
        _spottedObjectList.Clear();
        _objectInViewList = new List<Collider>();
        Collider[] targetInViewList = Physics.OverlapSphere(thisObject.position, viewRadius, targetlayerMask);
        Debug.Log($"count: {targetInViewList.Length}");
        _objectInViewList = targetInViewList.ToList();

        for (int i = 0; i < targetInViewList.Length; i++)
        {
            Transform target = targetInViewList[i].transform;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}