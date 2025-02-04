﻿using CookPopularControl.Controls.Notify;
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
using System.Windows.Shapes;

namespace MvvmTestDemo.DemosView
{
    /// <summary>
    /// NotifyDemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyDemoWindow : Window
    {
        public NotifyDemoWindow()
        {
            InitializeComponent();
        }

        private int bubbleMessageIndex = 1;
        private const string token = "NewPanel";
        private void OpenBubbleMessage_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            switch (btn.Name)
            {
                case "ShowInfoBubbleMessage":
                    BubbleMessage.ShowInfo($"写代码的厨子_{bubbleMessageIndex++}", token);
                    break;
                case "ShowWarningBubbleMessage":
                    BubbleMessage.ShowWarning($"写代码的厨子_{bubbleMessageIndex++}", token);
                    break;
                case "ShowErrorBubbleMessage":
                    BubbleMessage.ShowError($"写代码的厨子_{bubbleMessageIndex++}", token);
                    break;
                case "ShowFatalBubbleMessage":
                    BubbleMessage.ShowFatal($"写代码的厨子_{bubbleMessageIndex++}", token);
                    break;
                case "ShowQuestionBubbleMessage":
                    BubbleMessage.ShowQuestion($"写代码的厨子_{bubbleMessageIndex++}",isSure =>
                    {
                        //MessageDialog.ShowInfo($"Clicked the {isSure.ToString()}");
                        BubbleMessage.ShowInfo($"Clicked the {isSure}");
                        return true;
                    }, token);
                    break;
                case "ShowSuccessBubbleMessage":
                    BubbleMessage.ShowSuccess($"写代码的厨子_{bubbleMessageIndex++}", token);
                    break;
                default:
                    break;
            }
        }
    }
}
