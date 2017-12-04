using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    class BalancedBrackets
    {
        char[,] brackets = new char[3, 2] {{ '{', '}' }, { '(', ')' }, { '[', ']' }};
        public bool IsBalanced(string s)
        {
            Stack<char> stack = new Stack<char>();
            foreach (char c in s.ToCharArray())
            {
                if (isOpenBracket(c))
                {
                    stack.Push(c);
                }
                else if (stack.Count == 0 || !isMatch(stack.Pop(), c))
                    return false;
            }
            return stack.Count == 0;
        }

        public bool isOpenBracket(char c)
        {
            for (int i = 0; i < brackets.GetLength(0); i++)
            {
                if (c == brackets[i, 0])
                {
                    return true;
                }
            }
            return false;
        }

        public bool isMatch(char top, char c)
        {
            for (int i = 0; i < brackets.GetLength(0); i++)
            {
                if (top == brackets[i, 0])
                {
                    return brackets[i,1] == c;
                }
            }
            return false;
        }

    }
}
