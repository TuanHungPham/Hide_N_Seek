using System;
using System.Collections.Generic;
using UnityEngine;

public class IAPPanelUI : MonoBehaviour
{
    [SerializeField] private List<IAPProductUI> _iapProductList;

    private void Start()
    {
        InitializeIAPProduct();
    }

    private void InitializeIAPProduct()
    {
        throw new NotImplementedException();
    }
}