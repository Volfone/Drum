using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Drum
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random = new Random();
        string neededWord = "ASDASDASDASDASDASD";
        List<char> chars = new List<char>();
        int letterCount = 0;
        List<char> firstList = new List<char>();
        List<char> secondList = new List<char>();
        string word = "";
        List<char> Word = new List<char>();

        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 40; i++)
            {
                chars.Add(' ');
            }
            foreach (var i in neededWord)
            {
                letterCount++;
                int j = random.Next(chars.Count);
                while(chars[j] != ' ')
                {
                    j = random.Next(chars.Count);
                }
                chars[j] = i;
            }
            for (int i = 0; i < 40; i++)
            {
                if (chars[i] == ' ')
                {
                    chars[i] = Convert.ToChar(random.Next(65, 90));
                }
            }
            for (int i = 0; i < chars.Count; i++)
            {
                firstList.Add(chars[i]);
            }
            foreach (char c in firstList)
            {
                secondList.Add(c);
            }
            Update();
        }
        public void Update()
        {
            LetterPanel.Children.Clear();
            for (int i = 0; i < neededWord.Length; i++)
            {
                TextBox TB = new TextBox();
                TB.Name = "TextBox" + "_" + i;
                TB.TextAlignment = TextAlignment.Center;
                TB.FontSize = 24;
                TB.IsEnabled = false;
                TB.BorderBrush = Brushes.Black;
                TB.Height = 40;
                TB.Width = 40;
                LetterPanel.Children.Add(TB);
            }/*
            MessageBox.Show($"{letterCount}");*/
            for(int i = 0; i < Word.Count; i++)
            {
                foreach(TextBox TB in LetterPanel.Children)
                {
                    if (TB.Name == "TextBox_" + i)
                    {
                        TB.Text += Word[i];
                    }
                }
            }

            ButtonPanel.Children.Clear();
            ButtonPanel_2.Children.Clear();
            ButtonPanel_3.Children.Clear();
            ButtonPanel_4.Children.Clear();
            for (int i = 0; i < secondList.Count; i++)
            {
                Button btn = new Button();
                btn.Name = "Button" + "_" + i;
                btn.Content = secondList[i];
                btn.Height = 20;
                btn.Width = 20;
                btn.Click += Click;
                btn.Margin = new Thickness(5, 0, 0, 0);
                if (secondList[i] == ' ')
                {
                    btn.Visibility = Visibility.Hidden;
                }
                if (i < 10)
                {
                    ButtonPanel.Children.Add(btn);
                }
                if (i < 20 && i >= 10)
                {
                    ButtonPanel_2.Children.Add(btn);
                }
                if (i < 30 && i >= 20)
                {
                    ButtonPanel_3.Children.Add(btn);
                }
                if (i <= 50 && i >= 30)
                {
                    ButtonPanel_4.Children.Add(btn);
                }
            }
        }
        public void Click(object sender, RoutedEventArgs e)
        {
            int num = Convert.ToInt32((sender as Button).Name.Split('_')[1]);
            /*MessageBox.Show($"{num} {secondList[num]}");*/
            bool isFilled = false;
            bool isSame = false;
            foreach (TextBox letter in LetterPanel.Children)
            {
                if(letter.Text == "" && !isFilled)
                {
                    isFilled = true;
                    Word.Add(secondList[num]);
                    word += secondList[num].ToString();
                    letter.Text = secondList[num].ToString();
                }
                if (neededWord.Length == Word.Count)
                {
                    for (int i = 0; i < Word.Count; i++)
                    {
                        if (neededWord[i] == Word[i])
                        {
                            isSame = true;
                        }
                        else
                        {
                            isSame = false;
                        }
                    }
                }
                if (letterCount == (Convert.ToInt32(letter.Name.Split('_')[1]) + 1) && letter.Text != ""/* && isSame*/)
                {
                    string completedWord = "";
                    foreach(var symbol in Word)
                    {
                        completedWord += symbol.ToString();
                    }
                    if(neededWord == completedWord)
                    {
                        MessageBox.Show($"{completedWord} is correct");
                        Word = new List<char>();
                        secondList = firstList;
                        Update();
                        return;
                    }
                    else
                    {
                        MessageBox.Show($"{completedWord} is wrong");
                        Word = new List<char>();
                        secondList.Clear();
                        foreach(char c in firstList)
                        {
                            secondList.Add(c);
                        }
                        Update();
                        return;
                    }
                }
            }
            (sender as Button).Visibility = Visibility.Hidden;
            secondList[num] = ' ';
            Update();
            /*Panel.Children.RemoveAt(num);*/
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
