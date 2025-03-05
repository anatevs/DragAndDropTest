using UnityEngine;

namespace GameCore
{
    public class ScreenPosition : MonoBehaviour
    {
        [SerializeField]
        private float _borderMoveOffset = 0.5f;

        [SerializeField]
        private float _moveStep = 3f;

        [SerializeField]
        private DragAndDropController _dragController;

        [SerializeField]
        private EnviromentComponent _enviromentComponent;

        private Camera _camera;

        private float _leftCameraBorder;

        private float _rightCameraBorder;

        private float[] _leftRightCheckPoint;

        private void Start()
        {
            _camera = Camera.main;

            (_leftCameraBorder, _rightCameraBorder) = GetCameraBorders();

            _leftRightCheckPoint = new float[2]
            {
                _leftCameraBorder + _borderMoveOffset,
                _rightCameraBorder - _borderMoveOffset
            };
        }

        private void Update()
        {
            if (_dragController.CurrentDragging != null)
            {
                if (_dragController.CurrentDragging.IsUnderLeft(_leftRightCheckPoint[0]))
                {
                    MoveLeft();
                }

                if (_dragController.CurrentDragging.IsUnderRight(_leftRightCheckPoint[1]))
                {
                    MoveRight();
                }
            }
        }

        private void MoveLeft()
        {
            var directionX = -_moveStep;

            var bordersOffset = _enviromentComponent.GetLeftBorder() - _leftCameraBorder;

            if (bordersOffset >= directionX)
            {
                directionX = bordersOffset;
            }

            _enviromentComponent.MoveOpposite(directionX);
        }

        private void MoveRight()
        {
            var directionX = _moveStep;

            var bordersOffset = _enviromentComponent.GetRightBorder() - _rightCameraBorder;

            if (bordersOffset <= directionX)
            {
                directionX = bordersOffset;
            }

            _enviromentComponent.MoveOpposite(directionX);
        }

        private (float left, float right) GetCameraBorders()
        {
            var left = _camera.transform.position.x
                - _camera.aspect * _camera.orthographicSize;

            var right = _camera.transform.position.x
                + _camera.aspect * _camera.orthographicSize;

            return (left, right);
        }
    }
}