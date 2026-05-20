using UnityEngine;

namespace TOYOTOU.Runtime
{
    /// <summary>
    /// プレイヤーのステータスリストなどのデータを保持するScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = nameof(DataRepository), menuName = nameof(DataRepository))]
    public class DataRepository : ScriptableObject
    {
        /// <summary>
        /// 定義されているすべてのプレイヤーステータスを取得します
        /// </summary>
        public PlayerStatus[] States => _stateses;

        [SerializeField] PlayerStatus[] _stateses;
    }
}
