using Profile;
using Services.Ads.UnityAds;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;
        private readonly UnityAdsService _adsManager;

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, UnityAdsService adsManager)
        {
            _profilePlayer = profilePlayer;
            _adsManager = adsManager;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, GameSettings);
            _view.InitAds(PlayRewardedAds);
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;

        private void GameSettings() =>
            _profilePlayer.CurrentState.Value = GameState.Settings;

        //нужен ли этот метод? без него тоже всё работает
        private void RewardedAds()
        {
            if (_adsManager.IsInitialized) PlayRewardedAds();
            else _adsManager.Initialized.AddListener(PlayRewardedAds);
        }

        private void PlayRewardedAds() =>
            _adsManager.RewardedPlayer.Play();

    }
}
