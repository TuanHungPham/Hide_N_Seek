using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class IAPProduct
{
    public string productID;
    public eProductType productType;
    public Sprite productImage;
    public long quantity;
}