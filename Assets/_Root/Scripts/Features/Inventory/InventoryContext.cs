using Tool;
using Profile;
using UnityEngine;
using Features.Inventory.Items;
using Object = UnityEngine.Object;

namespace Features.Inventory
{
    internal class InventoryContext : BaseContext
    {
        private readonly ResourcePath _viewPath = new("Prefabs/Inventory/InventoryView");
        private readonly ResourcePath _dataSourcePath = new("Configs/Inventory/ItemConfigDataSource");

        public InventoryContext(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            var view = LoadInventoryView(placeForUi);
            var repository = CreateInventoryRepository();
            var controller = new InventoryController(view, repository, profilePlayer.Inventory);
            AddController(controller);
        }

        private InventoryView LoadInventoryView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi);
            AddGameObject(objectView);

            return objectView.GetComponent<InventoryView>();
        }

        private ItemsRepository CreateInventoryRepository()
        {
            ItemConfig[] itemConfigs = ContentDataSourceLoader.LoadItemConfigs(_dataSourcePath);
            var repository = new ItemsRepository(itemConfigs);

            return repository;
        }
    }
}
