using System;
using System.Collections;
using Infrastructure.GameEvents;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace Editor
{
    [CustomPropertyDrawer(typeof(GameEvent<>), true)]
    internal sealed class GameEventPropertyDrawer : PropertyDrawer
    {
        private SerializedProperty _serializedProperty;
        private Type _genericType;

        public override bool CanCacheInspectorGUI(SerializedProperty property) => false;

        private void Initialize(SerializedProperty property)
        {
            if (_serializedProperty == property)
                return;

            _serializedProperty = property;
            _genericType = GetGenericArgument();
            Assert.IsNotNull(_genericType, "Unable to find generic argument");
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            Initialize(property);
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Initialize(property);
            EditorGUI.LabelField(position, "Event - " + _genericType.Name);
        }

        private Type GetGenericArgument()
        {
            Type type = fieldInfo.FieldType;

            while (type != null)
            {
                if (type.IsArray)
                    type = type.GetElementType();

                if (type == null)
                    return null;

                if (type.IsGenericType)
                {
                    if (typeof(IEnumerable).IsAssignableFrom(type))
                    {
                        type = type.GetGenericArguments()[0];
                    }
                    else if (type.GetGenericTypeDefinition() == typeof(GameEvent<>))
                    {
                        return type.GetGenericArguments()[0];
                    }
                    else
                    {
                        type = type.BaseType;
                    }
                }
                else
                {
                    type = type.BaseType;
                }
            }

            return null;
        }

    }
}
