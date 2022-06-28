using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Christ.Core
{
    public static class WoofSugar 
    {
        public static void IfNotNull<T>(this T component, Action<T> action) where T : Component
        {
            if (component != null)
                action?.Invoke(component);
        }

        public static void IfNull<T>(this T component, Action<T> action) where T : Component
        {
            if (component == null)
                action?.Invoke(component);
        }

        public static void Enable(this Component component)
        {
            Enable(component.gameObject);
        }

        public static void Enable(this GameObject gameObject)
        {
            gameObject.SetActive(true);
        }

        public static void Disable(this Component component)
        {
            Disable(component.gameObject);
        }

        public static void Disable(this GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
    }
}

