using Profile;
using Services.Ads.UnityAds;
using Services.IAP;
using Tool;
using Tool.Analytics;
using Tool.Analytics.UnityAnalytics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;
        private readonly AnalyticsManager _analytics;
        private readonly UnityAdsService _adsManager;
        private readonly IAPService _iapService;

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, AnalyticsManager analytics,
            UnityAdsService adsManager, IAPService iapService)
        {
            _profilePlayer = profilePlayer;
            _adsManager = adsManager;
            _iapService = iapService;
            _analytics = analytics;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, GameSettings);
            _view.InitAds(PlayRewardedAds);
            _view.InitIap(DoIap);
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

        private void DoIap()
        {
            _iapService.Buy("prod_2");
            _analytics.TransactionProd2Event();
            Debug.Log("Buy prod_2");
        }
    }
}
