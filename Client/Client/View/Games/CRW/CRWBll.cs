using System;
using System.Collections.Generic;
using System.Text;

namespace Client.View.Games.CRW
{
    public class CRWBll
    {
        public CRWBll()
        {
            TiKu.InitList();
        }

        public List<CRW_Question> GetCRW_QuestionByList(CRW_Level level)
        {
            List<CRW_Question> r = new List<CRW_Question>();

            var list = TiKu.InitList();

            Random random = new Random();

            for (int index = 0; index < level.QuestionCount; index++)
            {
                int randomIndex = random.Next(TiKu.ListCount);
                var randTiKu = list[randomIndex];

                CRW_Question toAdd = new CRW_Question();
                toAdd.No = index + 1;
                toAdd.Left = randTiKu.Left;
                toAdd.Symbol = randTiKu.Symbol;
                toAdd.Right = randTiKu.Right;
                toAdd.Result = randTiKu.Result;

                toAdd.ChangeStatus(CRW_Question_Status.Remember);

                r.Add(toAdd);
            }

            return r;
        }
    }
}
