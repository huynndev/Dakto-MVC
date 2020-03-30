namespace Sora.Common.CommonObjects
{
    public static class EmptyArray<T>
    {
        public static T[] Instance { get; } = new T[0];
    }
}
