using Profile;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(StartGameConfig), menuName = "Configs / " + nameof(StartGameConfig), order = 0)]
internal class StartGameConfig : ScriptableObject
{
    [field: SerializeField] public GameState InitialState { get; private set; }
    [field: SerializeField] public float SpeedCar { get; private set; }
    [field: SerializeField] public float JumpHeight { get; private set; }
}

