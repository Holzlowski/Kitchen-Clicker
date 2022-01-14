using System;
using System.Collections.Generic;

namespace Util
{
    public static class ListExtension
    {
        private static readonly Random R = new Random();

        public static T Random<T>(this List<T> list) => list[R.Next(list.Count)];
    }
}