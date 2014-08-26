using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISWorkerProcessLister.Core
{
    public static class StringExtensions
    {
        /// <summary>
        /// Removes the closing signs from a string to get a string of lenght x.
        /// </summary>
        /// <param name="s">String to be cut.</param>
        /// <param name="x">Length of the output string.</param>
        /// <returns></returns>
        public static string MaxLengthCutRight(this string s, int x)
        {
            x = Math.Min(s.Length, x);
            return s.Substring(0, x);
        }

        /// <summary>
        /// Removes the leading signs from a string to get a string of lenght x.
        /// </summary>
        /// <param name="s">String to be cut.</param>
        /// <param name="x">Length of the output string.</param>
        /// <returns></returns>
        public static string MaxLengthCutLeft(this string s, int x)
        {
            x = Math.Min(s.Length, x);
            return s.Substring(s.Length - x, x);
        }
    }
}
