using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DraggingComponent : MonoBehaviour
    {
        [SerializeField]
        private Transform _bottomPoint;

        private readonly Vector2 _downDirection = Vector2.down;
        private readonly Vector2 _upDirection = Vector2.up;
        private readonly Vector2 _zeroVector = Vector2.zero;

        private Rigidbody2D _rigidbody;

        private bool _isFalling;

        private Vector2 _targetPoint;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            MakeFalling(false);
        }

        private void Update()
        {
            if (_isFalling)
            {
                if (_bottomPoint.position.y <= _targetPoint.y)
                {
                    MakeFalling(false);
                }
            }
        }

        public bool ExploreDown(LayerMask standLayer, float rayLength, GameObject floor, out Vector2 standPoint)
        {
            standPoint = _zeroVector;

            var colliders = Physics2D.RaycastAll(_bottomPoint.position, _downDirection, rayLength);

            foreach (var collider in colliders)
            {
                if (collider.transform.gameObject != this && ((standLayer & (1 << collider.transform.gameObject.layer)) != 0))
                {
                    var pointColliders = Physics2D.OverlapPointAll(collider.point);

                    if (pointColliders.Length > 1)
                    {
                        if (collider.point.y != _bottomPoint.position.y)
                        {
                            continue;
                        }
                        else if (collider.point.y == _bottomPoint.position.y
                            && pointColliders.Length != 2
                            && collider.transform.gameObject == floor)
                        {
                            continue;
                        }
                    }

                    standPoint = collider.point;
                    return true;
                }
            }

            var reverseColliders = Physics2D.RaycastAll(
                new Vector2(_bottomPoint.position.x, _bottomPoint.position.y - rayLength),
                _upDirection, rayLength);

            if (reverseColliders.Length > 2)
            {
                standPoint = reverseColliders[1].point;
                return true;
            }

            return false;
        }

        public void StartFalling(Vector2 targetPoint)
        {
            MakeFalling(true);
            _targetPoint = targetPoint;
        }

        public void MakeFalling(bool isFalling)
        {
            _isFalling = isFalling;
            _rigidbody.isKinematic = !isFalling;
            _rigidbody.velocity = _zeroVector;
        }
    }
}