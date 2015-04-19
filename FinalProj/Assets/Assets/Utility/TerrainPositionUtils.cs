using UnityEngine;
using System.Collections;

namespace Utils
{
    public static class TerrainPositionUtils
    {
        public static float GetGroundLevelAtPosition(Vector3 pos)
        {
            float groundLevel;
            groundLevel = Terrain.activeTerrain.SampleHeight(pos);
            return groundLevel;
        }
    }
}

