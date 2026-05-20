using UnityEngine;

namespace TOYOTOU.Runtime
{
    [CreateAssetMenu(fileName = nameof(DataRepository), menuName = nameof(DataRepository))]
    public class DataRepository : ScriptableObject
    {
        public PlayerStatus[] States => _stateses;
        [SerializeField] PlayerStatus[] _stateses;
    }
}
