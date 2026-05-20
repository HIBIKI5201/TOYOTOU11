using UnityEngine;
using UnityEngine.InputSystem;

namespace TOYOTOU.Runtime
{
    [CreateAssetMenu(fileName = nameof(InputActionKeyConfig), menuName = nameof(InputActionKeyConfig))]
    public class InputActionKeyConfig : ScriptableObject
    {
        public InputAction MoveAction => _moveAction;
        public InputAction Skill1Action => _skill1Action;
        public InputAction Skill2Action => _skill2Action;

        public Vector2 GetMoveValue() => _moveAction.ReadValue<Vector2>();
        public bool IsTriggerdSkill1() => _skill1Action.triggered;
        public bool IsTriggerdSkill2() => _skill2Action.triggered;

        [SerializeField] private InputAction _moveAction;
        [SerializeField] private InputAction _skill1Action;
        [SerializeField] private InputAction _skill2Action;

        private void OnValidate()
        {
            Debug.Assert(_moveAction.expectedControlType == "Vector2", "MoveActionはVector2である必要があります。", this);
        }

        private void OnEnable()
        {
            _moveAction.Enable();
            _skill1Action.Enable();
            _skill2Action.Enable();
        }

        private void OnDisable()
        {
            _moveAction.Disable();
            _skill1Action.Disable();
            _skill2Action.Disable();
        }
    }
}
