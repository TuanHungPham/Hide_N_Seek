using System;
using System.Collections;
using UnityEngine;

public class FootPrintSystem : MonoBehaviour
{
    [SerializeField] private float _footprintTimer;
    [SerializeField] private float _footprintTime;
    [SerializeField] private float _footPrintCreatingDelay;
    [SerializeField] private bool _feetIsPainted;

    [Space(20)] [SerializeField] private Material _paintMaterial;
    [SerializeField] private GameObject _footPrint;

    public void SetFootPrint(bool set, Material material)
    {
        _feetIsPainted = set;
        _footprintTimer = _footprintTime;
        _paintMaterial = material;
    }

    private void Start()
    {
        StartCoroutine(CreateFootPrint());
    }

    private void Update()
    {
        CheckFootprintTime();
    }

    private void CheckFootprintTime()
    {
        if (!_feetIsPainted) return;

        if (_footprintTimer <= 0)
        {
            _feetIsPainted = false;
            return;
        }

        _footprintTimer -= Time.deltaTime;
    }

    IEnumerator CreateFootPrint()
    {
        while (true)
        {
            if (_feetIsPainted)
            {
                var playerTransform = transform;
                GameObject footPrint = Instantiate(_footPrint);
                footPrint.transform.position = playerTransform.position;
                footPrint.transform.rotation = playerTransform.rotation;

                Footprint footprintScript = footPrint.GetComponent<Footprint>();
                footprintScript.SetupFootPrint(_paintMaterial);

                yield return new WaitForSeconds(_footPrintCreatingDelay);
            }
            else
            {
                yield return new WaitForSeconds(_footPrintCreatingDelay);
            }
        }
    }

    public bool FeetIsPainted()
    {
        return _feetIsPainted;
    }
}