using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(WaveManager))]
public class WaveManagerEditor : Editor
{
    private WaveManager WaveManager => (WaveManager)target;

    private ReorderableList enemyTypes;
    private void OnEnable()
    {
        enemyTypes = new ReorderableList(serializedObject, serializedObject.FindProperty(nameof(WaveManager.WaveEnemyTypes)));
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        enemyTypes.drawHeaderCallback = (rect) =>
        {
            EditorGUI.LabelField(rect, "Enemy Types");
        };

        const int propCount = 4;
        enemyTypes.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
            SerializedProperty element = enemyTypes.serializedProperty.GetArrayElementAtIndex(index);
            SerializedProperty nameProperty = element.FindPropertyRelative(nameof(WaveEnemyType.Name));
            SerializedProperty prefabProperty = element.FindPropertyRelative(nameof(WaveEnemyType.Prefab));
            SerializedProperty dangerProperty = element.FindPropertyRelative(nameof(WaveEnemyType.DangerLevel));
            SerializedProperty minWaveProperty = element.FindPropertyRelative(nameof(WaveEnemyType.MinimalWaveNumber));

            float height = propCount * EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(new Rect(rect.x, rect.y + height * 0 / propCount, rect.width, height / propCount), nameProperty);
            EditorGUI.PropertyField(new Rect(rect.x, rect.y + height * 1 / propCount, rect.width, height / propCount), prefabProperty);
            EditorGUI.PropertyField(new Rect(rect.x, rect.y + height * 2 / propCount, rect.width, height / propCount), dangerProperty);
            EditorGUI.PropertyField(new Rect(rect.x, rect.y + height * 3 / propCount, rect.width, height / propCount), minWaveProperty);
        };

        enemyTypes.onAddCallback = (list) =>
        {
            WaveManager.WaveEnemyTypes.Add(new WaveEnemyType { Prefab = null, DangerLevel = 1, Name = "Chicken", MinimalWaveNumber = 1});
        };

        enemyTypes.onCanRemoveCallback = list => list.count > 0;

        enemyTypes.elementHeight = (propCount + 0.5f) * EditorGUIUtility.singleLineHeight;

        enemyTypes.draggable = true;

        enemyTypes.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

}
