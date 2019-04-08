using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.ActionableObjectsModel
{
	public class AOMShellExecute
	{
        public string DisplayName { get; set; } = "Shell Execute";
        public string ShortText
		{
			get
			{
				return "Run Audio, Video and Exe";
			}
			set
			{
				ShortText = "Run Audio, Video and Exe";
			}
		}
		public string FileName
		{
			get;
			set;
		}
		public FileType FileTypeValue
		{
			get;
			set;
		}
		public void ExecuteFile()
		{
			if(FileTypeValue == FileType.Audio)
			{
				SoundPlayer sp = new SoundPlayer(FileName);
				sp.Play();
			}
			else 
			{
				Process.Start(FileName);
			}
		}
	}
	public enum FileType
	{
		Audio,
		Video,
		Executable
	}
}
