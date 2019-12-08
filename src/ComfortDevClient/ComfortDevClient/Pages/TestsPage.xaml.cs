using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Logic.Classes;
using ComfortDevClient.ViewModel;
using ComfortDevClient.Pages;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;
using System.Threading;
using Logic;

namespace ComfortDevClient.Pages
{
    /// <summary>
    /// Interaction logic for TestsPage.xaml
    /// </summary>
    public partial class TestsPage : Page
    {
        private Test test;

        public TestsPage()
        {
            InitializeComponent();            
            GetTest();
        }

        private async void GetTest()
        {
            IEnumerable<Question> questions = await Actions.GetTestQuestions();
            if (questions != null)
            {
                test = new Test(new List<Question>(questions));
                test.ShowQuestion(this);
            }
        }

        private void Next_button_Click(object sender, RoutedEventArgs e)
        {

            test?.NextQuestion();
            test?.ShowQuestion(this);
        }

        private void Previous_button_Click(object sender, RoutedEventArgs e)
        {
            test?.PreviousQuestion();
            test?.ShowQuestion(this);
        }

        private void SkipButton_Click(object sender, RoutedEventArgs e)
        {
            var result = test?.GetResult();
        }
    }

    class Test
    {
        private List<string> questions;
        private List<List<KeyValuePair<RadioButton, Answer>>> answers;
        private int currentQuestionIndex;

        public Test(List<Question> questions)
        {
            this.questions = new List<string>();
            questions.ForEach(quest => this.questions.Add(quest.QuestionText));

            this.answers = new List<List<KeyValuePair<RadioButton, Answer>>>();
            foreach (var question in questions)
            {
                var groupAnswers = new List<KeyValuePair<RadioButton, Answer>>();
                foreach (var answer in question.Answers)
                {
                    groupAnswers.Add(
                        new KeyValuePair<RadioButton, Answer>(
                            new RadioButton { Content = answer.AnswerText }, answer));
                }
                answers.Add(groupAnswers);
            }

            currentQuestionIndex = 0;
        }

        public bool IsFirstQuestion()
        {
            return currentQuestionIndex == 0;
        }

        public bool IsLastQuestion()
        {
            return currentQuestionIndex == questions.Count - 1;
        }

        public void NextQuestion()
        {
            if (!this.IsLastQuestion())
            {
                currentQuestionIndex++; 
            }
        }

        public void PreviousQuestion()
        {
            if (!this.IsFirstQuestion())
            {
                currentQuestionIndex--;
            }
        }

        public void ShowQuestion(TestsPage testPage)
        {
            testPage.question.Text = questions[currentQuestionIndex];

            testPage.blockOfAnswers.Children.Clear();
            foreach (var pair in answers[currentQuestionIndex])
            {
                testPage.blockOfAnswers.Children.Add(pair.Key);
            }
        }

        public Dictionary<int, int> GetResult()
        {
            var result = new Dictionary<int, int>();

            foreach (var answerGroup in answers)
            {
                foreach (var pair in answerGroup)
                {
                    if (pair.Key.IsChecked.Value)
                    {
                        if (!result.ContainsKey(pair.Value.Topic.Id))
                        {
                            result[pair.Value.Topic.Id] = 1;
                        }
                        else
                        {
                            result[pair.Value.Topic.Id]++;
                        }
                    }
                }
            }

            return result;
        }
    }
}
