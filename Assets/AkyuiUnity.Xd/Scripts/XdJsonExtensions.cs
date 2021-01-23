using System.Linq;
using UnityEngine;
using XdParser;
using XdParser.Internal;

namespace AkyuiUnity.Xd
{
    public static class XdJsonExtensions
    {
        public static string GetSimpleName(this XdObjectJson xdObjectJson)
        {
            if (xdObjectJson.Name == null) return string.Empty;
            return xdObjectJson.Name.Split('@')[0];
        }

        public static string[] GetParameters(this XdObjectJson xdObjectJson)
        {
            if (xdObjectJson.Name == null) return new string[] { };

            var e = xdObjectJson.Name.Split('@');
            if (e.Length <= 1) return new string[] { };

            return e[1].Split(',').Select(x => x.ToLowerInvariant().Trim()).ToArray();
        }

        public static float GetRepeatGridSpacing(this XdObjectJson xdObjectJson, string scrollingType)
        {
            float spacing;

            if (scrollingType == "vertical")
            {
                spacing = xdObjectJson.Meta?.Ux?.RepeatGrid?.PaddingY ?? 0f;
            }
            else
            {
                spacing = xdObjectJson.Meta?.Ux?.RepeatGrid?.PaddingX ?? 0f;
            }

            return spacing;
        }

        public static void RemoveConstraint(this XdObjectJson xdObjectJson)
        {
            if (xdObjectJson?.Meta?.Ux != null)
            {
                xdObjectJson.Meta.Ux.ConstraintRight = false;
                xdObjectJson.Meta.Ux.ConstraintLeft = false;
                xdObjectJson.Meta.Ux.ConstraintTop = false;
                xdObjectJson.Meta.Ux.ConstraintBottom = false;
            }
        }

        public static Color GetFillUnityColor(this XdObjectJson xdObjectJson)
        {
            var colorJson = xdObjectJson.GetFillColor();
            Color color = new Color32((byte) colorJson.R, (byte) colorJson.G, (byte) colorJson.B, 255);
            color.a = xdObjectJson.Style?.Opacity ?? 1f;
            return color;
        }
    }
}