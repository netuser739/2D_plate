using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    internal class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonAds;
        [SerializeField] private Button _buttonIap;

        public void Init(UnityAction startGame, UnityAction gameSettings)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(gameSettings);
        }  
        
        public void InitAds(UnityAction rewardedAds)
        {
            _buttonAds.onClick.AddListener(rewardedAds);
        }

        public void InitIap(UnityAction iap)
        {
            _buttonIap.onClick.AddListener(iap);
        }

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonAds.onClick.RemoveAllListeners();
            _buttonIap.onClick.RemoveAllListeners();
        }
    }
}
