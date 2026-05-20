using System;
using System.Collections.Generic;

namespace TOYOTOU.Runtime
{
    /// <summary>
    ///     動的に変化する浮動小数点数。
    /// </summary>
    public sealed class DynamicFloat
    {
        /// <summary> 計算された値。 </summary>
        public float Value => _value;

        /// <summary>
        /// 初期値を指定して DynamicFloat のインスタンスを作成します。
        /// </summary>
        /// <param name="initialValue">初期値</param>
        public DynamicFloat(float initialValue = 0f)
        {
            _baseValue = initialValue;
            _value = CalculateValue();
        }

        public static implicit operator float(DynamicFloat dynamicFloat)
        {
            return dynamicFloat._value;
        }

        /// <summary>
        ///     ベースの値を変更する。
        /// </summary>
        /// <param name="newValue"></param>
        public void SetValue(float newValue)
        {
            _baseValue = newValue;
            _value = CalculateValue();
        }

        /// <summary>
        ///     モディファイアを追加する。
        /// </summary>
        /// <param name="modifier"></param>
        public void Add(Func<float, float> modifier)
        {
            _modifires.Add(modifier);
            _value = CalculateValue();

        }

        /// <summary>
        ///     モディファイアを削除する。
        /// </summary>
        /// <param name="modifier"></param>
        public void Remove(Func<float, float> modifier)
        {
            _modifires.Remove(modifier);
            _value = CalculateValue();
        }

        private float _value;

        private float _baseValue;
        private List<Func<float, float>> _modifires;

        private float CalculateValue()
        {
            float modifiedValue = _baseValue;
            foreach (var modifier in _modifires)
            {
                modifiedValue = modifier(modifiedValue);
            }
            return modifiedValue;
        }
    }
}
