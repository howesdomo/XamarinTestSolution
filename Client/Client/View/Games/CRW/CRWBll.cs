using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
                toAdd.CorrectImageSource = "game_CRW_Correct.png";
                toAdd.WrongImageSource = "game_CRW_Wrong.png";

                toAdd.No = index + 1;
                toAdd.Left = randTiKu.Left;
                toAdd.Symbol = randTiKu.Symbol;
                toAdd.Right = randTiKu.Right;
                toAdd.EqualsSymbol = randTiKu.EqualsSymbol;
                toAdd.Result = randTiKu.Result;

                toAdd.ChangeStatus(CRW_Question_Status.Remember);

                r.Add(toAdd);
            }

            return r;
        }

        /// <summary>
        /// 检查正确率
        /// 0% ~ 100%
        /// </summary>
        public decimal CheckCorrectPercentage(List<CRW_Question> args)
        {
            int correctCount = args.Count(i => i.Status == CRW_Question_Status.InputCorrectAnswer);
            int total = args.Count;

            decimal r = Convert.ToDecimal(correctCount) / Convert.ToDecimal(total) * 100M;

            // 向下取整, 提高难度 
            // 33.3333% ==> 33%
            // 65.5555% ==> 65%
            int r2 = Convert.ToInt32(r.ToString("N0"));
            return r2;
        }

        /// <summary>
        /// 根据正确率 判断下一局的等级
        /// </summary>
        public Tuple<CRW_Level, string> CalcLevel(CRW_Level level, List<CRW_Question> args)
        {
            // 语音
            string ttsMsg = string.Empty;
            string levelChangeMsg = string.Empty;

            if (level == null)
            {
                throw new BusinessException("level is null");
            }

            if (level.LevelNo <= 0)
            {
                throw new BusinessException("levelNo 少于 0");
            }

            decimal correctPercentage = this.CheckCorrectPercentage(args);

            int tmpLevelNo = -9876;
            if (correctPercentage <= 65m) // 降级
            {
                tmpLevelNo = level.LevelNo - 1;
                levelChangeMsg = "下降";
            }
            else if (correctPercentage > 85m) // 升级
            {
                tmpLevelNo = level.LevelNo + 1;
                levelChangeMsg = "上升";
            }
            else // 等级不变
            {
                tmpLevelNo = level.LevelNo;
                levelChangeMsg = "不变";
            }

            if (tmpLevelNo <= 0)
            {
                tmpLevelNo = 1;
            }

            //
            ttsMsg = "答题完毕, 正确率百分之{0}".FormatWith(correctPercentage);
            ttsMsg += "由于是百分之{0}等级{1}".FormatWith( correctPercentage, levelChangeMsg);

            return new Tuple<CRW_Level, string>(new CRW_Level(tmpLevelNo), ttsMsg);
        }

        public Tuple<int, CRW_Question, CRW_Question> ReadNextQuestion(int? currentIndex, CRW_Level level, List<CRW_Question> questionList)
        {
            int index = 0;

            if (currentIndex.HasValue == false)
            {
                currentIndex = 0;
            }

            index = currentIndex.Value;

            CRW_Question toRemember = null;
            CRW_Question toAnswer = null;

            if (index <= level.RememberEndIndex)
            {
                toRemember = questionList[index];
                toRemember.ChangeStatus(CRW_Question_Status.Remember);
            }

            if (index >= level.AnswerStartIndex && index <= level.MaxIndex)
            {
                toAnswer = questionList[index - level.SuSuan];
                toAnswer.ChangeStatus(CRW_Question_Status.Answer);
            }

            return new Tuple<int, CRW_Question, CRW_Question>
            (
                index + 1,
                toRemember,
                toAnswer
            );
        }
    }
}
