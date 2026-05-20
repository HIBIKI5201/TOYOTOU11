using UnityEngine;
using UnityEngine.InputSystem;

namespace TOYOTOU.Runtime
{
    /// <summary>
    /// Input System の各アクション（移動、スキル）を管理し、簡単に入力値を取得できるようにする ScriptableObject。
    /// </summary>
    [CreateAssetMenu(fileName = nameof(InputActionKeyConfig), menuName = nameof(InputActionKeyConfig))]
    public class InputActionKeyConfig : ScriptableObject
    {
        /// <summary> 移動アクション </summary>
        public InputAction MoveAction => _moveAction;

        /// <summary> スキル1発動アクション </summary>
        public InputAction Skill1Action => _skill1Action;

        /// <summary> スキル2発動アクション </summary>
        public InputAction Skill2Action => _skill2Action;

        /// <summary>
        /// 全てのアクションを有効化します。
        /// </summary>
        public void Enable()
        {
            _moveAction.Enable();
            _skill1Action.Enable();
            _skill2Action.Enable();
        }

        /// <summary>
        /// 全てのアクションを無効化します。
        /// </summary>
        public void Disable()
        {
            _moveAction.Disable();
            _skill1Action.Disable();
            _skill2Action.Disable();
        }

        /// <summary>
        /// 現在の移動入力値を Vector2 で取得します。
        /// </summary>
        /// <returns>入力された移動方向</returns>
        public Vector2 GetMoveValue() => _moveAction.ReadValue<Vector2>();

        /// <summary>
        /// スキル1がトリガーされたかどうかを返します。
        /// </summary>
        /// <returns>トリガーされていればtrue</returns>
        public bool IsTriggerdSkill1() => _skill1Action.triggered;

        /// <summary>
        /// スキル2がトリガーされたかどうかを返します。
        /// </summary>
        /// <returns>トリガーされていればtrue</returns>
        public bool IsTriggerdSkill2() => _skill2Action.triggered;

        [SerializeField] private InputAction _moveAction;
        [SerializeField] private InputAction _skill1Action;
        [SerializeField] private InputAction _skill2Action;

        private void OnValidate()
        {
            Debug.Assert(_moveAction.expectedControlType == "Vector2", "MoveActionはVector2である必要があります。", this);
        }
    }
}
