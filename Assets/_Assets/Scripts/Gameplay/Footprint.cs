using System.Collections;
using System.Collections.Generic;
using MarchingBytes;
using UnityEngine;

public class Footprint : MonoBehaviour
{
    private const float MAX_ALPHA = 1f;
    private const float MIN_ALPHA = 0f;
    [SerializeField] private float _fadedOutTime;
    [SerializeField] private float _fadedAmount;

    [Space(20)] [SerializeField] private Material _printMaterial;

    [SerializeField] private List<MeshRenderer> _footPrintList = new List<MeshRenderer>();
    private EasyObjectPool EasyObjectPool => EasyObjectPool.instance;

    public void SetupFootPrint(Material material)
    {
        _printMaterial = material;

        ResetFootPrint();

        SetFootPrintMaterial();

        StartCoroutine(FadeOutFootPrint());
        Invoke(nameof(RemoveFootPrint), _fadedOutTime);
    }

    private void SetFootPrintMaterial()
    {
        foreach (Transform print in transform)
        {
            MeshRenderer meshRenderer = print.GetComponent<MeshRenderer>();

            meshRenderer.material = _printMaterial;

            if (_footPrintList.Contains(meshRenderer)) continue;
            _footPrintList.Add(meshRenderer);
        }
    }

    IEnumerator FadeOutFootPrint()
    {
        int time = Mathf.CeilToInt(MAX_ALPHA / _fadedAmount);
        float fadingTime = _fadedOutTime / time;

        for (int i = 0; i < time; i++)
        {
            foreach (var meshRenderer in _footPrintList)
            {
                Color materialColor = meshRenderer.material.color;
                materialColor.a -= _fadedAmount;
                meshRenderer.material.color = materialColor;
            }

            yield return new WaitForSeconds(fadingTime);
        }
    }

    private void ResetFootPrint()
    {
        foreach (var meshRenderer in _footPrintList)
        {
            Color materialColor = meshRenderer.material.color;
            materialColor.a = 255f;
            meshRenderer.material.color = materialColor;
        }
    }

    private void RemoveFootPrint()
    {
        EasyObjectPool.ReturnObjectToPool(gameObject);
    }
}