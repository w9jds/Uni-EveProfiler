﻿using EveProfiler.Logic;
using Windows.UI.Xaml.Data;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EveProfiler.Shared.Controls
{
    public sealed partial class CharacterSheet : CharacterControlBase
    {
        public CharacterSheet()
        {
            InitializeComponent();
        }

        public override void SetCharacter(Character character)
        {
            SetBinding(DataContextProperty, new Binding() { Source = character.Attributes[AttributeTypes.Sheet] });
        }
    }
}
