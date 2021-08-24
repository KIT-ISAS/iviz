namespace Iviz.Octree
{
    public struct Float4
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public Float4(float x, float y, float z, float w) => (this.x, this.y, this.z, this.w) = (x, y, z, w);

        public void Deconstruct(out float x, out float y, out float z, out float w) =>
            (x, y, z, w) = (this.x, this.y, this.z, this.w);
    }
}