using WpfInterface.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace WpfInterface;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private BinaryViewModel BinViewModel { get; set; } = null!;
    public MainWindow()
    {
        InitializeComponent();
        BinViewModel = (BinaryViewModel)this.Resources["BinViewModel"];
    }
    private void DivideButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        BinViewModel.Divide();
        eventArgs.Handled = true; 
    }
    private void EquivalenceButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        BinViewModel.Equivalence();
        eventArgs.Handled = true; 
    }
}

