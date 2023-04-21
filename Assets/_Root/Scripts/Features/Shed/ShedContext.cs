using Features.Inventory;
using Features.Shed.Upgrade;
using Profile;
using System;
using Tool;
using UnityEngine;

namespace Features.Shed
{
    internal class ShedContext : BaseController
    {
        private readonly ResourcePath _viewPath = new("Prefabs/Shed/ShedView");
        private readonly ResourcePath _dataSourcePath = new("Configs/Shed/UpgradeItemConfigDataSource");

        public ShedContext(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            if(placeForUi == null) 
                throw new ArgumentNullException(nameof(placeForUi));

            if (profilePlayer == null)
                throw new ArgumentNullException(nameof(profilePlayer));

            CreateController(profilePlayer, placeForUi);
        }

        private ShedController CreateController(ProfilePlayer profilePlayer, Transform placeForUi)
        {
            InventoryContext inventoryContext = CreateInventoryContext(placeForUi, profilePlayer);
            UpgradeHandlersRepository shedRepository = CreateRepository();
            ShedView shedView = LoadView(placeForUi);

            return new ShedController
                (
                    shedView,
                    profilePlayer,
                    shedRepository
                );
        }
        private InventoryContext CreateInventoryContext(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            var context = new InventoryContext(placeForUi, profilePlayer);
            AddContext(context);

            return context;
        }

        private UpgradeHandlersRepository CreateRepository()
        {
            UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(_dataSourcePath);
            var repository = new UpgradeHandlersRepository(upgradeConfigs);
            AddRepository(repository);

            return repository;
        }

        private ShedView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<ShedView>();
        }
    }
}
