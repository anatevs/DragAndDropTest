using UnityEngine;

namespace Gameplay
{
    public class DragAndDropController : MonoBehaviour
    {
        [SerializeField]
        private InputHandler _inputHandler;

        [SerializeField]
        private LayerMask _dragLayerMask;

        [SerializeField]
        private LayerMask _standableLayerMask;

        [SerializeField]
        private GameObject _floor;

        private GrabbingObject _currentDragging;

        private readonly float _rayLength = 13f;

        private readonly float _fakeHight = 0.1f;

        private bool _isDragging;

        private Vector2 _shiftHoldPoint;

        private Vector2 _targetDropPoint;

        private void OnEnable()
        {
            _inputHandler.OnDragging += GrabObject;
            _inputHandler.OnDropped += DropObject;
        }

        private void OnDisable()
        {
            _inputHandler.OnDragging -= GrabObject;
            _inputHandler.OnDropped -= DropObject;
        }

        private void GrabObject(Vector2 pointer)
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
                _shiftHoldPoint = new Vector2(
                    collider.transform.position.x,
                    collider.transform.position.y)
                    - pointer;

                _currentDragging = collider.gameObject.GetComponent<GrabbingObject>();
                _currentDragging.MakeFalling(false);
            }
        }

        private void DropObject()
        {
            if (_isDragging)
            {
                _currentDragging.ExploreDown(_standableLayerMask, _rayLength, _floor, out _targetDropPoint);
                _targetDropPoint = new Vector2(_targetDropPoint.x, _targetDropPoint.y - _fakeHight);
                _currentDragging.StartFalling(_targetDropPoint);
            }

            _currentDragging = null;
            _isDragging = false;
        }
    }
}