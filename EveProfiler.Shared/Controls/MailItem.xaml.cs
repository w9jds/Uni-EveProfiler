﻿using EveProfiler.Logic.CharacterAttributes;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EveProfiler.Shared.Controls
{
    public sealed partial class MailItem : UserControl
    {
        public MailItem()
        {
            InitializeComponent();
        }

        public void extendItem()
        {
            Mail thisMail = DataContext as Mail;
            thisMail.IsExtended = !thisMail.IsExtended;
        }
    }
}
