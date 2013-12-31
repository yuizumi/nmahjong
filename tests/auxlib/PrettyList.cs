using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NMahjong.Testing
{
    internal class PrettyList<T> : ReadOnlyCollection<T>
    {
        internal PrettyList(IList<T> items) : base(items)
        {
        }

        public override string ToString()
        {
            // TODO(yuizumi): Pretty-print the items.
            var sb = new StringBuilder("{");
            for (int i = 0; i < Count; i++) {
                ((i == 0) ? sb : sb.Append(", ")).Append(Items[i]);
            }
            return sb.Append("}").ToString();
        }
    }
}
