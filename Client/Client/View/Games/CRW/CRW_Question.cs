using System;
using System.Collections.Generic;
using System.Text;

namespace Client.View.Games.CRW
{
    public class CRW_Question : ViewModel.BaseViewModel
    {
        public int No { get; set; }

        public string NoInfo { get; set; }

        public int Left { get; set; }

        public string Symbol { get; set; }

        public int Right { get; set; }

        public string EqualsSymbol { get; set; }

        public int Result { get; set; }

        public string LeftInfo { get; set; }

        public string SymbolInfo { get; set; }

        public string RightInfo { get; set; }

        public string ResultInfo { get; set; }

        public CRW_Question_Status Status { get; set; }

        public const string cQuestionMark = "?";

        public void ChangeStatus(CRW_Question_Status status)
        {
            switch (status)
            {
                case CRW_Question_Status.Remember:
                    {
                        this.NoInfo = "{0}题".FormatWith(this.No);
                        this.LeftInfo = this.Left.ToString();
                        this.SymbolInfo = this.Symbol.ToString();
                        this.RightInfo = this.Right.ToString();
                        this.ResultInfo = cQuestionMark;
                        this.Status = CRW_Question_Status.Remember;
                    }
                    break;
                case CRW_Question_Status.Answer:
                    {
                        this.NoInfo = "{0}题".FormatWith(this.No);
                        this.LeftInfo = cQuestionMark;
                        this.SymbolInfo = cQuestionMark;
                        this.RightInfo = cQuestionMark;
                        this.ResultInfo = cQuestionMark;
                        this.Status = CRW_Question_Status.Answer;
                    }
                    break;
                case CRW_Question_Status.ShowAnswer:
                    {
                        this.NoInfo = "{0}题".FormatWith(this.No);
                        this.LeftInfo = this.Left.ToString();
                        this.SymbolInfo = this.Symbol.ToString();
                        this.RightInfo = this.Right.ToString();
                        this.ResultInfo = this.Result.ToString();
                        this.Status = CRW_Question_Status.ShowAnswer;
                    }
                    break;
                default:
                    break;
            }

            NotifyUI();
        }

        public void NotifyUI()
        {
            this.OnPropertyChanged("NoInfo");
            this.OnPropertyChanged("LeftInfo");
            this.OnPropertyChanged("SymbolInfo");
            this.OnPropertyChanged("RightInfo");
            this.OnPropertyChanged("ResultInfo");
        }

    }

    public enum CRW_Question_Status
    {
        Remember = 0,
        Answer = 1,
        ShowAnswer = 2
    }

}
