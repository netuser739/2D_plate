using Tool;
using UnityEngine;

namespace Game.Car
{
    internal class CarView : MonoBehaviour
    {
        private ISubscriptionProperty<float> _diff;

        public void Init(ISubscriptionProperty<float> diff)
        {
            _diff = diff;
            _diff.SubscribeOnChange(Jump);
        }

        private void OnDestroy()
        {
            _diff?.UnSubscribeOnChange(Jump);
        }

        private void Jump(float value)
        {
            Vector2 position = transform.position;
            float yPos = transform.position.y;
            position.y = value + 5 * Time.deltaTime * Time.deltaTime;
            transform.position = position;
            Debug.Log("Jump");
        }
    }
}
