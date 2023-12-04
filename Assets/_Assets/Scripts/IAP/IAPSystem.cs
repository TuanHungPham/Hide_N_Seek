using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class IAPSystem : PermanentMonoSingleton<IAPSystem>, IDetailedStoreListener
{
    // private static IAPSystem instance;
    // public static IAPSystem Instance => instance;

    [SerializeField] private IAPProductTemplate _iapProductTemplate;
    private string environment = "production";
    private IStoreController controller;
    private IExtensionProvider extensions;
    private Dictionary<string, IAPProduct> _iapProductDic;
    private GameplayManager GameplayManager => GameplayManager.Instance;

    // private void Awake()
    // {
    //     DontDestroyOnLoad(this);
    //     instance = this;
    // }

    async void Start()
    {
        await InitializeUnityGamingServices();
        InitializeIAPSystem();
    }

    private async Task InitializeUnityGamingServices()
    {
        try
        {
            var options = new InitializationOptions()
                .SetEnvironmentName(environment);

            await UnityServices.InitializeAsync(options);
        }
        catch (Exception exception)
        {
            Debug.Log($"(UNITY GAMING SERVICES) Error: {exception.Message}");
        }
    }

    private void InitializeIAPSystem()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        _iapProductDic = new Dictionary<string, IAPProduct>();

        foreach (var iapProduct in _iapProductTemplate.IAPProductTemplateList)
        {
            builder.AddProduct(iapProduct.productID, ProductType.Consumable);
            _iapProductDic.Add(iapProduct.productID, iapProduct);
        }

        UnityPurchasing.Initialize(this, builder);
    }

    public void PurchaseProduct(string productId)
    {
        controller.InitiatePurchase(productId);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
        this.extensions = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log($"(IAP) Initializing Error: {error.ToString()}");
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log($"(IAP) Initializing Error: {error.ToString()} --- {message}");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        var productID = e.purchasedProduct.definition.id;
        IAPProduct iapProduct = _iapProductDic[productID];

        GiveItemToUser(iapProduct);

        return PurchaseProcessingResult.Complete;
    }

    private void GiveItemToUser(IAPProduct iapProduct)
    {
        eProductType productType = iapProduct.productType;
        long quantity = iapProduct.quantity;

        switch (productType)
        {
            case eProductType.COIN:
                GameplayManager.AddCoin(quantity);
                break;
            case eProductType.TICKET:
                GameplayManager.AddTicket(quantity);
                break;
        }
    }

    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        Debug.Log($"(IAP) Purchase Failed: {i.metadata.localizedTitle} --- {p.ToString()}");
    }

    public void OnPurchaseFailed(Product i, PurchaseFailureDescription p)
    {
        Debug.Log($"(IAP) Purchase Failed: {i.metadata.localizedTitle} --- {p.message}");
    }

    public Product GetProduct(string productID)
    {
        return controller.products.WithID(productID);
    }

    public IAPProductTemplate GetIAPProductTemplate()
    {
        return _iapProductTemplate;
    }
}