using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
        }

        public void Disable()
        {
            InputActionKeyConfig key = _key;
            key.MoveAction.started += MoveHandler;
            key.Skill1Action.started += SkillHandler;
            key.Skill2Action.started += SkillHandler;
        }

        [SerializeField] private PlayerModelSelector _playerSelector;
        [SerializeField] private SkillSelector _skill1Selector;
        [SerializeField] private SkillSelector _skill2Selector;
        [SerializeField] private ReadyButton _ready;

        private InputActionKeyConfig _key;

        private int _index;

        private void MoveHandler(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            if (input.y != 0)
            {
                _index = Math.Sign(input.y) + _index;
                return;
            }

            switch (_index)
            {
                case 0: _playerSelector.Next(input.x); break;
            }
        }

        private void SkillHandler(InputAction.CallbackContext context)
        {
            _ready.TriggeredReady();
        }
    }
}
