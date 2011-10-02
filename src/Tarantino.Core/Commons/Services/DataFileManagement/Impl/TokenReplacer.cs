using System;

using Tarantino.Core.Commons.Services.DataFileManagement;

namespace Tarantino.Core.Commons.Services.DataFileManagement.Impl
{
	
	public class TokenReplacer : ITokenReplacer
	{
		private string _text;

		public string Text
		{
			get { return _text; }
			set { _text = value; }
		}

		public void Replace(string token, string tokenValue)
		{
			if (_text == null)
				throw new ApplicationException("The Text property must be set before tokens may be replaced");

			string tokenWithDelimiters = string.Format("||{0}||", token);
			_text = _text.Replace(tokenWithDelimiters, tokenValue);
		}
	}
}