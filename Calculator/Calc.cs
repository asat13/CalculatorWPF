using System;
using System.Data.Odbc;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace Calculator
{
	public class Calc
	{
		private string _firstDigit = "0", _result = "0";
		private char? _operator;
		private bool _operationReady = false;

		public Calc()
		{
			_operator = null;
		}

		public string ProcessDigit(string value)
		{
			if (_operator == null)
			{
				_firstDigit = (_firstDigit == "" || _firstDigit == "0") ? value : $"{_firstDigit}{value}";

				return _firstDigit;
			}

			_result = (_result == "" || _result == "0") ? value : $"{_result}{value}";

			_operationReady = true;
			return _result;
		}

		public string ProcessOperator(string buttonContent)
		{

			if (buttonContent.Equals("AC"))
			{
				Reset();
				return "0";
			}

			if (buttonContent.Equals("+/-"))
			{
				if (_operator == null)
				{
					if(_firstDigit != "0")
						_firstDigit = _firstDigit.Contains("-") ? _firstDigit.Replace("-", "") : $"-{_firstDigit}";

					return _firstDigit;
				}
				else
				{
					if(_result != "0")
						_result = _result.Contains("-") ? _result.Replace("-", "") : $"-{_result}";

					return _result;
				}
			}

			if (buttonContent.Equals("="))
				return _operationReady ? RunOperation() : null;

			if (buttonContent.Equals("."))
			{
				if (_operator == null)
				{
					if (!_firstDigit.Contains(".")) 
						_firstDigit = $"{_firstDigit}{buttonContent}";
					
					return _firstDigit;
				}

				if (!_result.Contains(".")) 
					_result = $"{_result}{buttonContent}";

				return _result;
			}

			if (buttonContent.Length > 1) 
				return null;
			
			_operator = buttonContent[0];
			return null;
		}

		private void Reset()
		{
			_operator = null;
			_operationReady = false;
			_firstDigit = "0";
			_result = "0";
		}

		private string RunOperation()
		{
			double first = double.Parse(_firstDigit);
			double result = double.Parse(_result);

			switch (_operator)
			{
				case '+':
					result = first + result;	
					break;
				case '-':
					result = first - result;
					break;
				case '/':
					try
					{
						result = first / result;
					}
					catch (Exception e)
					{
						return "Error";
					}
					
					break;
				case '*':
					result = first * result;
					break;
				default:
					result = 0;
					break;
			}

			_firstDigit = result.ToString();
			var temp = result;
			_result = "";
			_operationReady = false;
			_operator = null;
			return temp.ToString();
		}
	}
}