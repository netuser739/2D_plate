using Tool;
using Profile;
using UnityEngine;
using Game.Car;
using Game.InputLogic;
using Game.TapeBackground;
using Features.AbilitySystem;
using Features.AbilitySystem.Abilities;
using System.Collections;
using System.Collections.Generic;

namespace Game
{
    internal class GameController : BaseController
    {
        private readonly ResourcePath _abilitiesViewPath = new ("Prefabs/Ability/AbilitiesView");
        private readonly ResourcePath _abilitiesDataSourcePath = new ("Configs/Ability/AbilityItemConfigDataSource");

        private readonly SubscriptionProperty<float> _leftMoveDiff;
        private readonly SubscriptionProperty<float> _rightMoveDiff;

        private readonly CarController _carController;
        private readonly InputGameController _inputGameController;
        private readonly AbilitiesController _abilitiesController;
        private readonly TapeBackgroundController _tapeBackgroundController;


        public GameController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _leftMoveDiff = new SubscriptionProperty<float>();
            _rightMoveDiff = new SubscriptionProperty<float>();

            _carController = CreateCarController(profilePlayer);
            _inputGameController = CreateInputGameController(profilePlayer, _leftMoveDiff, _rightMoveDiff);
            _abilitiesController = CreateAbilitiesController(placeForUi, _carController);
            _tapeBackgroundController = CreateTapeBackground(_leftMoveDiff, _rightMoveDiff);
        }


        private TapeBackgroundController CreateTapeBackground(SubscriptionProperty<float> leftMoveDiff, SubscriptionProperty<float> rightMoveDiff)
        {
            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            return tapeBackgroundController;
        }

        private InputGameController CreateInputGameController(ProfilePlayer profilePlayer,
            SubscriptionProperty<float> leftMoveDiff, SubscriptionProperty<float> rightMoveDiff)
        {
            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
            AddController(inputGameController);

            return inputGameController;
        }

        private CarController CreateCarController(ProfilePlayer curentCar)
        {
            var carController = new CarController(curentCar.CurrentCar);
            AddController(carController);

            return carController;
        }

        private AbilitiesController CreateAbilitiesController(Transform placeForUi, IAbilityActivator abilityActivator)
        {
            IAbilitiesView view = LoadAbilitiesView(placeForUi);
            IEnumerable<AbilityItemConfig> abilityItemConfigs = LoadAbilityItemConfigs();
            IAbilitiesRepository repository = CreateAbilitiesRepository(abilityItemConfigs);
            
            var abilitiesController = new AbilitiesController(view , repository, abilityItemConfigs, abilityActivator);
            AddController(abilitiesController);

            return abilitiesController;
        }

        private AbilityItemConfig[] LoadAbilityItemConfigs() =>
            ContentDataSourceLoader.LoadAbilityItemConfigs(_abilitiesDataSourcePath);

        private AbilitiesRepository CreateAbilitiesRepository(IEnumerable<AbilityItemConfig> abilityItemConfigs)
        {
            var repository = new AbilitiesRepository(abilityItemConfigs);
            AddRepository(repository);

            return repository;
        }

        private AbilitiesView LoadAbilitiesView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_abilitiesViewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<AbilitiesView>();
        }
    }
}
