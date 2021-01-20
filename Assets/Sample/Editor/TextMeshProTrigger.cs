﻿using System;
using AkyuiUnity.Editor.ScriptableObject;
using AkyuiUnity.Generator;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace AkyuiUnity.CommonTrigger
{
    [CreateAssetMenu(menuName = "Akyui/Triggers/TextMeshPro", fileName = nameof(TextMeshProTrigger))]
    public class TextMeshProTrigger : AkyuiImportTrigger
    {
        [SerializeField] private string fontFilePath = "Assets/Fonts/{name} SDF";

        public override Component SetOrCreateComponentValue(GameObject gameObject, TargetComponentGetter componentGetter, IComponent component, IAssetLoader assetLoader)
        {
            if (component is TextComponent textComponent)
            {
                var text = componentGetter.GetComponent<TextMeshProUGUI>();
                text.enableWordWrapping = false;
                text.overflowMode = TextOverflowModes.Overflow;

                if (textComponent.Text != null) text.text = textComponent.Text;
                if (textComponent.Size != null) text.fontSize = Mathf.RoundToInt(textComponent.Size.Value);
                if (textComponent.Color != null) text.color = textComponent.Color.Value;
                if (textComponent.Align != null)
                {
                    switch (textComponent.Align.Value)
                    {
                        case TextComponent.TextAlign.UpperLeft:
                            text.alignment = TextAlignmentOptions.TopLeft;
                            break;
                        case TextComponent.TextAlign.UpperCenter:
                            text.alignment = TextAlignmentOptions.Top;
                            break;
                        case TextComponent.TextAlign.UpperRight:
                            text.alignment = TextAlignmentOptions.TopRight;
                            break;
                        case TextComponent.TextAlign.MiddleLeft:
                            text.alignment = TextAlignmentOptions.MidlineLeft;
                            break;
                        case TextComponent.TextAlign.MiddleCenter:
                            text.alignment = TextAlignmentOptions.Midline;
                            break;
                        case TextComponent.TextAlign.MiddleRight:
                            text.alignment = TextAlignmentOptions.MidlineRight;
                            break;
                        case TextComponent.TextAlign.LowerLeft:
                            text.alignment = TextAlignmentOptions.BottomLeft;
                            break;
                        case TextComponent.TextAlign.LowerCenter:
                            text.alignment = TextAlignmentOptions.Bottom;
                            break;
                        case TextComponent.TextAlign.LowerRight:
                            text.alignment = TextAlignmentOptions.BottomRight;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                if (textComponent.Font != null)
                {
                    var fontPath = fontFilePath.Replace("{name}", textComponent.Font) + ".asset";
                    text.font = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(fontPath);
                    if (text.font == null)
                    {
                        Debug.LogWarning($"TextMeshPro Font {fontPath} is not found");
                    }
                }

                return text;
            }

            return null;
        }
    }
}