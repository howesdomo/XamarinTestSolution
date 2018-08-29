using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Client.View.Games.CRW
{
    /// <summary>
    /// 题库
    /// </summary>
    public class TiKu
    {
        private static List<CRW_Question> List { get; set; }

        public static int ListCount { get; private set; }

        public static List<CRW_Question> InitList()
        {
            if (List == null)
            {
                List = new List<CRW_Question>();

                var a = InitJiaFaList();
                var b = InitJianFaList();

                List.AddRange(a);
                List.AddRange(b);

                ListCount = List.Count;
            }

            return List;
        }

        private static List<CRW_Question> sJiaFaList { get; set; }

        private static List<CRW_Question> sJianFaList { get; set; }

        public static List<CRW_Question> InitJiaFaList()
        {
            if (sJiaFaList == null)
            {
                sJiaFaList = new List<CRW_Question>();

                var query = TiKu_JiaFa
                            .Split('\r', '\n')
                            .Where(i => i.IsNullOrWhiteSpace() == false);

                foreach (string item in query)
                {
                    var qArr = item.Split(',')
                                .Where(i => i.IsNullOrWhiteSpace() == false)
                                .Select(i => i.TrimAdv())
                                .ToArray();

                    if (qArr.Length != 4) { continue; }

                    CRW_Question toAdd = new CRW_Question();

                    toAdd.Left = int.Parse(qArr[0]);
                    toAdd.Symbol = qArr[1];
                    toAdd.Right = int.Parse(qArr[2]);
                    toAdd.EqualsSymbol = "=";
                    toAdd.Result = int.Parse(qArr[3]);

                    sJiaFaList.Add(toAdd);
                }
            }

            return sJiaFaList;
        }

        public static List<CRW_Question> InitJianFaList()
        {
            if (sJianFaList == null)
            {
                sJianFaList = new List<CRW_Question>();

                var query = TiKu_JianFa
                            .Split('\r', '\n')
                            .Where(i => i.IsNullOrWhiteSpace() == false);

                foreach (string item in query)
                {
                    var qArr = item.Split(',')
                                .Where(i => i.IsNullOrWhiteSpace() == false)
                                .Select(i => i.TrimAdv())
                                .ToArray();

                    if (qArr.Length != 4) { continue; }

                    CRW_Question toAdd = new CRW_Question();

                    toAdd.Left = int.Parse(qArr[0]);
                    toAdd.Symbol = qArr[1];
                    toAdd.Right = int.Parse(qArr[2]);
                    toAdd.EqualsSymbol = "=";
                    toAdd.Result = int.Parse(qArr[3]);

                    sJianFaList.Add(toAdd);
                }
            }

            return sJianFaList;
        }

        #region 加法题库

        private static string TiKu_JiaFa =
@"0, +, 0, 0 
0, +, 1, 1 
0, +, 2, 2 
0, +, 3, 3 
0, +, 4, 4 
0, +, 5, 5 
0, +, 6, 6 
0, +, 7, 7 
0, +, 8, 8 
0, +, 9, 9 
1, +, 0, 1 
1, +, 1, 2 
1, +, 2, 3 
1, +, 3, 4 
1, +, 4, 5 
1, +, 5, 6 
1, +, 6, 7 
1, +, 7, 8 
1, +, 8, 9 
2, +, 0, 2 
2, +, 1, 3 
2, +, 2, 4 
2, +, 3, 5 
2, +, 4, 6 
2, +, 5, 7 
2, +, 6, 8 
2, +, 7, 9 
3, +, 0, 3 
3, +, 1, 4 
3, +, 2, 5 
3, +, 3, 6 
3, +, 4, 7 
3, +, 5, 8 
3, +, 6, 9 
4, +, 0, 4 
4, +, 1, 5 
4, +, 2, 6 
4, +, 3, 7 
4, +, 4, 8 
4, +, 5, 9 
5, +, 0, 5 
5, +, 1, 6 
5, +, 2, 7 
5, +, 3, 8 
5, +, 4, 9 
6, +, 0, 6 
6, +, 1, 7 
6, +, 2, 8 
6, +, 3, 9 
7, +, 0, 7 
7, +, 1, 8 
7, +, 2, 9 
8, +, 0, 8 
8, +, 1, 9 
9, +, 0, 9";

        #endregion

        #region 减法题库

        private static string TiKu_JianFa =
@"0, -, 0, 0 
1, -, 0, 1 
1, -, 1, 0 
2, -, 0, 2 
2, -, 1, 1 
2, -, 2, 0 
3, -, 0, 3 
3, -, 1, 2 
3, -, 2, 1 
3, -, 3, 0 
4, -, 0, 4 
4, -, 1, 3 
4, -, 2, 2 
4, -, 3, 1 
4, -, 4, 0 
5, -, 0, 5 
5, -, 1, 4 
5, -, 2, 3 
5, -, 3, 2 
5, -, 4, 1 
5, -, 5, 0 
6, -, 0, 6 
6, -, 1, 5 
6, -, 2, 4 
6, -, 3, 3 
6, -, 4, 2 
6, -, 5, 1 
6, -, 6, 0 
7, -, 0, 7 
7, -, 1, 6 
7, -, 2, 5 
7, -, 3, 4 
7, -, 4, 3 
7, -, 5, 2 
7, -, 6, 1 
7, -, 7, 0 
8, -, 0, 8 
8, -, 1, 7 
8, -, 2, 6 
8, -, 3, 5 
8, -, 4, 4 
8, -, 5, 3 
8, -, 6, 2 
8, -, 7, 1 
8, -, 8, 0 
9, -, 0, 9 
9, -, 1, 8 
9, -, 2, 7 
9, -, 3, 6 
9, -, 4, 5 
9, -, 5, 4 
9, -, 6, 3 
9, -, 7, 2 
9, -, 8, 1 
9, -, 9, 0";

        #endregion

    }

}
