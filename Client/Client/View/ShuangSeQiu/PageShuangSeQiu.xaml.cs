using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View.ShuangSeQiu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageShuangSeQiu : ContentPage
    {
        public int RedMax
        {
            get { return 33; }
        }

        public int BlueMax
        {
            get { return 16; }
        }

        UcShuangSeQiu[] RedArray { get; set; }
        UcShuangSeQiu[] BlueArray { get; set; }

        public PageShuangSeQiu()
        {
            InitializeComponent();
            initUI();
            initEvent();
        }



        private void initUI()
        {
            this.RedArray = new UcShuangSeQiu[33];
            this.BlueArray = new UcShuangSeQiu[16];

            int count = 0;
            for (int rowIndex = 0; rowIndex < 5; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < 7; columnIndex++)
                {
                    count = count + 1;

                    if (count > this.RedMax)
                    {
                        break;
                    }

                    UcShuangSeQiu toAdd = new UcShuangSeQiu();
                    toAdd.Btn.Text = "{0}".FormatWith(count);
                    gRedBalls.Children.Add(toAdd);
                    this.RedArray[count - 1] = toAdd;

                    Grid.SetRow(toAdd, rowIndex);
                    Grid.SetColumn(toAdd, columnIndex);
                }

                if (count > this.RedMax)
                {
                    break;
                }
            }


            count = 0;
            for (int rowIndex = 0; rowIndex < 3; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < 7; columnIndex++)
                {
                    count = count + 1;

                    if (count > this.BlueMax)
                    {
                        break;
                    }

                    UcShuangSeQiu toAdd = new UcShuangSeQiu();
                    toAdd.Btn.Text = "{0}".FormatWith(count);
                    gBlueBalls.Children.Add(toAdd);
                    this.BlueArray[count - 1] = toAdd;

                    Grid.SetRow(toAdd, rowIndex);
                    Grid.SetColumn(toAdd, columnIndex);
                }

                if (count > this.BlueMax)
                {
                    break;
                }
            }
        }

        private void initEvent()
        {
            this.btnRandomRed6.Clicked += BtnRandomRed6_Clicked;
            this.btnRandomBlue1.Clicked += BtnRandomBlue1_Clicked;
        }

        private void BtnRandomRed6_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.IsEnabled = false;

            try
            {
                randomRed6();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                btn.IsEnabled = true;
            }
        }

        Random rand = new Random(DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second);

        public void randomRed6()
        {
            bool[] r = new bool[this.RedMax];
            int count = 6;
            for (int i = 0; i < count; i++)
            {
                int randNo = rand.Next(this.RedMax);
                int tmpIndex = randNo - 1;

                while (tmpIndex < 0 || r[tmpIndex] == true)
                {
                    randNo = rand.Next(this.RedMax);
                    tmpIndex = randNo - 1;
                }

                r[tmpIndex] = true;
            }

            for (int index = 0; index < this.RedMax; index++)
            {
                this.RedArray[index].SetIsSelected(false);
                if (r[index])
                {
                    this.RedArray[index].SetIsSelected(true);
                }
            }
        }

        private void BtnRandomBlue1_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.IsEnabled = false;

            try
            {
                randomBlue1();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                btn.IsEnabled = true;
            }
        }

        void randomBlue1()
        {
            bool[] r = new bool[this.BlueMax];
            int count = 1;
            for (int i = 0; i < count; i++)
            {
                int randNo = rand.Next(this.BlueMax);
                int tmpIndex = randNo - 1;

                while (tmpIndex < 0 || r[tmpIndex] == true)
                {
                    randNo = rand.Next(this.BlueMax);
                    tmpIndex = randNo - 1;
                }

                r[tmpIndex] = true;
            }

            for (int index = 0; index < this.BlueMax; index++)
            {
                this.BlueArray[index].SetIsSelected(false, isRedBall: false);
                if (r[index])
                {
                    this.BlueArray[index].SetIsSelected(true, isRedBall: false);
                }
            }
        }

    }

    public class PageShuangSeQiuViewModel : ViewModel.BaseViewModel
    {
        //public UcShuangSeQiu[] SelectedRedArray = new UcShuangSeQiu[6];

        //public UcShuangSeQiu[] SelectedBlueArray = new UcShuangSeQiu[1];

    }

}