using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Memezuki.GameplayAbilitySystem.Attribute;

/// <summary>
/// 屬性系統編輯器
/// </summary>
public class AttributeSystemEditor : Editor
{
    public void OnEnable()
    {
        VisualElement rootElement = new VisualElement();

        VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Packages/com.memezuki.gameplayabilitysystem/Editor/Attribute/AttributeSystem/AttributeSystemEditor.uxml");
        visualTree.CloneTree(rootElement);

        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Packages/com.memezuki.gameplayabilitysystem/Editor/Attribute/AttributeSystem/AttributeSystemEditor.uss");
        rootElement.styleSheets.Add(styleSheet);
    }

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement container = new VisualElement();

        container.Add(new PropertyField(this.serializedObject.FindProperty("_attributes"), "Attributes"));
        container.Add(new PropertyField(this.serializedObject.FindProperty("_attributeEventHandlers"), "Events"));

        return container;
    }
}