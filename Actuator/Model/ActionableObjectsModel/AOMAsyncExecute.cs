using Actuator.Model.DeviceModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Actuator.Model.ActionableObjectsModel
{
    public class AOMAsyncExecute
    {
        public string DisplayName { get; set; } = "Async Execute";
        public string ShortText
        {
            get
            {
                return "Run Audio, Video or Exe Parallely";
            }
            set
            {
                ShortText = "Run Audio, Video or Exe Parallely";
            }
        }

        public AOMShellExecute FiletoExecute
        {
            get;
            set;
        }
        public async void ExecuteFile()
        {
            if (FiletoExecute.FileTypeValue == FileType.Audio)
            {
                SoundPlayer sp = new SoundPlayer(FiletoExecute.FileName);
                await Task.Run(() =>
                    sp.Play()
                );
            }
            else
            {
                await Task.Run(() =>
                    Process.Start(FiletoExecute.FileName)
                );
            }
        }
    }
}
