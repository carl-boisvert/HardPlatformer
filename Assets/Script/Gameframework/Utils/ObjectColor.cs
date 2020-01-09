using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework.Utils
{
    public class ObjectColor
    {

        public enum color
        {
            BLUE,
            PINK,
            PURPLE,
            RED
        }

        public static Color getColor(ObjectColor.color color)
        {
            switch (color)
            {
                case ObjectColor.color.BLUE:
                    return new Color(0, 0, 1, 1);
                case ObjectColor.color.RED:
                    return new Color(1, 0, 0, 1);
                case ObjectColor.color.PINK:
                    return new Color(224f / 255f, 71f / 255f, 218f / 255f, 1);
                case ObjectColor.color.PURPLE:
                    return new Color(127f / 255f, 7f / 255f, 161f / 255f, 1);
                default:
                    throw new UnityException("Color doesn't exist");
            }
        }

        public static Color addColor(ObjectColor.color color1, ObjectColor.color color2)
        {
            var colorObject1 = getColor(color1);
            var colorObject2 = getColor(color2);
            return colorObject1 + colorObject2;
        }
    }
}