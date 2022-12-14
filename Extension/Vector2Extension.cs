using UnityEngine;

namespace Wayway.Engine
{
    public static class Vector2Extension
    {
        public static float Random(this Vector2 minMax)
        {
            //Warning: if Max is smaller than Min returns Max
            return minMax.x > minMax.y ? minMax.y : UnityEngine.Random.Range(minMax.x, minMax.y);
        }
    }
}
