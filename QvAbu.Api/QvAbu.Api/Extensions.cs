using System;

namespace QvAbu.Api
{
    public static class Extensions
    {
        public static string CombineRevision(this Guid id, int revision)
        {
            return id.ToString() + revision;
        }
    }
}
