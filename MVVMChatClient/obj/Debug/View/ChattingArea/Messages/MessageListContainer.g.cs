﻿#pragma checksum "..\..\..\..\..\View\ChattingArea\Messages\MessageListContainer.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F81462442816358342535049082BD9978ADF668D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MVVMChatClient;
using MVVMChatClient.Core.Model;
using Microsoft.Expression.Interactivity.Core;
using Microsoft.Expression.Interactivity.Input;
using Microsoft.Expression.Interactivity.Layout;
using Microsoft.Expression.Interactivity.Media;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace MVVMChatClient {
    
    
    /// <summary>
    /// MessageListContainer
    /// </summary>
    public partial class MessageListContainer : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\..\View\ChattingArea\Messages\MessageListContainer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer scroll;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\..\View\ChattingArea\Messages\MessageListContainer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl itemsControl;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MVVMChatClient;component/view/chattingarea/messages/messagelistcontainer.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\ChattingArea\Messages\MessageListContainer.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.scroll = ((System.Windows.Controls.ScrollViewer)(target));
            
            #line 17 "..\..\..\..\..\View\ChattingArea\Messages\MessageListContainer.xaml"
            this.scroll.PreviewMouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.Scroll_PreviewMouseWheel);
            
            #line default
            #line hidden
            
            #line 18 "..\..\..\..\..\View\ChattingArea\Messages\MessageListContainer.xaml"
            this.scroll.PreviewKeyUp += new System.Windows.Input.KeyEventHandler(this.Scroll_PreviewKeyUp);
            
            #line default
            #line hidden
            
            #line 19 "..\..\..\..\..\View\ChattingArea\Messages\MessageListContainer.xaml"
            this.scroll.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.Scroll_PreviewKeyDown);
            
            #line default
            #line hidden
            
            #line 20 "..\..\..\..\..\View\ChattingArea\Messages\MessageListContainer.xaml"
            this.scroll.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Scroll_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 21 "..\..\..\..\..\View\ChattingArea\Messages\MessageListContainer.xaml"
            this.scroll.PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Scroll_PreviewMouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.itemsControl = ((System.Windows.Controls.ItemsControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

