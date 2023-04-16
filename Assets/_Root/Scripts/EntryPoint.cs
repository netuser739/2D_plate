using Profile;
using UnityEngine;
using UnityEngine.Serialization;

internal class EntryPoint : MonoBehaviour
{
    [SerializeField] private StartGameConfig _gameConfigs;
    [SerializeField] private Transform _placeForUi;

    private MainController _mainController;

    private void Start()
    {
        var profilePlayer = new ProfilePlayer(_gameConfigs.SpeedCar, _gameConfigs.JumpHeight, _gameConfigs.InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}
