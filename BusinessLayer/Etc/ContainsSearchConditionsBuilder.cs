using BusinessLayer.Extensions;
using System.Linq;
using System.Text.RegularExpressions;

namespace BusinessLayer.Etc
{
    internal class ContainsSearchConditionsBuilder
    {
        private bool isStrictMode = true;

        private int strictDepth;

        private bool isPrefixMode;

        private int prefixDepth;

        private string[] varyables;

        private string resultQuery;

        public ContainsSearchConditionsBuilder(string text)
        {
            string[] temp = Regex.Replace(text, @"[^\w\s]", "").Trim().Replace("  ", " ").Split(' ');
            varyables = new string[temp.Length];
            for (int i = 0; i < varyables.Length; i++)
            {
                varyables[i] = temp[i].Wrap('\"');
            }
        }

        public ContainsSearchConditionsBuilder AllowNonStrict(int depth)
        {
            isStrictMode = false;
            strictDepth = depth;
            return this;
        }

        public ContainsSearchConditionsBuilder AllowPrefix(int depth)
        {
            isPrefixMode = true;
            prefixDepth = depth;
            return this;
        }

        private void MakePrefix()
        {
            for (int i = 0; i < varyables.Length; i++)
            {
                string current = varyables[i];
                if (current.Length <= prefixDepth)
                {
                    varyables[i] = current.Insert(current.Length - 1, "*");
                    continue;
                }
                int prefIndex = current.Length - current.Length / prefixDepth;
                varyables[i] = current.Substring(0, prefIndex) + "*\"";
            }
        }

        private void MakeStrictQuery()
        {
            resultQuery = string.Join(" and ", varyables);
        }

        private void MakeNonStrictQuery()
        {
            if (strictDepth >= varyables.Length)
            {
                MakeStrictQuery();
                return;
            }
            string strictArea = string.Join(" and ", varyables.Take(strictDepth));
            if (strictDepth > 1)
                strictArea = strictArea.WrapRounded();
            string[] nonStrictVars = varyables.Skip(strictDepth).ToArray();
            string nonStrictArea = (strictArea + " and " + (string.Join(" or ", nonStrictVars)).WrapRounded()).WrapRounded();
            resultQuery = string.Join(" or ", strictArea, nonStrictArea);
        }

        public string Build()
        {
            if (isPrefixMode)
                MakePrefix();
            if (isStrictMode)
                MakeStrictQuery();
            else
                MakeNonStrictQuery();
            return resultQuery;
        }
    }
}