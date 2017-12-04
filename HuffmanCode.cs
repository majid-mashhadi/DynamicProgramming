using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Data_Structure
{

    public class LetterFrequency
    {
        public char letter { get; internal set; }
        public int Frequency { get; internal set; }
    }

    public class HuffmanNode : LetterFrequency
    {
        public double weight { get; internal set; }
        internal HuffmanNode left, right;
        public HuffmanNode()
        {
            this.left = null;
            this.right = null;
        }
        public HuffmanNode(char letter, int Frequency)
            : this()
        {
            this.letter = letter;
            this.Frequency = Frequency;
        }

        public HuffmanNode(char letter, double weight)
            : this()
        {
            this.letter = letter;
            this.weight = weight;
        }
    }

    public class TwoNodes
    {
        public HuffmanNode node1, node2;
    }

    public class HuffmanCode
    {
        HuffmanNode[] Letters;
        LinkedList<HuffmanNode> q1, q2;
        Dictionary<char, string> dict;

        public HuffmanCode()
        {
            InitLetters();
            Array.Sort(this.Letters, (a, b) => { return a.weight.CompareTo(b.weight); });
            q1 = new LinkedList<HuffmanNode>();
            q2 = new LinkedList<HuffmanNode>();
            FillQueue();
            BuildTree();
            dict = new Dictionary<char, string>();
            Traverse(q2.First.Value, "");

            var r = dict.ToList();
            r.Sort((a, b) => { return a.Value.Length.CompareTo(b.Value.Length); });
            foreach(var i in r )
            {
                Console.WriteLine(string.Format("{0} --> {1}", i.Key, i.Value));
            }
            //  Console.ReadKey();
        }

        public void InitLetters()
        {

            Letters = new HuffmanNode[]
            {
                new HuffmanNode() { letter= 'A',Frequency= 77},
                new HuffmanNode() { letter= 'B',Frequency= 17} ,
                new HuffmanNode() { letter= 'C',Frequency= 32},
                new HuffmanNode() { letter= 'D',Frequency=42 },
                new HuffmanNode() { letter= 'E',Frequency=120 },
                new HuffmanNode() { letter= 'F',Frequency= 24},
                new HuffmanNode() { letter= 'G',Frequency=17 },
                new HuffmanNode() { letter= 'H',Frequency= 50},
                new HuffmanNode() { letter= 'I',Frequency=76 },
                new HuffmanNode() { letter= 'J',Frequency=4 },
                new HuffmanNode() { letter= 'K',Frequency=7 },
                new HuffmanNode() { letter= 'L',Frequency=42 },
                new HuffmanNode() { letter= 'M',Frequency=24 },
                new HuffmanNode() { letter= 'N',Frequency=67 },
                new HuffmanNode() { letter= 'O',Frequency=67 },
                new HuffmanNode() { letter= 'P',Frequency=20 },
                new HuffmanNode() { letter= 'Q',Frequency= 5},
                new HuffmanNode() { letter= 'R',Frequency= 59},
                new HuffmanNode() { letter= 'S',Frequency=67 },
                new HuffmanNode() { letter= 'T',Frequency=85 },
                new HuffmanNode() { letter= 'U',Frequency=37 },
                new HuffmanNode() { letter= 'V',Frequency=12 },
                new HuffmanNode() { letter= 'W',Frequency= 22},
                new HuffmanNode() { letter= 'X',Frequency=4 },
                new HuffmanNode() { letter= 'Y',Frequency=22 },
                new HuffmanNode() { letter= 'Z',Frequency= 2},
                new HuffmanNode() { letter= 'a',Frequency= 77},
                new HuffmanNode() { letter= 'b',Frequency= 17},
                new HuffmanNode() { letter= 'c',Frequency= 32},
                new HuffmanNode() { letter= 'd',Frequency=42 },
                new HuffmanNode() { letter= 'e',Frequency=120 },
                new HuffmanNode() { letter= 'f',Frequency= 24},
                new HuffmanNode() { letter= 'g',Frequency=17 },
                new HuffmanNode() { letter= 'h',Frequency= 50},
                new HuffmanNode() { letter= 'i',Frequency=76 },
                new HuffmanNode() { letter= 'j',Frequency=4 },
                new HuffmanNode() { letter= 'k',Frequency=7 },
                new HuffmanNode() { letter= 'l',Frequency=42 },
                new HuffmanNode() { letter= 'm',Frequency=24 },
                new HuffmanNode() { letter= 'n',Frequency=67 },
                new HuffmanNode() { letter= 'o',Frequency=67 },
                new HuffmanNode() { letter= 'p',Frequency=20 },
                new HuffmanNode() { letter= 'q',Frequency= 5},
                new HuffmanNode() { letter= 'r',Frequency= 59},
                new HuffmanNode() { letter= 's',Frequency=67 },
                new HuffmanNode() { letter= 't',Frequency=85 },
                new HuffmanNode() { letter= 'u',Frequency=37 },
                new HuffmanNode() { letter= 'v',Frequency=12 },
                new HuffmanNode() { letter= 'w',Frequency= 22},
                new HuffmanNode() { letter= 'x',Frequency=4 },
                new HuffmanNode() { letter= 'y',Frequency=22 },
                new HuffmanNode() { letter= 'z',Frequency= 2},
                new HuffmanNode() { letter= ' ',Frequency= 30},
                new HuffmanNode() { letter= '.',Frequency= 10},
                new HuffmanNode() { letter= ';',Frequency= 5}
            };

            int total = 0;
            foreach (LetterFrequency letter in Letters)
            {
                total += letter.Frequency;
            }

            double sum = 0;
            foreach (HuffmanNode letter in Letters)
            {
                letter.weight = Math.Round(letter.Frequency * 1.0 / total, 4);
                sum = sum + letter.weight;
            }
        }

        private void FillQueue()
        {
            foreach (HuffmanNode node in Letters)
            {
                q1.AddLast(node);
            }
        }

        private LinkedListNode<HuffmanNode> Peek(LinkedList<HuffmanNode> list, int pos = 0, bool dequeue = false)
        {
            if (list.Count == 0 || list.Count == 1 && pos == 1) return null;// new LinkedListNode<HuffmanNode>(new HuffmanNode('*', double.MaxValue));
            LinkedListNode<HuffmanNode> node;

            if (pos == 0)
                node = list.First;
            else
                node = list.First.Next;
            return node;

        }

        private TwoNodes GetTwoNodes()
        {
            TwoNodes item = new TwoNodes();
            LinkedListNode<HuffmanNode> node1, node2, node3, node4;
            node1 = Peek(q1, 0);
            node2 = Peek(q1, 1);
            node3 = Peek(q2, 0);
            node4 = Peek(q2, 1);
            LinkedListNode<HuffmanNode>[] arr = new LinkedListNode<HuffmanNode>[4] { node1, node2, node3, node4 };
            Array.Sort(arr, (a, b) =>
            {
                if (a == null && b == null) return 1;
                else if (a == null && b != null) return 1;
                else if (a != null && b == null) return -1;
                else return a.Value.weight.CompareTo(b.Value.weight);
            });
            int q1c = 0, q2c = 0;
            if (arr[0].List == q1)
            {
                item.node1 = q1.ElementAt(q1c);
                q1c++;
            }
            else
            {
                item.node1 = q2.ElementAt(q2c);
                q2c++;
            }
            if (arr[1].List == q1)
            {
                item.node2 = q1.ElementAt(q1c);
                q1c++;
            }
            else
            {
                item.node2 = q2.ElementAt(q2c);
                q2c++;
            }
            while (q1c > 0)
            {
                q1c--;
                q1.RemoveFirst();
            }
            while (q2c > 0)
            {
                q2c--;
                q2.RemoveFirst();
            }
            return item;
        }

        private void BuildTree()
        {
            if (q1 == null || q1.Count == 0) return;
            if (q1.Count == 1 && q2.Count == 0)
            {
                q2.AddFirst(q1.First());
                q1.RemoveFirst();
                return;
            }
            while (true)
            {
                if (q1.Count == 0 && q2.Count == 1) return;
                TwoNodes twoNodes = GetTwoNodes();
                HuffmanNode newNode = new HuffmanNode('*', Math.Round(twoNodes.node1.weight + twoNodes.node2.weight, 3));
                newNode.right = twoNodes.node1.weight < twoNodes.node2.weight ? twoNodes.node1 : twoNodes.node2;
                newNode.left = twoNodes.node1.weight < twoNodes.node2.weight ? twoNodes.node2 : twoNodes.node1;
                q2.AddLast(newNode);
            }
        }
        private void Traverse(HuffmanNode root, string prefix)
        {
            if (root == null) return;

            Traverse(root.left, prefix + '0');
            if (root.left == null && root.right == null)
            {
                dict.Add(root.letter, prefix);
            }
            Traverse(root.right, prefix + '1');
        }

        public string Compress(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if (dict.ContainsKey(c))
                {
                    sb.Append(dict[c]);
                }
            }
            return ConvertBinaryStringToByte(sb.ToString());
        }

        private string ConvertBinaryStringToByte(string str)
        {
            StringBuilder sb = new StringBuilder();
            int b = 0;
            int bitLen = 0;
            for (int i = 0; i < str.Length; i = i + 8)
            {
                b = 0;
                bitLen = 0;
                for (int j = 0; j < 8 && i + j < str.Length; j++)
                {
                    bitLen++;
                    b = b * 2;
                    if (str[i + j] == '1')
                    {
                        b += 1;
                    }
                }
                sb.Append(Convert.ToChar(b));
            }
            sb.Append(Convert.ToChar(bitLen));
            return sb.ToString();
        }

        public string Uncompress(string str)
        {
            StringBuilder sb = new StringBuilder();
            int lastZeroLenght = Convert.ToInt32(str[str.Length - 1]);
            for (int i = 0; i < str.Length - 1; i++)// (char c in str)
            {
                sb.Append(ConvertToHuffmanCode(str[i], i >= str.Length - 2, lastZeroLenght));
            }
            return GetStringFromHuffmanCode(sb);
        }

        private string ConvertToHuffmanCode(char c, bool isLast, int lastZeroLenght)
        {
            int b = 0;
            StringBuilder sb = new StringBuilder();
            b = Convert.ToInt32(c);

            while (b > 0)
            {
                if (isLast)
                    lastZeroLenght--;
                if ((b & 1) == 0)
                {
                    sb.Insert(0, "0");
                }
                else
                {
                    sb.Insert(0, "1");
                }
                b >>= 1;
            }
            if (!isLast)
            {
                while (sb.Length < 8)
                {
                    sb = sb.Insert(0, "0");
                }
            }
            else
            {
                while (lastZeroLenght != 0 && isLast)
                {
                    sb = sb.Insert(0, "0");
                    lastZeroLenght--;
                }
            }
            return sb.ToString();
        }

        private string GetString(HuffmanNode root, StringBuilder sb, int len)
        {
            if (root == null || len < 0 || len > sb.Length) return "";
            if (root.left == null && root.right == null)
            {
                sb.Remove(0, len);
                return root.letter.ToString();
            }
            if (sb[len] == '0')
                return GetString(root.left, sb, len + 1);
            if (sb[len] == '1')
                return GetString(root.right, sb, len + 1);
            return "";
        }

        private string GetStringFromHuffmanCode(StringBuilder sb)
        {
            StringBuilder result = new StringBuilder();
            while (sb.Length != 0)
            {
                result.Append(GetString(q2.First.Value, sb, 0));
            }
            return result.ToString();
        }
    }
}
