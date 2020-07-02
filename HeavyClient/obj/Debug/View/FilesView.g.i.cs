﻿#pragma checksum "..\..\..\View\FilesView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6A21E8B6EE0BC3F667EA919D4C218BF460EE3C91"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using HeavyClient.View;
using HeavyClient.ViewModel;
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
using XamlAnimatedGif;


namespace HeavyClient.View {
    
    
    /// <summary>
    /// FilesView
    /// </summary>
    public partial class FilesView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 41 "..\..\..\View\FilesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel selectionPanel;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\View\FilesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox fileList;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\View\FilesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button decryptButton;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\View\FilesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel processingPanel;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\View\FilesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textProcessState;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\View\FilesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image loadingGif;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\View\FilesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textTimer;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\View\FilesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar Progress;
        
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
            System.Uri resourceLocater = new System.Uri("/HeavyClient;component/view/filesview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\FilesView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            this.selectionPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 2:
            
            #line 43 "..\..\..\View\FilesView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.openDialog);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 44 "..\..\..\View\FilesView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.emptyList);
            
            #line default
            #line hidden
            return;
            case 4:
            this.fileList = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            this.decryptButton = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\View\FilesView.xaml"
            this.decryptButton.Click += new System.Windows.RoutedEventHandler(this.decrypt);
            
            #line default
            #line hidden
            return;
            case 6:
            this.processingPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 7:
            this.textProcessState = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.loadingGif = ((System.Windows.Controls.Image)(target));
            return;
            case 9:
            this.textTimer = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.Progress = ((System.Windows.Controls.ProgressBar)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

