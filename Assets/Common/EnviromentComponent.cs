using UnityEngine;

namespace GameCore
{
    public class EnviromentComponent : MonoBehaviour
    {
        public Transform EnviromentTransform => _enviromentTransform;

        [SerializeField]
        private Transform _enviromentTransform;

        [SerializeField]
        private float _backgoundHalfWidth = 15f;

        public void MoveOpposite(float directionX)
        {
            _enviromentTransform.position = new Vector3(
                _enviromentTransform.position.x - directionX,
                    _enviromentTransform.position.y,
                    _enviromentTransform.position.z);
        }

        public float GetLeftBorder()
        {
            return _enviromentTransform.position.x - _backgoundHalfWidth;
        }

        public float GetRightBorder()
        {
            return _enviromentTransform.position.x + _backgoundHalfWidth;
        }
    }
}