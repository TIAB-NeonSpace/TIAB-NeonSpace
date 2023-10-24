using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;

// Placing the Purchaser class in the CompleteProject namespace allows it to interact with ScoreManager, 
// one of the existing Survival Shooter scripts.
namespace CompleteProject
{
    // Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
    public class Purchaser : MonoBehaviour//, IStoreListener
    {
        private static IStoreController m_StoreController;          // The Unity Purchasing system.
        private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
        //앱스토어에 등록된 상품 아이디
        public const string ProductIDCoin_500 = "coin_500";
        public const string ProductIDCoin_2000 = "coin_2000";
        public const string ProductIDCoin_5000 = "coin_5000";
        public const string ProductIDCoin_10000 = "coin_10000";
        public const string ProductIDCoin_40000 = "coin_40000";

        void Start()
        {
            if (m_StoreController == null)
                InitializePurchasing();
        }
        //인앱상품 구매를 위한 초기화
        public void InitializePurchasing()
        {
            if (IsInitialized())
            {
                return;
            }

            //var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            //builder.AddProduct(ProductIDCoin_500, ProductType.Consumable);
            //builder.AddProduct(ProductIDCoin_2000, ProductType.Consumable);
            //builder.AddProduct(ProductIDCoin_5000, ProductType.Consumable);
            //builder.AddProduct(ProductIDCoin_10000, ProductType.Consumable);
            //builder.AddProduct(ProductIDCoin_40000, ProductType.Consumable);
            //UnityPurchasing.Initialize(this, builder);
        }
        //초기화가 되어 있는지 체크
        private bool IsInitialized()
        {
            // Only say we are initialized if both the Purchasing references are set.
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }
        //인앱상품 구매 호출 함수
        public void BuyItem(int idx)
        {

            if(Application.isEditor)
            {
                DataManager.instance.SetCoin(idx);
                LobbyController.instance.SetLabel(EnumBase.UIState.Coin, string.Format("{0:N0}", DataManager.instance.GetCoin()));
            }
            else // 이 부분은 나중에 혜영이한테 도움을 요청한다.
            {
                switch (idx)
                {
                    case 500:
                        BuyProductID(ProductIDCoin_500);
                        break;
                    case 2000:
                        BuyProductID(ProductIDCoin_2000);
                        break;
                    case 5000:
                        BuyProductID(ProductIDCoin_5000);
                        break;
                    case 10000:
                        BuyProductID(ProductIDCoin_10000);
                        break;
                    case 40000:
                        BuyProductID(ProductIDCoin_40000);
                        break;
                }
            }
        }
        
        void BuyProductID(string productId)
        {
            if (IsInitialized())
            {
                Product product = m_StoreController.products.WithID(productId);

                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    m_StoreController.InitiatePurchase(product);
                }
                else
                {
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            else
            {
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }

        public void RestorePurchases()
        {
            if (!IsInitialized())
            {
                Debug.Log("RestorePurchases FAIL. Not initialized.");
                return;
            }

            if (Application.platform == RuntimePlatform.IPhonePlayer ||
                Application.platform == RuntimePlatform.OSXPlayer)
            {
                Debug.Log("RestorePurchases started ...");

                //var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
                //apple.RestoreTransactions((result) => {
                //    Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
                //});
            }
            else
            {
                Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
            }
        }

        //public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        //{
        //    Debug.Log("OnInitialized: PASS");

        //    m_StoreController = controller;
        //    m_StoreExtensionProvider = extensions;
        //}
        
        //public void OnInitializeFailed(InitializationFailureReason error)
        //{
        //    Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
        //}
        //구매 성공 콜백함수
//        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
//        {
//            bool validPurchase = true; // Presume valid for platforms with no R.V.

//            // Unity IAP's validation logic is only included on these platforms.
//#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX
//            // Prepare the validator with the secrets we prepared in the Editor
//            // obfuscation window.
//            var validator = new CrossPlatformValidator(GooglePlayTangle.Data(),
//                AppleTangle.Data(), Application.identifier);

//            try
//            {
//                // On Google Play, result has a single product ID.
//                // On Apple stores, receipts contain multiple products.
//                var result = validator.Validate(args.purchasedProduct.receipt);
//                // For informational purposes, we list the receipt(s)
//                Debug.Log("Receipt is valid. Contents:");
//                foreach (IPurchaseReceipt productReceipt in result)
//                {
//                    Debug.Log(productReceipt.productID);
//                    Debug.Log(productReceipt.purchaseDate);
//                    Debug.Log(productReceipt.transactionID);
//                    if (String.Equals(args.purchasedProduct.definition.id, ProductIDCoin_500, StringComparison.Ordinal))
//                    {
//                        DataManager.instance.SetCoin(500);
//                    }
//                    else if (String.Equals(args.purchasedProduct.definition.id, ProductIDCoin_2000, StringComparison.Ordinal))
//                    {
//                        DataManager.instance.SetCoin(2000);
//                    }
//                    else if (String.Equals(args.purchasedProduct.definition.id, ProductIDCoin_5000, StringComparison.Ordinal))
//                    {
//                        DataManager.instance.SetCoin(5000);
//                    }
//                    else if (String.Equals(args.purchasedProduct.definition.id, ProductIDCoin_10000, StringComparison.Ordinal))
//                    {
//                        DataManager.instance.SetCoin(10000);
//                    }
//                    else if (String.Equals(args.purchasedProduct.definition.id, ProductIDCoin_40000, StringComparison.Ordinal))
//                    {
//                        DataManager.instance.SetCoin(40000);
//                    }
//                    else
//                    {
//                        Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
//                    }
//                    if (LobbyController.instance != null)
//                    {
//                        LobbyController.instance.SetLabel(EnumBase.UIState.Coin, string.Format("{0:N0}", DataManager.instance.GetCoin()));
//                        //LobbyController.instance.SetLabel(EnumBase.LobbyUIState.ActionPop, DataManager.instance.GetAction(5));
//                        //LobbyController.instance.SetSprite(EnumBase.LobbyUIState.ActionPop, "Action6");

//                        //LobbyController.instance.SetTween(EnumBase.LobbyUIState.ActionPop, true);
//                    }
//                    else
//                    {
//                        UIManager.instance.SetMoney();
//                    }
//                }
//            }
//            catch (IAPSecurityException)
//            {
//                Debug.Log("Invalid receipt, not unlocking content");
//                validPurchase = false;
//            }
//#endif
//            if (validPurchase)
//            {
//                // Unlock the appropriate content here.
//            }
//            return PurchaseProcessingResult.Complete;
//        }

//        //구매 실패 콜백함수
//        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
//        {
//            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
//        }
    }
}