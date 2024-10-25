﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EnvelopeBlock
{
    public partial class SetValEditWindow : Window
    {
        bool invalid;
        int min = 0;
        int max = 255;
        int value = 0;

        public int Value { get { return value; } }

        public SetValEditWindow(int val, int min, int max)
        {
            InitializeComponent();
            textBox.KeyDown += textBox_KeyDown;
            textBox.TextChanged += textBox_TextChanged;

            this.min = min;
            this.max = max;

            if (val >= 0)
            {
                textBox.Text = EnvelopeBlockMachine.Settings.NumeralSystem == DisplayValueTypes.Dec ? val.ToString() : val.ToString("X");
                textBox.SelectAll();
                invalid = false;
            }
            else
            {
                invalid = true;
            }

            textBox.Focus();
        }

        void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (EnvelopeBlockMachine.Settings.NumeralSystem == DisplayValueTypes.Dec)
                invalid = !int.TryParse(textBox.Text, out value) || value > max || value < min;
            else
                invalid = !int.TryParse(textBox.Text, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out value) || value > max || value < min;

            textBox.Foreground = invalid ? Brushes.Red : Brushes.Black;
        }

        void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogResult = false;
                Close();
                e.Handled = true;
            }

            if (e.Key == Key.Return && !invalid)
            {
                DialogResult = true;
                Close();
                e.Handled = true;
            }
        }
    }
}
