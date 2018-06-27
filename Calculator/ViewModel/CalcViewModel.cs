using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Calculator.Annotations;
using Calculator.ViewModel.Commands;

namespace Calculator.ViewModel
{
	public class CalcViewModel:INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private Calc _calculator;

		private string _currentValue = "0";
		public string CurrentValue
		{
			get => _currentValue;
			set
			{
				_currentValue = value;
				OnPropertyChanged();
			}
		}

		public ButtonPressedCommand ButtonPressedCommand { get; set; }

		public CalcViewModel()
		{
			ButtonPressedCommand = new ButtonPressedCommand(this);
			_calculator = new Calc();
		}

		public void ProcessDigit(string value)
		{
			CurrentValue = _calculator.ProcessDigit(value);
		}

		public void ProcessOperator(string buttonContent)
		{
			var val = _calculator.ProcessOperator(buttonContent);

			if (!string.IsNullOrWhiteSpace(val))
				CurrentValue = val;
		}
	}
}
