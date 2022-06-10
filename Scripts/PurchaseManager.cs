using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.SceneManagement;

public class PurchaseManager : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;
    private int currentProductIndex;

    public string[] NC_PRODUCTS;

    public static event OnSuccessConsumable OnPurchaseConsumable;

    public static event OnSuccessNonConsumable OnPurchaseNonConsumable;

    public static event OnFailedPurchase PurchaseFailed;

    void Awake()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        foreach (string s in NC_PRODUCTS)
            builder.AddProduct(s, ProductType.NonConsumable);
        UnityPurchasing.Initialize(this, builder);
    }

    public void BuyNonConsumable(int index)
    {
        currentProductIndex = index;
        BuyProductID(NC_PRODUCTS[index]);
    }

    void BuyProductID(string productId)
    {
        if (m_StoreController != null && m_StoreExtensionProvider != null)
        {
            Product product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
                m_StoreController.InitiatePurchase(product);
            else
                OnPurchaseFailed(product, PurchaseFailureReason.ProductUnavailable);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (NC_PRODUCTS.Length > 0 && string.Equals(args.purchasedProduct.definition.id, NC_PRODUCTS[currentProductIndex], StringComparison.Ordinal))
            OnSuccessNC(args);
        return PurchaseProcessingResult.Complete;
    }

    public delegate void OnSuccessConsumable(PurchaseEventArgs args);

    public delegate void OnSuccessNonConsumable(PurchaseEventArgs args);

    protected virtual void OnSuccessNC(PurchaseEventArgs args)
    {
        if (OnPurchaseNonConsumable != null)
            OnPurchaseNonConsumable(args);
        if (NC_PRODUCTS[currentProductIndex] == "buy")
        {
            PlayerPrefs.SetInt("Buy", 1);
            SceneManager.LoadScene(1);
        }
    }

    public delegate void OnFailedPurchase(Product product, PurchaseFailureReason failureReason);

    protected virtual void OnFailedP(Product product, PurchaseFailureReason failureReason)
    {
        PurchaseFailed(product, failureReason);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        OnFailedP(product, failureReason);
    }
}