using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

namespace TOYOTOU.Runtime
{
    [Serializable]
    public class UINavigater
    {
        public void Enable(InputActionKeyConfig key)
        {
            _key = key;
            key.MoveAction.started += MoveHandler;
            key.Skill1Action.started += SkillHandler;
            key.Skill2Action.started += SkillHandler;

            _selectArrows = new (_playerArrowLeft, _playerArrowRight,
                _skill1ArrowLeft, _skill1ArrowRight,
                _skill2ArrowLeft, _skill2ArrowRight);
            _selectArrows.AllDeactive();
            _selectArrows.Activate(_index);
        }

        public void Disable()
        {
            InputActionKeyConfig key = _key;
            key.MoveAction.started += MoveHandler;
            key.Skill1Action.started += SkillHandler;
            key.Skill2Action.started += SkillHandler;
        }

        [SerializeField] private PlayerModelSelector _playerSelector;
        [SerializeField] private UIArrow _playerArrowLeft;
        [SerializeField] private UIArrow _playerArrowRight;
        [Space]
        [SerializeField] private SkillSelector _skill1Selector;
        [SerializeField] private UIArrow _skill1ArrowLeft;
        [SerializeField] private UIArrow _skill1ArrowRight;
        [Space]
        [SerializeField] private SkillSelector _skill2Selector;
        [SerializeField] private UIArrow _skill2ArrowLeft;
        [SerializeField] private UIArrow _skill2ArrowRight;
        [Space]
        [SerializeField] private ReadyButton _ready;

        private InputActionKeyConfig _key;
        private SelectArrows _selectArrows;

        private int _index;

        private void MoveHandler(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            if (input.y != 0)
            {
                int lastIndex = _index;
                _index = Mathf.Clamp(Math.Sign(-input.y) + _index, 0, 2); // 下に行くほどインデックスが上がるため、input.yが逆転する。
                _selectArrows.Deactivate(lastIndex);
                _selectArrows.Activate(_index);
                return;
            }

            if (input.x != 0)
            {
                switch (_index)
                {
                    case 0:
                        _playerSelector.Next(input.x);
                        break;
                }

                UIArrow arrow = input.x > 0 ? _selectArrows.GetRightArrow(_index) : _selectArrows.GetLeftArrow(_index);
                arrow.Selected();
            }
        }

        private void SkillHandler(InputAction.CallbackContext context)
        {
            _ready.TriggeredReady();
        }

        private struct SelectArrows
        {
            public SelectArrows(params UIArrow[] arrows) => _arrows = arrows;

            public void Activate(int index)
            {
                UIArrow left = GetLeftArrow(index);
                UIArrow right = GetRightArrow(index);
                left.gameObject.SetActive(true);
                right.gameObject.SetActive(true);
            }

            public void Deactivate(int index)
            {
                UIArrow left = GetLeftArrow(index);
                UIArrow right = GetRightArrow(index);
                left.gameObject.SetActive(false);
                right.gameObject.SetActive(false);
            }

            public void AllDeactive()
            {
                int n = _arrows.Length / 2;
                for (int i = 0; i < n; i++)
                {
                    Deactivate(i);
                }
            }

            public UIArrow GetLeftArrow(int index) => _arrows[index * 2 + 1];
            public UIArrow GetRightArrow(int index) => _arrows[index * 2];

            private UIArrow[] _arrows;
        }
    }
}
