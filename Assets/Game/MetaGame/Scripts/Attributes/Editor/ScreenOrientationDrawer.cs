using Game.Config;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Game.Attribute
{
    [CustomPropertyDrawer(typeof(ScreenOrientationAttribute))]
    public class ScreenOrientationDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var list = AssetDatabase.LoadAssetAtPath<ScreenOrientationConfig>("Assets/Game/MetaGame/ConfigAssets/StringDropDownConfigs/ScreenOrientationConfig.asset")
                .data.Select(x => x.screenOrient).ToList();

            if (list == null || list.Count == 0)
            {
                EditorGUI.PropertyField(position, property, true);
            }
            else
            {
                int index = Mathf.Max(0, list.IndexOf(property.stringValue));
                property.stringValue = list[EditorGUI.Popup(position, label.ToString(), index, list.ToArray())];
            }
        }
    }
}