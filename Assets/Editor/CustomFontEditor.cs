using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

public class CustomFontEditor : EditorWindow
{
    [Serializable]
    public class CharacterData
    {
        public string character = "";
        public float x = 0;
        public float y = 0;
        public float width = 0;
        public float height = 0;
    }

    private Texture2D atlasTexture;
    private Font targetFont;
    private int characterCount = 1;
    private List<CharacterData> characterDataList = new List<CharacterData>();
    private Vector2 scrollPosition;
    private float characterSpacing = 1f;
    private int atlasWidth = 512;
    private int atlasHeight = 512;

    [MenuItem("Tools/Custom Font Editor")]
    public static void ShowWindow()
    {
        GetWindow<CustomFontEditor>("自定义字体编辑器");
    }

    private void OnGUI()
    {
        GUILayout.Label("自定义字体配置工具", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // 基础设置
        atlasTexture = (Texture2D)EditorGUILayout.ObjectField("图集图片", atlasTexture, typeof(Texture2D), false);
        targetFont = (Font)EditorGUILayout.ObjectField("目标字体文件", targetFont, typeof(Font), false);

        EditorGUILayout.Space();

        // 图集尺寸设置
        GUILayout.Label("图集尺寸设置", EditorStyles.boldLabel);
        atlasWidth = EditorGUILayout.IntField("图集宽度", atlasWidth);
        atlasHeight = EditorGUILayout.IntField("图集高度", atlasHeight);
        characterSpacing = EditorGUILayout.FloatField("字符间距", characterSpacing);

        EditorGUILayout.Space();

        // 字符数量设置
        GUILayout.Label("字符配置", EditorStyles.boldLabel);
        int newCount = EditorGUILayout.IntField("字符数量", characterCount);
        if (newCount != characterCount)
        {
            characterCount = Mathf.Max(1, newCount);
            UpdateCharacterList();
        }

        EditorGUILayout.Space();

        // 字符数据列表
        GUILayout.Label("字符数据列表", EditorStyles.boldLabel);
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(300));

        for (int i = 0; i < characterDataList.Count; i++)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField($"字符 {i + 1}", EditorStyles.boldLabel);

            CharacterData data = characterDataList[i];

            // 字符输入
            string charInput = EditorGUILayout.TextField("字符", data.character);
            if (charInput.Length > 0)
            {
                data.character = charInput[0].ToString();
            }

            // 位置和尺寸输入
            data.x = EditorGUILayout.FloatField("位置 X", data.x);
            data.y = EditorGUILayout.FloatField("位置 Y", data.y);
            data.width = EditorGUILayout.FloatField("宽度", data.width);
            data.height = EditorGUILayout.FloatField("高度", data.height);

            // 预览信息
            if (atlasTexture != null)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("UV预览:");
                // 使用正确的UV计算
                float uvx = data.x / atlasWidth;
                float uvy = data.y / atlasHeight; // 直接使用输入的Y坐标
                float uvWidth = data.width / atlasWidth;
                float uvHeight = data.height / atlasHeight;
                GUILayout.Label($"({uvx:F3}, {uvy:F3}, {uvWidth:F3}, {uvHeight:F3})");
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space();

        // 操作按钮
        if (GUILayout.Button("设置字体属性", GUILayout.Height(30)))
        {
            SetFontProperties();
        }

        if (GUILayout.Button("重置所有", GUILayout.Height(25)))
        {
            ResetAll();
        }

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("填写说明：\n" +
                               "1. 字符: 输入单个字符，如 A, B, 0, 1 等\n" +
                               "2. 位置 X/Y: 在 Sprite Editor 中切图的 X 和 Y 坐标\n" +
                               "3. 宽度/高度: 在 Sprite Editor 中切图的宽度和高度\n" +
                               "4. 图集宽度/高度: 整个图集图片的尺寸", MessageType.Info);
    }

    private void UpdateCharacterList()
    {
        // 调整列表大小以匹配字符数量
        while (characterDataList.Count < characterCount)
        {
            characterDataList.Add(new CharacterData());
        }

        while (characterDataList.Count > characterCount)
        {
            characterDataList.RemoveAt(characterDataList.Count - 1);
        }
    }

    private void SetFontProperties()
    {
        if (targetFont == null)
        {
            EditorUtility.DisplayDialog("错误", "请先选择目标字体文件", "确定");
            return;
        }

        if (atlasTexture == null)
        {
            EditorUtility.DisplayDialog("错误", "请先选择图集图片", "确定");
            return;
        }

        if (characterDataList.Count == 0)
        {
            EditorUtility.DisplayDialog("错误", "请至少添加一个字符", "确定");
            return;
        }

        // 检查所有字符数据是否有效
        for (int i = 0; i < characterDataList.Count; i++)
        {
            var data = characterDataList[i];
            if (string.IsNullOrEmpty(data.character) || data.character.Length != 1)
            {
                EditorUtility.DisplayDialog("错误", $"第 {i + 1} 个字符数据无效，请确保输入单个字符", "确定");
                return;
            }

            if (data.width <= 0 || data.height <= 0)
            {
                EditorUtility.DisplayDialog("错误", $"第 {i + 1} 个字符的宽度或高度无效", "确定");
                return;
            }
        }

        // 创建字符信息列表
        List<CharacterInfo> characterInfos = new List<CharacterInfo>();

        foreach (CharacterData data in characterDataList)
        {
            char character = data.character[0];
            CharacterInfo charInfo = CreateCharacterInfo(character, data);
            characterInfos.Add(charInfo);
        }

        // 直接设置字体属性
        targetFont.characterInfo = characterInfos.ToArray();

        // 确保字体有材质
        if (targetFont.material == null)
        {
            // 创建简单材质
            Material fontMaterial = new Material(Shader.Find("GUI/Text Shader"));
            fontMaterial.mainTexture = atlasTexture;
            targetFont.material = fontMaterial;

            // 保存材质
            string fontPath = AssetDatabase.GetAssetPath(targetFont);
            string materialPath = fontPath.Replace(".font", ".mat");
            AssetDatabase.CreateAsset(fontMaterial, materialPath);
        }
        else
        {
            // 更新现有材质的纹理
            targetFont.material.mainTexture = atlasTexture;
        }

        // 标记字体为已修改并保存
        EditorUtility.SetDirty(targetFont);
        AssetDatabase.SaveAssets();

        EditorUtility.DisplayDialog("成功", "字体属性设置完成！", "确定");
    }

    private CharacterInfo CreateCharacterInfo(char character, CharacterData data)
    {
        CharacterInfo charInfo = new CharacterInfo();

        // 设置字符索引
        charInfo.index = (int)character;

        // 修正UV坐标计算 - 直接使用输入的坐标
        float uvx = data.x / atlasWidth;
        float uvy = data.y / atlasHeight; // 直接使用输入的Y坐标，不再进行翻转
        float uvWidth = data.width / atlasWidth;
        float uvHeight = data.height / atlasHeight;

        // 设置UV坐标
        charInfo.uvBottomLeft = new Vector2(uvx, uvy);
        charInfo.uvBottomRight = new Vector2(uvx + uvWidth, uvy);
        charInfo.uvTopLeft = new Vector2(uvx, uvy + uvHeight);
        charInfo.uvTopRight = new Vector2(uvx + uvWidth, uvy + uvHeight);

        // 设置顶点位置 - 将基线放在字符中心
        int width = Mathf.RoundToInt(data.width);
        int height = Mathf.RoundToInt(data.height);

        charInfo.minX = 0;
        charInfo.maxX = width;

        // 基线在字符中心，所以上下各占一半高度
        charInfo.minY = -height / 2;
        charInfo.maxY = height / 2;

        // 设置前进量（字符宽度 + 间距）
        charInfo.advance = Mathf.RoundToInt(data.width + characterSpacing);

        return charInfo;
    }

    private void ResetAll()
    {
        characterDataList.Clear();
        characterCount = 1;
        UpdateCharacterList();
        atlasTexture = null;
        targetFont = null;
        atlasWidth = 512;
        atlasHeight = 512;
        characterSpacing = 1f;
    }
}