using UnityEngine;

namespace GameCore
{
    public class DragAndDropController : MonoBehaviour
    {
        public DraggingComponent CurrentDragging => _currentDragging;

        [SerializeField]
        private InputHandler _inputHandler;

        [SerializeField]
        private LayerMask _dragLayerMask;

        [SerializeField]
        private LayerMask _standableLayerMask;

        [SerializeField]
        private GameObject _floor;

        [SerializeField]
        private EnviromentComponent _enviromentComponent;

        [SerializeField]
        private Transform _playerTransform;

        private DraggingComponent _currentDragging;

        private readonly float _rayLength = 13f;

        private readonly float _fakeHight = 0.1f;

        private bool _isDragging;

        private Vector2 _shiftHoldPoint;

        private Vector2 _targetDropPoint;

        private float[] _leftRightCameraBorders;

        private void OnEnable()
        {
            _inputHandler.OnDragging += DragObject;
            _inputHandler.OnDropped += DropObject;
        }

        private void OnDisable()
        {
            _inputHandler.OnDragging -= DragObject;
            _inputHandler.OnDropped -= DropObject;
        }

        private void DragObject(Vector2 pointer)
        {
            if (_isDragging)
            {
                _currentDragging.transform.position = pointer + _shiftHoldPoint;

                return;
            }

            var collider = Physics2D.OverlapPoint(pointer, _dragLayerMask);

            if (collider != null)
            {
                _isDragging = true;
                _currentDragging = collider.gameObject.GetComponent<DraggingComponent>();

                _shiftHoldPoint = new Vector2(
                    _currentDragging.transform.position.x,
                    _currentDragging.transform.position.y)
                    - pointer;

                _currentDragging.MakeFalling(false);

                _currentDragging.transform.SetParent(_playerTransform);
            }
        }

        private void DropObject()
        {
            if (_isDragging)
            {
                _currentDragging.ExploreDown(_standableLayerMask, _rayLength, _floor, out _targetDropPoint);
                _targetDropPoint = new Vector2(_targetDropPoint.x, _targetDropPoint.y - _fakeHight);
                _currentDragging.StartFalling(_targetDropPoint);

                _currentDragging.transform.SetParent(_enviromentComponent.EnviromentTransform);
            }

            _currentDragging = null;
            _isDragging = false;
        }
    }
}