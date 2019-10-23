using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Extensions.IEnumerable
{
    public static class IEnumerableExtension
    {
        public static bool MembersAreEqual(IEnumerable<string> first, IEnumerable<string> second)
        {
            if (first.Count() != second.Count())
                return false;

            foreach (string str in first)
            {
                if (!second.Contains(str))
                    return false;
            }

            return true;
        }

        public static void ClearOfNulls<T>(this IEnumerable<T> items)
        {
            if (items != null)
            {
                List<T> itemsList = new List<T>(items);
                itemsList.RemoveAll(item => item == null);
                items = itemsList;
            }
        }
    }
}
