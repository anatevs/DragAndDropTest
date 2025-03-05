using UnityEngine;
using DG.Tweening;

namespace GameCore
{
    public class EnviromentComponent : MonoBehaviour
    {
        public Transform EnviromentTransform => _enviromentTransform;

        public bool IsMoving => _isMoving;

        [SerializeField]
        private Transform _enviromentTransform;

        [SerializeField]
        private float _backgoundHalfWidth = 15f;

        [SerializeField]
        private float _moveDuration = 0.5f;

        private bool _isMoving = false;

        public void MoveOpposite(float directionX)
        {
            if (!_isMoving)
            {
                _isMoving = true;

                _enviromentTransform.DOMoveX(_enviromentTransform.position.x - directionX, _moveDuration)
                    .OnComplete(MakeOnMoveEnd);
            }
        }

        public float GetLeftBorder()
        {
            return _enviromentTransform.position.x - _backgoundHalfWidth;
        }

        public float GetRightBorder()
        {
            return _enviromentTransform.position.x + _backgoundHalfWidth;
        }

        private void MakeOnMoveEnd()
        {
            _isMoving = false;
        }
    }
}