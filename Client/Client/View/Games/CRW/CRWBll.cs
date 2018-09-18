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

                toAdd.CRWTypeID = level.CRWTypeID;

                toAdd.No = index + 1;
                toAdd.Left = randTiKu.Left;
                toAdd.Symbol = randTiKu.Symbol;
                toAdd.Right = randTiKu.Right;
                toAdd.EqualsSymbol = randTiKu.EqualsSymbol;
                toAdd.Result = randTiKu.Result;
                toAdd.TTSMsg = "{0}{1}{2}".FormatWith(ToChineseNumber(toAdd.Left),
                                                      ToChineseSymbol(toAdd.Symbol),
                                                      ToChineseNumber(toAdd.Right));


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

            ttsMsg = "答题完毕, 正确率百分之{0}".FormatWith(this.ToChineseNumber(correctPercentage));

            switch (levelChangeMsg)
            {
                case "上升": ttsMsg += "由于超过百分之{0}等级{1}".FormatWith(this.ToChineseNumber(85), levelChangeMsg); break;
                case "不变": ttsMsg += "由于是百分之{0}等级{1}".FormatWith(this.ToChineseNumber(correctPercentage), levelChangeMsg); break;
                case "下降": ttsMsg += "由于低于百分之{0}等级{1}".FormatWith(this.ToChineseNumber(65), levelChangeMsg); break;
            }
            // ttsMsg += "由于是百分之{0}等级{1}".FormatWith(this.ToChineseNumber(correctPercentage), levelChangeMsg);

            return new Tuple<CRW_Level, string>(new CRW_Level(tmpLevelNo, level.CRWTypeID), ttsMsg);
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

        #region 数字转中文数字

        public string ToChineseNumber(decimal args, bool isUpper = false)
        {
            string r = string.Empty;

            string tmp = args.ToString();
            for (int index = 0; index < tmp.Length; index++)
            {
                char info1 = zhongwen(tmp[index], isUpper);
                string info2 = weishu(tmp.Length - 1 - index);

                if (info1.Equals('一') && (info2.Equals("十") || info2.Equals("十万") || info2.Equals("十亿")))
                {
                    r = "{0}{1}".FormatWith(r, info2);
                    continue;
                }

                if (info1.Equals('零') && index > 0)
                {
                    continue;
                }

                r = "{0}{1}{2}".FormatWith(r, info1, info2);
            }

            return r;
        }

        private char zhongwen(char c, bool isUpper)
        {
            char r = '零';

            if (isUpper)
            {
                switch (c)
                {
                    case '0': r = '零'; break;
                    case '1': r = '壹'; break;
                    case '2': r = '贰'; break;
                    case '3': r = '叁'; break;
                    case '4': r = '肆'; break;
                    case '5': r = '伍'; break;
                    case '6': r = '陆'; break;
                    case '7': r = '柒'; break;
                    case '8': r = '捌'; break;
                    case '9': r = '玖'; break;
                }
            }
            else
            {
                switch (c)
                {
                    case '0': r = '零'; break;
                    case '1': r = '一'; break;
                    case '2': r = '二'; break;
                    case '3': r = '三'; break;
                    case '4': r = '四'; break;
                    case '5': r = '五'; break;
                    case '6': r = '六'; break;
                    case '7': r = '七'; break;
                    case '8': r = '八'; break;
                    case '9': r = '九'; break;
                }
            }

            return r;
        }

        private string weishu(int index)
        {
            string r = string.Empty;
            switch (index)
            {
                case 1: r = "十"; break;
                case 2: r = "百"; break;
                case 3: r = "千"; break;

                case 4: r = "万"; break;
                case 5: r = "十"; break; // 十万
                case 6: r = "百"; break;
                case 7: r = "千"; break;

                case 8: r = "亿"; break;
                case 9: r = "十"; break; // 十亿
                case 10: r = "百"; break;
                case 11: r = "千"; break;

                case 12: r = "兆"; break;
            }

            return r;
        }

        #endregion

        public string ToChineseSymbol(string args)
        {
            string r = string.Empty;

            switch (args)
            {
                case "+": r = "加"; break;
                case "-": r = "减"; break;
            }

            return r;
        }


    }
}
