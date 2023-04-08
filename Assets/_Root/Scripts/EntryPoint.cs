using System;
using Profile;
using Services.Ads.UnityAds;
using Services.IAP;
using Tool.Analytics;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.Start;

    [SerializeField] private Transform _placeForUi;
    [SerializeField] private UnityAdsService _adsService;
    [SerializeField] private AnalyticsManager _analyticsManager;
    [SerializeField] private IAPService _iapService;

    private MainController _mainController;

    private void Start()
    {
        var profilePlayer = new ProfilePlayer(SpeedCar, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer, _analyticsManager, _adsService);

        _analyticsManager.SendMainMenuOpenedEvent();

        if (_adsService.IsInitialized) OnAdsInitialized();
        else _adsService.Initialized.AddListener(OnAdsInitialized);

        if (_iapService.IsInitialized) OnIapInitialized();
        else _iapService.Initialized.AddListener(OnIapInitialized);
    }

    private void OnDestroy()
    {
        _adsService.Initialized.RemoveListener(OnAdsInitialized);
        _iapService.Initialized.RemoveListener(OnIapInitialized);
        _mainController.Dispose();
    }

    private void OnAdsInitialized() => _adsService.InterstitialPlayer.Play();
    private void OnIapInitialized() => _iapService.Buy("prod_1");

}
