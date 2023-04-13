using Tool;
using UnityEngine;

namespace Game.InputLogic
{
    internal abstract class BaseInputView : MonoBehaviour
    {
        protected float Speed;
        protected float Jump;

        private SubscriptionProperty<float> _leftMove;
        private SubscriptionProperty<float> _rightMove;
        private SubscriptionProperty<float> _upMove;


        public virtual void Init(
            SubscriptionProperty<float> leftMove,
            SubscriptionProperty<float> rightMove,
            SubscriptionProperty<float> upMove,
            float speed, float jump)
        {
            _leftMove = leftMove;
            _rightMove = rightMove;
            _upMove = upMove;
            Speed = speed;
            Jump = jump;
        }


        protected void OnLeftMove(float value) =>
            _leftMove.Value = value;

        protected void OnRightMove(float value) =>
            _rightMove.Value = value;

        protected void OnUpMove(float value) =>
            _upMove.Value = value;
    }
}
