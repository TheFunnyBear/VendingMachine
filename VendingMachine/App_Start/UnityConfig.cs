using System;

using Unity;
using Unity.Lifetime;
using VendingMachine.BuisnesLogic;
using VendingMachine.Helpers;
using VendingMachine.Repository;
using VendingMachine.Service;

namespace VendingMachine
{
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        public static IUnityContainer Container => container.Value;
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IPictureHelper, PictureHelper>();

            container.RegisterType<IPictureService, PictureService>();
            container.RegisterType<IPictureRepository, PictureRepository>();
            container.RegisterType<ICashBoxService, CashBoxService>();
            container.RegisterType<ICashBoxRepository, CashBoxRepository>();
            container.RegisterType<ILoginKeysService, LoginKeysService>();
            container.RegisterType<ILoginKeysRepository, LoginKeysRepository>();
            container.RegisterType<IStoreService, StoreService>();
            container.RegisterType<IStoreRepository, StoreRepository>();
            container.RegisterType<ITransactionsService, TransactionsService>();
            container.RegisterType<ITransactionsRepository, TransactionsRepository>();

            container.RegisterType<ICashBoxManager, CashBoxManager>(new ContainerControlledLifetimeManager());
            container.RegisterType<IStoreManager, StoreManager>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPurchaseManager, PurchaseManager>(new ContainerControlledLifetimeManager());
            container.RegisterType<IBasketSlot, BasketSlot>(new ContainerControlledLifetimeManager());
            container.RegisterType<IChangeSlot, ChangeSlot>(new ContainerControlledLifetimeManager());
        }
    }
}