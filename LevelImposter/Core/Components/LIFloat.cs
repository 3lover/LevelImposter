﻿using System;
using UnityEngine;
using Il2CppInterop.Runtime.Attributes;

namespace LevelImposter.Core
{
    /// <summary>
    /// Object that oscillates up and down
    /// </summary>
    public class LIFloat : MonoBehaviour
    {
        public LIFloat(IntPtr intPtr) : base(intPtr)
        {
        }

        private float _height = 0.2f;
        private float _speed = 2.0f;
        private float _t = 0;
        private float _yOffset = 0;

        [HideFromIl2Cpp]
        public void Init(LIElement elem)
        {
            _height = elem.properties.floatingHeight ?? _height;
            _speed = elem.properties.floatingSpeed ?? _speed;
            _yOffset = elem.y;
        }

        public void Update()
        {
            _t += Time.deltaTime;
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                (Mathf.Sin(_t * _speed) + 1) * _height / 2 + _yOffset,
                transform.localPosition.z
            );
        }
    }
}
