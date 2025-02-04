﻿using CookPopularControl.Communal.Data.Args;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;


/*
 * Copyright (c) 2021 All Rights Reserved.
 * Description：CookLabel
 * Author： Chance_写代码的厨子
 * Create Time：2021-05-11 08:57:14
 */
namespace CookPopularControl.Controls.GroupControls
{
    /// <summary>
    /// 搜索框
    /// </summary>
    [ContentProperty("Content")]
    [DefaultProperty("Content")]
    [DefaultEvent("ContentChanged")]
    [Localizability(LocalizationCategory.Text)]
    public class SearchControl : Control
    {
        public static readonly ICommand SearchCommand = new RoutedCommand(nameof(SearchCommand), typeof(SearchControl));

        /// <summary>
        /// 搜索内容
        /// </summary>
        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        /// <summary>
        /// 标识<see cref="Content"/>的依赖属性
        /// </summary>
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(SearchControl),
                new PropertyMetadata(default(object), OnContentChanged));

        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SearchControl search)
            {
                search.OnContentChanged();
            }
        }

        [Description("点击搜索按钮时发生")]
        public event RoutedEventHandler StartSearch
        {
            add { this.AddHandler(StartSearchEvent, value); }
            remove { this.RemoveHandler(StartSearchEvent, value); }
        }
        /// <summary>
        /// <see cref="StartSearchEvent"/>标识搜索事件 
        /// </summary>
        public static readonly RoutedEvent StartSearchEvent =
            EventManager.RegisterRoutedEvent("StartSearch", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SearchControl));

        protected virtual void OnStartSearch(string content)
        {
            RoutedPropertySingleEventArgs<string> arg = new RoutedPropertySingleEventArgs<string>(content, StartSearchEvent);
            this.RaiseEvent(arg);
        }


        [Description("搜索内容更改时发生")]
        public event TextChangedEventHandler ContentChanged
        {
            add { this.AddHandler(ContentChangedEvent, value); }
            remove { this.RemoveHandler(ContentChangedEvent, value); }
        }
        public static readonly RoutedEvent ContentChangedEvent =
            EventManager.RegisterRoutedEvent("ContentChanged", RoutingStrategy.Bubble, typeof(TextChangedEventHandler), typeof(SearchControl));

        protected virtual void OnContentChanged()
        {
            TextChangedEventArgs arg = new TextChangedEventArgs(ContentChangedEvent, UndoAction.Create);
            this.RaiseEvent(arg);
        }


        public SearchControl()
        {
            CommandBindings.Add(new CommandBinding(SearchCommand, (s, e) =>
            {
                OnStartSearch((Content ?? string.Empty).ToString());
            }, (s, e) => e.CanExecute = true));
        }
    }
}
