using Tool;
using UnityEngine;
using Features.AbilitySystem;
using Features.AbilitySystem.Abilities;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Features.AbilitySystem
{
    internal class AbilityContext : BaseContext
    {
        private readonly ResourcePath _abilitiesViewPath = new("Prefabs/Ability/AbilitiesView");
        private readonly ResourcePath _abilitiesDataSourcePath = new("Configs/Ability/AbilityItemConfigDataSource");

        public AbilityContext(Transform placeForUi, IAbilityActivator abilityActivator)
        {
            IAbilitiesView view = LoadView(placeForUi);
            IEnumerable<AbilityItemConfig> abilityItemConfigs = LoadItemConfigs();
            IAbilitiesRepository repository = CreateRepository(abilityItemConfigs);

            AbilitiesController controller = new AbilitiesController(view, repository, abilityItemConfigs, abilityActivator);
            AddController(controller);
        }

        private AbilityItemConfig[] LoadItemConfigs() =>
            ContentDataSourceLoader.LoadAbilityItemConfigs(_abilitiesDataSourcePath);

        private AbilitiesRepository CreateRepository(IEnumerable<AbilityItemConfig> abilityItemConfigs)
        {
            var repository = new AbilitiesRepository(abilityItemConfigs);
            AddRepository(repository);

            return repository;
        }

        private AbilitiesView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_abilitiesViewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<AbilitiesView>();
        }
    }
}
