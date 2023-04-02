using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class SettingsMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBack;

        public void InitMenu(UnityAction mainMenu)
        {
            _buttonBack.onClick.AddListener(mainMenu);
        }

        public void OnDestroy()
        {
            _buttonBack.onClick.RemoveAllListeners();
        }
    }
}