namespace TagScanner.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class GainController
    {
        public static string MP3GainPath { get; set; } = @"C:\Program Files (x86)\MP3Gain\mp3gain.exe";

        public GainController() { }

        public void GetGain(IEnumerable<string> mp3Paths)
        {
            var paths = mp3Paths.Select(s => $"\"{s}\"").Aggregate((s, t) => $"{s} {t}");
            var process = new Process();
            var startInfo = process.StartInfo;
            startInfo.FileName = MP3GainPath;
            //startInfo.Arguments = $"/o/x/ss {paths}";
            startInfo.Arguments = $"/o/ss {paths}";
            startInfo.CreateNoWindow = true;
            startInfo.ErrorDialog = true;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            process.OutputDataReceived += OutputDataReceived;
            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();
            process.Close();
            process.OutputDataReceived -= OutputDataReceived;
        }

        private void OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                var info = e.Data;
                if (string.IsNullOrWhiteSpace(info))
                    return;
                var g = new GainInfo(info);
                Debug.WriteLine(g);
            }
            catch (FormatException)
            {

            }
        }

        public class GainInfo
        {
            public string Filename { get; set; }
            public int MP3Gain { get; set; }
            public float DbGain { get; set; }
            public double AmplitudeMax { get; set; }
            public int GlobalGainMin { get; set; }
            public int GlobalGainMax { get; set; }

            public GainInfo(string info)
            {
                var infos = info.TrimEnd().Split('\t');
                Filename = infos[0];
                MP3Gain = int.Parse(infos[1]);
                DbGain = float.Parse(infos[2]);
                AmplitudeMax = double.Parse(infos[3]);
                GlobalGainMax = int.Parse(infos[4]);
                GlobalGainMin = int.Parse(infos[5]);
            }

            public override string ToString() =>
                $"{Filename} MP3Gain={MP3Gain} DbGain={DbGain} AmplitudeMax={AmplitudeMax} GlobalGainMin={GlobalGainMin} Max={GlobalGainMax}";
        }

        #region Documentation

        /*
            This controller class makes extensive use of MP3Gain.exe version 1.4.6:
                copyright(c) 2001-2004 by Glen Sawyer, uses mpglib, which can be found at http://www.mpg123.de.

            Usage: c:\Program Files (x86)\MP3Gain\mp3gain [options] <infile> [<infile 2> ...]

            options:
            /? or /h - show this message
            /a - apply Album gain automatically (files are all from the same album: a single gain change is applied to all files, 
                  so their loudness relative to each other remains unchanged, but the average album loudness is normalized)
            /c - ignore clipping warning when applying gain
            /d<n> - modify suggested dB gain by floating-point n
            /f - Force mp3gain to assume input file is an MPEG 2 Layer III file (i.e. don't check for mis-named Layer I or Layer II files)
            /g<i>  - apply gain i to mp3 without doing any analysis
            /k - automatically lower Track/Album gain to not clip audio
            /l0<i> - apply gain i to channel 0 (left channel) of mp3 without doing any analysis (ONLY works for STEREO mp3s, not Joint Stereo mp3s)
            /l1<i> - apply gain i to channel 1 (right channel) of mp3
            /m<i> - modify suggested MP3 gain by integer i
            /o - output is a database-friendly tab-delimited list
            /p - Preserve original file timestamp
            /q - Quiet mode: no status messages
            /r - apply Track gain automatically (all files set to equal loudness)
            /sc - only check stored tag info (no other processing)
            /sd - delete stored tag info (no other processing)
            /sr - force re-calculation (do not read tag info)
            /ss - skip (ignore) stored tag info (do not read or write tags)
            /t - mp3gain writes modified mp3 to temp file, then deletes original instead of modifying bytes in original file
            /u - undo changes made by mp3gain (based on stored tag info)
            /v - show version number
            /w - "wrap" gain change if gain+change > 255 or gain+change < 0 (use "/? wrap" switch for a complete explanation)
            /x - Only find max. amplitude of mp3

            If you specify /r and /a, only the second one will work
            If you do not specify /c, the program will stop and ask before applying gain change to a file that might clip
         */

        #endregion
    }
}