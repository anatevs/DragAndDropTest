using System;
using UnityEngine;

namespace GameCore
{
    public sealed class MovingSectionsController : MonoBehaviour,
        ILeftScreenAlignment
    {
        public Vector2 InitPos
        {
            get => _initPos;
            set => _initPos = value;
        }

        //[SerializeField]
        //private MapSection[] _sections;

        [SerializeField]
        private float _speed = 10f;

        [SerializeField]
        bool _isMoving;

        private float _unshiftedPosX;

        private Vector2 _initPos;

        private Action[] _changePlaceActions;

        private Vector3[] _startPositions;

        public void SetToInitPosX()
        {
            transform.position = _initPos;
        }

        public void AlignXToScreen(float leftCameraBorder)
        {
            //var initShiftX = _sections[0].LeftBorderShift + leftCameraBorder;

            //_initPos = new Vector2(
            //    _unshiftedPosX + initShiftX,
            //    transform.position.y);

            //transform.position = _initPos;

            //for (int i = 0; i < _sections.Length; i++)
            //{
            //    _sections[i].LeftCameraBorder = leftCameraBorder;

            //    _startPositions[i] = _sections[i].transform.position;
            //}
        }

        public void SetIsMoving(bool isMoving)
        {
            _isMoving = isMoving;
        }

        public void SetSectionsToInitX()
        {
            //for (int i = 0; i < _sections.Length; i++)
            //{
            //    _sections[i].transform.position = _startPositions[i];

            //    _sections[i].InvokeOnInitPosSet(i);
            //}
        }

        private void Awake()
        {
            //_changePlaceActions = new Action[_sections.Length];

            //_startPositions = new Vector3[_sections.Length];

            //_unshiftedPosX = transform.position.x;
        }

        private void OnEnable()
        {
            //for (int i = 0; i < _sections.Length; i++)
            //{
            //    var otherIndex = (i + 1) % _sections.Length;

            //    _changePlaceActions[i] = ChangePlaceAction(i, otherIndex);

            //    _sections[i].OnBorderAchieved += _changePlaceActions[i];
            //}
        }
        private void OnDisable()
        {
            //for (int i = 0; i < _sections.Length; i++)
            //{
            //    _sections[i].OnBorderAchieved -= _changePlaceActions[i];
            //}
        }

        private void Update()
        {
            if (_isMoving)
            {
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
            }
        }

        //private Action ChangePlaceAction(int index, int otherIndex)
        //{
            //return () =>
            //{
            //    _sections[index].PlaceLeftBorderToX(
            //        _sections[otherIndex].GetRightBorderX());
            //};
        //}
    }
}