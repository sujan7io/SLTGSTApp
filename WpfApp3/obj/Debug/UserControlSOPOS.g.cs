﻿#pragma checksum "..\..\UserControlSOPOS.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "092ABA73F978F25D1BEFC55369334DB8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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
using WpfApp3;


namespace WpfApp3 {
    
    
    /// <summary>
    /// UserControlSOPOS
    /// </summary>
    public partial class UserControlSOPOS : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 34 "..\..\UserControlSOPOS.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CustName;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\UserControlSOPOS.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Date;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\UserControlSOPOS.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Time;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\UserControlSOPOS.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox BillNumber;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\UserControlSOPOS.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchText;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\UserControlSOPOS.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DisplaySearch;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\UserControlSOPOS.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DisplayMainTable;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\UserControlSOPOS.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Subtotal;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\UserControlSOPOS.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Tax;
        
        #line default
        #line hidden
        
        
        #line 127 "..\..\UserControlSOPOS.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Discount;
        
        #line default
        #line hidden
        
        
        #line 136 "..\..\UserControlSOPOS.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Total;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApp3;component/usercontrolsopos.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\UserControlSOPOS.xaml"
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
            this.CustName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.Date = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.Time = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.BillNumber = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.SearchText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            
            #line 79 "..\..\UserControlSOPOS.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.DisplaySearch = ((System.Windows.Controls.DataGrid)(target));
            
            #line 80 "..\..\UserControlSOPOS.xaml"
            this.DisplaySearch.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DisplaySearch_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.DisplayMainTable = ((System.Windows.Controls.DataGrid)(target));
            
            #line 90 "..\..\UserControlSOPOS.xaml"
            this.DisplayMainTable.CurrentCellChanged += new System.EventHandler<System.EventArgs>(this.DisplayMainTable_CurrentCellChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Subtotal = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.Tax = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.Discount = ((System.Windows.Controls.TextBox)(target));
            
            #line 127 "..\..\UserControlSOPOS.xaml"
            this.Discount.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Discount_TextChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            this.Total = ((System.Windows.Controls.TextBox)(target));
            return;
            case 13:
            
            #line 141 "..\..\UserControlSOPOS.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

