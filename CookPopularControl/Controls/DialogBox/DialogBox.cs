﻿using CookPopularControl.Controls.Adorner;
using CookPopularControl.Tools.Boxes;
using CookPopularControl.Tools.Extensions;
using Microsoft.Xaml.Behaviors.Layout;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;



/*
 * Copyright (c) 2021 All Rights Reserved.
 * Description：DialogBox
 * Author： Chance_写代码的厨子
 * Create Time：2021-04-12 13:55:42
 */
namespace CookPopularControl.Controls.DialogBox
{
    /// <summary>
    /// 对话窗口
    /// </summary>
    public class DialogBox : ContentControl
    {
        private AdornerContainer Container;
        private static readonly HashSet<FrameworkElement> DialogInstances = new HashSet<FrameworkElement>();
        public static readonly ICommand OpenDialogCommand = new RoutedCommand("OpenDialog", typeof(DialogBox));
        public static readonly ICommand CloseDialogCommand = new RoutedCommand("CloseDialog", typeof(DialogBox));


        public static string GetMark(DependencyObject obj) => (string)obj.GetValue(MarkProperty);
        public static void SetMark(DependencyObject obj, string value) => obj.SetValue(MarkProperty, value);
        /// <summary>
        /// <see cref="MarkProperty"/>表示<see cref="DialogBox"/>的标识附加属性
        /// </summary>
        public static readonly DependencyProperty MarkProperty =
            DependencyProperty.RegisterAttached("Mark", typeof(string), typeof(DialogBox), new PropertyMetadata(default(string), OnMarkValueChanged));

        private static void OnMarkValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                var isExist = DialogInstances.Select(d => GetMark(d) == e.NewValue.ToString()).SingleOrDefault();
                if (!isExist)
                    DialogInstances.Add(element);
            }
        }


        static DialogBox()
        {
            CommandManager.RegisterClassCommandBinding(typeof(FrameworkElement), new CommandBinding(OpenDialogCommand, Executed, (s, e) => e.CanExecute = true));
            CommandManager.RegisterClassCommandBinding(typeof(DialogBox), new CommandBinding(CloseDialogCommand, Executed, (s, e) => e.CanExecute = true));
        }

        private static void Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var dialogBox = sender as DialogBox;
            if (e.Command == OpenDialogCommand && e.Parameter != null)
                Show(e.Parameter);
            else if (e.Command == CloseDialogCommand)
                dialogBox!.Close();
        }

        public static DialogBox Show<T>(string mark = "") where T : new() => Show(new T(), mark);

        public static DialogBox Show(object content, string mark = "")
        {
            DialogBox dialogBox;
            dialogBox = new DialogBox { Content = content };
            SetMark(dialogBox, mark);

            FrameworkElement element;
            AdornerDecorator adorner;

            if (string.IsNullOrEmpty(GetMark(dialogBox)))
            {
                element = WindowExtension.GetActiveWindow();
                adorner = VisualTreeHelperExtension.GetVisualDescendants(element).OfType<AdornerDecorator>().FirstOrDefault();
            }
            else
            {
                element = DialogInstances.SingleOrDefault(d => GetMark(d) == mark);
                adorner = element is Window ? VisualTreeHelperExtension.GetVisualDescendants(element).OfType<AdornerDecorator>().FirstOrDefault()
                                            : VisualTreeHelperExtension.GetVisualDescendants(element).OfType<DialogBoxContainer>().FirstOrDefault();
            }

            if (adorner is not null)
            {
                var layer = adorner.AdornerLayer;
                if (layer is not null)
                {
                    var container = new AdornerContainer(layer) { Child = dialogBox };
                    dialogBox.Container = container;
                    layer.Add(container);
                }
            }

            return dialogBox;
        }

        public void Close()
        {
            var element = DialogInstances.SingleOrDefault(d => GetMark(d) == GetMark(this));
            if (string.IsNullOrEmpty(GetMark(this)))
                Close(WindowExtension.GetActiveWindow());
            else if (element != null && DialogInstances.Contains(element))
                Close(element);
        }

        private void Close(DependencyObject element)
        {
            if (element != null && Container != null)
            {
                var decorator = VisualTreeHelperExtension.GetVisualDescendants(element).OfType<AdornerDecorator>().FirstOrDefault();
                if (decorator != null)
                {
                    var layer = decorator.AdornerLayer;
                    layer?.Remove(Container);
                    DialogInstances.Remove(this);
                }
            }
        }
    }
}
