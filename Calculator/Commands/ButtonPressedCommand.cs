using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator.ViewModel.Commands
{
	public class ButtonPressedCommand : ICommand
	{
		private CalcViewModel CalcViewModel { get; }

		public event EventHandler CanExecuteChanged;

		public ButtonPressedCommand(CalcViewModel calcViewModel)
		{
			CalcViewModel = calcViewModel;
		}
		
		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			string buttonContent = parameter.ToString();

			if (char.IsDigit(buttonContent[0]))
				CalcViewModel.ProcessDigit(buttonContent);
			else
				CalcViewModel.ProcessOperator(buttonContent);
		}
	}
}
