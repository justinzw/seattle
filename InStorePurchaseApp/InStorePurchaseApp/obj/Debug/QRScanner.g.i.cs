﻿#pragma checksum "C:\Users\cbingh\Documents\Visual Studio 2012\Projects\InStorePurchaseApp\InStorePurchaseApp\QRScanner.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "103EFC1969A432ACAD235C17FF06A317"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace InStorePurchaseApp {
    
    
    public partial class QRScanner : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Canvas Canvas;
        
        internal System.Windows.Media.VideoBrush ViewFinder;
        
        internal System.Windows.Media.CompositeTransform ViewFinderTransform;
        
        internal System.Windows.Controls.TextBlock tbBarcodeType;
        
        internal System.Windows.Controls.TextBlock tbBarcodeData;
        
        internal System.Windows.Controls.Button puchaseButton;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/InStorePurchaseApp;component/QRScanner.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.Canvas = ((System.Windows.Controls.Canvas)(this.FindName("Canvas")));
            this.ViewFinder = ((System.Windows.Media.VideoBrush)(this.FindName("ViewFinder")));
            this.ViewFinderTransform = ((System.Windows.Media.CompositeTransform)(this.FindName("ViewFinderTransform")));
            this.tbBarcodeType = ((System.Windows.Controls.TextBlock)(this.FindName("tbBarcodeType")));
            this.tbBarcodeData = ((System.Windows.Controls.TextBlock)(this.FindName("tbBarcodeData")));
            this.puchaseButton = ((System.Windows.Controls.Button)(this.FindName("puchaseButton")));
        }
    }
}
