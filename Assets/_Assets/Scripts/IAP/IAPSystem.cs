using System;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class IAPSystem : MonoBehaviour, IDetailedStoreListener
{
    private static IAPSystem instance;
    public static IAPSystem Instance => instance;

    [SerializeField] private IAPProductTemplate _iapProductTemplate;
    private IStoreController controller;
    private IExtensionProvider extensions;

    public string environment = "production";

    private void Awake()
    {
        instance = this;
    }

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

        foreach (var iapProduct in _iapProductTemplate.IAPProductTemplateList)
        {
            builder.AddProduct(iapProduct.productID, ProductType.Consumable);
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
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
    }

    public void OnPurchaseFailed(Product i, PurchaseFailureDescription p)
    {
    }

    public IAPProductTemplate GetIAPProductTemplate()
    {
        return _iapProductTemplate;
    }
}