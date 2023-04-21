using Tool;
using Profile;
using System;
using System.Collections.Generic;
using UnityEngine;
using Features.Inventory;
using Features.Shed.Upgrade;
using JetBrains.Annotations;
using Features.Inventory.Items;

namespace Features.Shed
{
    internal interface IShedController
    {
    }

    internal class ShedController : BaseController, IShedController
    {
        private readonly ResourcePath _shedViewPath = new ("Prefabs/Shed/ShedView");
        private readonly ResourcePath _shedDataSourcePath = new ("Configs/Shed/UpgradeItemConfigDataSource");

        private readonly ResourcePath _inventoryViewPath = new("Prefabs/Inventory/InventoryView");
        private readonly ResourcePath _inventoryDataSourcePath = new("Configs/Inventory/ItemConfigDataSource");

        private readonly ShedView _view;
        private readonly ProfilePlayer _profilePlayer;
        private readonly InventoryController _inventoryController;
        private readonly UpgradeHandlersRepository _upgradeHandlersRepository;


        public ShedController(
            [NotNull] Transform placeForUi,
            [NotNull] ProfilePlayer profilePlayer)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            _profilePlayer
                = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));

            _upgradeHandlersRepository = CreateRepository();
            _inventoryController = CreateInventoryController(placeForUi);
            _view = LoadShedView(placeForUi);

            _view.Init(Apply, Back);
        }


        private UpgradeHandlersRepository CreateRepository()
        {
            UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(_shedDataSourcePath);
            var repository = new UpgradeHandlersRepository(upgradeConfigs);
            AddRepository(repository);

            return repository;
        }

        private InventoryController CreateInventoryController(Transform placeForUi)
        {
            IInventoryView view = LoadInventoryView(placeForUi);
            IItemsRepository repository = CreateInventoryRepository();
            IInventoryModel inventoryModel = _profilePlayer.Inventory;
            var inventoryController = new InventoryController(view, repository, inventoryModel);
            AddController(inventoryController);

            return inventoryController;
        }

        private ItemsRepository CreateInventoryRepository()
        {
            ItemConfig[] itemConfigs = ContentDataSourceLoader.LoadItemConfigs(_inventoryDataSourcePath);
            var repository = new ItemsRepository(itemConfigs);
            AddRepository(repository);

            return repository;
        }

        private InventoryView LoadInventoryView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_inventoryViewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi);
            AddGameObject(objectView);

            return objectView.GetComponent<InventoryView>();
        }

        private ShedView LoadShedView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_shedViewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<ShedView>();
        }


        private void Apply()
        {
            _profilePlayer.CurrentCar.Restore();

            UpgradeWithEquippedItems(
                _profilePlayer.CurrentCar,
                _profilePlayer.Inventory.EquippedItems,
                _upgradeHandlersRepository.Items);

            _profilePlayer.CurrentState.Value = GameState.Start;
            Log($"Apply. Current Speed: {_profilePlayer.CurrentCar.Speed}");
            Log($"Apply. Current Jump: {_profilePlayer.CurrentCar.Jump}");
        }

        private void Back()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
            Log($"Back. Current Speed: {_profilePlayer.CurrentCar.Speed}");
            Log($"Back. Current Jump: {_profilePlayer.CurrentCar.Jump}");
        }


        private void UpgradeWithEquippedItems(
            IUpgradable upgradable,
            IReadOnlyList<string> equippedItems,
            IReadOnlyDictionary<string, IUpgradeHandler> upgradeHandlers)
        {
            foreach (string itemId in equippedItems)
                if (upgradeHandlers.TryGetValue(itemId, out IUpgradeHandler handler))
                    handler.Upgrade(upgradable);
        }

        private void Log(string message) =>
            Debug.Log($"[{GetType().Name}] {message}");
    }
}
