using Features.Shed.Upgrade;

namespace Game.Car
{
    internal class CarModel : IUpgradable
    {
        private readonly float _defaultSpeed;
        private readonly float _defaultJump;

        public float Speed { get; set; }
        public float Jump { get; set; }


        public CarModel(float speed, float jump)
        {
            _defaultSpeed = speed;
            _defaultJump = jump;
            Speed = speed;
            Jump = jump;
        }

        public void Restore()
        {
            Speed = _defaultSpeed;
            Jump = _defaultJump;
        }
    }
}
