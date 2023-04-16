using Profile;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(StartGameConfig), menuName = "Configs / " + nameof(StartGameConfig), order = 0)]
internal class StartGameConfig : ScriptableObject
{
    [field: SerializeField] public float SpeedCar { get; set; }
    [field: SerializeField] public float JumpHeight { get; set; }
    [field: SerializeField] public GameState InitialState { get; set; }
}

