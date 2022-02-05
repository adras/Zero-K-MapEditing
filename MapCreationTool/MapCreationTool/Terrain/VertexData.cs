namespace MapCreationTool.Terrain
{
    class VertexData
    {
        /// <summary>
        /// Multiplier to get the index of vertex no. X in <see cref="vertices"/> array
        /// </summary>
        /// <remarks>
        /// To get the vertex information of vertex e.g. 4 in vertices array the correct index
        /// would be 4 * (3 + 3 + 2), therefore this multiplier can be used.
        /// e.g. to get the x-pos of the 4th vertex:
        /// float x = vertices[4 * idxMul]
        /// </remarks>
        public static uint IDX_MUL = 3 + 3 + 2;

        /// <summary>
        /// Holds the vertex information, position, normal, texture
        /// Format: float[3], float[3], float[2]
        /// </summary>
        /// <remarks>
        /// Since one vertex is composed of 3+3+2=8 floats, the index of the second vertex is 8
        /// </remarks>
        public float[] vertices;

        /// <summary>
        /// The index number of a vertex, if there are 40 vertices, the number goes from 0->39
        /// </summary>
        public uint[] indices;

        public int width;
        public int height;
    }
}
