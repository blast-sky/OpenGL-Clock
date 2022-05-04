using OpenTK.Mathematics;

namespace CGClock
{
    interface IDrawable
    {
        abstract void draw(Matrix4 view, Matrix4 projection, Vector3 viewPos);
    }
}
