using Tool;
using UnityEngine;
using Features.AbilitySystem;

namespace Game.Car
{
    internal class CarController : BaseController, IAbilityActivator
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Car");
        private readonly CarView _view;

        private readonly SubscriptionProperty<float> _diff;
        private readonly ISubscriptionProperty<float> _upMove;

        public GameObject ViewGameObject => _view.gameObject;


        public CarController(SubscriptionProperty<float> upMove)
        {
            _view = LoadView();
            _diff = new SubscriptionProperty<float>();
            _upMove = upMove;
            _view.Init(_diff);
            _upMove.SubscribeOnChange(Jump);
        }

        private CarView LoadView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            return objectView.GetComponent<CarView>();
        }

        protected override void OnDispose() =>
            _upMove.UnSubscribeOnChange(Jump);

        private void Jump(float value) =>
            _diff.Value = value;
    }
}
