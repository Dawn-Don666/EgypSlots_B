#if IAP
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using zeta_framework;

/// <summary>
/// 内购接口实现类
/// </summary>
public class IAPFinnish : IStoreListener
{
    public static IAPFinnish Instance;

    private string googlePublicKey;
    private string appleRootCert;

    private IStoreController controller;
    private IExtensionProvider extensions;

    private Action<bool> purchaseCallback;

    public IAPFinnish()
    {
        Instance = this;

        googlePublicKey = PestGeorgiaAmid.Instance.GooglePublicKey;
        appleRootCert = PestGeorgiaAmid.Instance.AppleRootCert;

        // 使用内购之前，先初始化Unity Gaming Services
        AlterationUGS.Instance.Initialize();

        var module = StandardPurchasingModule.Instance();
        var builder = ConfigurationBuilder.Instance(module);

        List<Zone> shopList = ZoneAmid.Instance.GetShopList(false);
        foreach(Zone shop in shopList)
        {
            if (string.IsNullOrEmpty(shop.gp_pid) && string.IsNullOrEmpty(shop.ios_pid))
            {
                continue;
            }
            IDs productIds = new IDs();
            if (!string.IsNullOrEmpty(shop.gp_pid))
            {
                productIds.Add(shop.gp_pid, new string[] { GooglePlay.Name });
            }
            if (!string.IsNullOrEmpty(shop.ios_pid))
            {
                productIds.Add(shop.ios_pid, new string[] { MacAppStore.Name });
            }
            builder.AddProduct(shop.id, ProductType.Consumable, productIds);
        }
        if (builder.products.Count > 0)
        {
            UnityPurchasing.Initialize(this, builder);
        }
    }

    /// <summary>
    /// 初始化成功
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="extensions"></param>
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
        this.extensions = extensions;
        Debug.Log("IAP 初始化成功");
    }

    /// <summary>
    /// 初始化失败回调
    /// 当 Unity IAP 遇到不可恢复的初始化错误时调用。
    /// 请注意，如果网络不可用，不会调用此方法；unityIap将后台重试，直到可用为止
    /// </summary>
    /// <param name="error"></param>
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError("IAP 初始化失败1: " + error);
    }

    /// <summary>
    /// 初始化失败回调
    /// 当 Unity IAP 遇到不可恢复的初始化错误时调用。
    /// 请注意，如果网络不可用，不会调用此方法；unityIap将后台重试，直到可用为止
    /// </summary>
    /// <param name="error"></param>
    /// <param name="message"></param>
    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.LogError("IAP 初始化失败2: " + error);
        Debug.LogError(message);
    }

    /// <summary>
    ///  支付失败
    /// </summary>
    /// <param name="product"></param>
    /// <param name="failureReason"></param>
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.LogError("购买失败！商品id:" + product.definition.id + ". FailureReason:" + failureReason);
        purchaseCallback?.Invoke(false);
    }

    /// <summary>
    /// 支付成功
    /// </summary>
    /// <param name="purchaseEvent"></param>
    /// <returns></returns>
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        bool validPurchase = true;  // 本地校验

#if UNITY_EDITOR
        // 编辑器中不做验证，直接校验通过
        validPurchase = true;

        //Unity IAP 的验证逻辑仅包含在这些平台上。
#elif UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX
        //用我们在 Editor 混淆处理窗口中准备的密钥来
        // 准备验证器。
        // GooglePlay的公钥和AppStore证书，带混淆
        //var validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.bundleIdentifier);
        // 没有混淆 
        CrossPlatformValidator validator = new CrossPlatformValidator(Convert.FromBase64String(googlePublicKey), Convert.FromBase64String(appleRootCert), Application.identifier);
        try
        {
            //在 Google Play 上，结果中仅有一个商品 ID。
            //在 Apple 商店中，收据包含多个商品。
            var result = validator.Validate(e.purchasedProduct.receipt);
            //为便于参考，我们将收据列出
            Debug.Log("Receipt is valid. Contents:");
            foreach (IPurchaseReceipt productReceipt in result)
            {
                Debug.Log(productReceipt.productID);
                Debug.Log(productReceipt.purchaseDate);
                Debug.Log(productReceipt.transactionID);
                string productId = productReceipt.productID;
                if (!ZoneAmid.Instance.shopDict.ContainsKey(productId))
                {
                    Debug.LogError("收据中的商品[" + productId + "]，不在配置中。");
                    validPurchase = false;
                    break;
                }
            }
        }
        catch (IAPSecurityException)
        {
            Debug.LogError("Invalid receipt, not unlocking content");
            validPurchase = false;
        }
#endif

        if (validPurchase)
        {
            // 在此处解锁相应的内容。
            string shopId = e.purchasedProduct.definition.id;
            ZoneAmid.Instance.DistributeRewards(ZoneAmid.Instance.GetShopById(shopId));
            purchaseCallback?.Invoke(true);
        }
        else
        {
            MedalFinnish.GetInstance().ShowToast("Purchase failed");
            purchaseCallback?.Invoke(false);
        }

        return PurchaseProcessingResult.Complete;
    }

    /// <summary>
    /// 发起购买
    /// </summary>
    /// <param name="payId"></param>
    public void StartPurchase(Zone shop, Action<bool> callBack)
    {
        if (shop.purchase_type != 1)
        {
            Debug.LogError("商品不是现金购买");
            callBack?.Invoke(false);
            return;
        }
        if (shop.price == 0)
        {
            // 如果是免费产品，不走支付流程
            callBack?.Invoke(true);
            return;
        }

        if (controller == null)
        {
            Debug.LogError("IAP初始化失败");
            callBack?.Invoke(false);
            return;
        }

        // 获取IAP产品
        string productId = shop.id;
        Product product = controller.products.WithID(productId);

        // 检查产品是否可用
        if (product != null && product.availableToPurchase)
        {
            // 发起购买
            purchaseCallback = callBack;
            controller.InitiatePurchase(productId);
        }
        else
        {
            Debug.LogError("Product not available for purchase: " + productId);
            callBack?.Invoke(false);
        }
    }
}

#endif