using System.Collections.Generic;
using UnityEngine;

public abstract class ShopDataManager<T> : MonoBehaviour
{
    public abstract void SetItemOwnedStateData(int id, bool isOwned);
    public abstract void SetCurrentUsingItem(int id);
    public abstract bool FindItemInData(int id, out T item);
    public abstract List<T> GetDataList();
    public abstract T GetCurrentUsingItem();
}