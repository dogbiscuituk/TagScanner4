namespace TagScanner.Controllers
{
    using System.Diagnostics;

    /*
        This controller class makes extensive use of MP3Gain.exe version 1.4.6:
            copyright(c) 2001-2004 by Glen Sawyer, uses mpglib, which can be found at http://www.mpg123.de.

        Usage: c:\Program Files (x86)\MP3Gain\mp3gain [options] <infile> [<infile 2> ...]

        options:
        /v - show version number
        /g <i>  - apply gain i to mp3 without doing any analysis
        /l 0 <i> - apply gain i to channel 0 (left channel) of mp3 without doing any analysis (ONLY works for STEREO mp3s, not Joint Stereo mp3s)
        /l 1 <i> - apply gain i to channel 1 (right channel) of mp3
        /r - apply Track gain automatically (all files set to equal loudness)
        /k - automatically lower Track/Album gain to not clip audio
        /a - apply Album gain automatically (files are all from the same
            album: a single gain change is applied to all files, so their loudness relative to each other remains unchanged, but the average album loudness is normalized)
        /m <i> - modify suggested MP3 gain by integer i
        /d <n> - modify suggested dB gain by floating-point n
        /c - ignore clipping warning when applying gain
        /o - output is a database-friendly tab-delimited list
        /t - mp3gain writes modified mp3 to temp file, then deletes original instead of modifying bytes in original file
        /q - Quiet mode: no status messages
        /p - Preserve original file timestamp
        /x - Only find max. amplitude of mp3
        /f - Force mp3gain to assume input file is an MPEG 2 Layer III file (i.e. don't check for mis-named Layer I or Layer II files)
        /? or /h - show this message
        /s c - only check stored tag info (no other processing)
        /s d - delete stored tag info (no other processing)
        /s s - skip (ignore) stored tag info (do not read or write tags)
        /s r - force re-calculation (do not read tag info)
        /u - undo changes made by mp3gain (based on stored tag info)
        /w - "wrap" gain change if gain+change > 255 or gain+change < 0 (use "/? wrap" switch for a complete explanation)

        If you specify /r and /a, only the second one will work
        If you do not specify /c, the program will stop and ask before applying gain change to a file that might clip
     */

    public class GainController
    {
        public enum OutputFormat
        {
            FreeText,
            TabDelimited,
        }

        public static string MP3GainPath { get; set; } = @"C:\Program Files (x86)\MP3Gain\mp3gain.exe";

        public GainController() { }

        public OutputFormat Format { get; set; } = OutputFormat.TabDelimited;

        public void GetGain(string mp3Path)
        {
            mp3Path = @"""C:\mp3gaintest\1987 - In My Tribe\01 - What's the Matter Here.mp3"" ""C:\mp3gaintest\1987 - In My Tribe\04 - Cherry Tree.mp3""";
            //mp3Path = @"""C:\mp3gaintest\1987 - In My Tribe\01 - What's the Matter Here.mp3"" ""C:\mp3gaintest\1987 - In My Tribe\04 - Cherry Tree.mp3""";
            //mp3Path = @"""C:\mp3gaintest\1987 - In My Tribe\01 - What's the Matter Here.mp3""";

            string
                FormatSwitch = Format == OutputFormat.FreeText ? string.Empty : "/o";

            var processInfo = new ProcessStartInfo
            {
                FileName = MP3GainPath,
                Arguments = $"{FormatSwitch}/x {mp3Path}",
                CreateNoWindow = true,
                ErrorDialog = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
            };

            var p = new Process();
            var s = p.StartInfo;
            s.FileName = MP3GainPath;
            s.Arguments = $"{FormatSwitch}/x {mp3Path}";
            s.CreateNoWindow = true;
            s.ErrorDialog = true;
            s.RedirectStandardError = true;
            s.RedirectStandardOutput = true;
            s.UseShellExecute = false;
            p.OutputDataReceived += OutputDataReceived;
            p.ErrorDataReceived += ErrorDataReceived;
            p.Start();
            p.BeginOutputReadLine();
            //var output = p.StandardOutput.ReadToEnd();
            //var error = p.StandardError.ReadToEnd();
            p.WaitForExit();
            p.Close();
            p.ErrorDataReceived -= ErrorDataReceived;
            p.OutputDataReceived -= OutputDataReceived;

            /* 
            The following are sample process outputs, in both string formats, for:
                First track (no clipping);
                Second track (warning for clipping);
                Both tracks;
                Error in path.

            StringFormat.FreeText:
            -------------------------
            
            "C:\\mp3gaintest\\1987 - In My Tribe\\01 - What's the Matter Here.mp3\r\n
            Recommended \"Track\" dB change: 1.760000\r\n
            Recommended \"Track\" mp3 gain change: 1\r\n
            Max PCM sample at current gain: 24142.217216\r\n
            Max mp3 global gain field: 210\r\n
            Min mp3 global gain field: 94\r\n
            \r\n
            \r\n
            Recommended \"Album\" dB change for all files: 1.760000\r\n
            Recommended \"Album\" mp3 gain change for all files: 1\r\n"

            "C:\\mp3gaintest\\1987 - In My Tribe\\04 - Cherry Tree.mp3\r\n
            Recommended \"Track\" dB change: 1.850000\r\n
            Recommended \"Track\" mp3 gain change: 1\r\n
            WARNING: some clipping may occur with this gain change!\r\n
            Max PCM sample at current gain: 29305.602048\r\n
            Max mp3 global gain field: 183\r\n
            Min mp3 global gain field: 121\r\n
            \r\n
            \r\n
            Recommended \"Album\" dB change for all files: 1.850000\r\n
            Recommended \"Album\" mp3 gain change for all files: 1\r\n
            WARNING: with this global gain change, some clipping may occur in file C:\\mp3gaintest\\1987 - In My Tribe\\04 - Cherry Tree.mp3\r\n"

            "C:\\mp3gaintest\\1987 - In My Tribe\\01 - What's the Matter Here.mp3\r\n
            Recommended \"Track\" dB change: 1.760000\r\n
            Recommended \"Track\" mp3 gain change: 1\r\n
            Max PCM sample at current gain: 24142.217216\r\n
            Max mp3 global gain field: 210\r\n
            Min mp3 global gain field: 94\r\n
            \r\n
            C:\\mp3gaintest\\1987 - In My Tribe\\04 - Cherry Tree.mp3\r\n
            Recommended \"Track\" dB change: 1.850000\r\n
            Recommended \"Track\" mp3 gain change: 1\r\n
            WARNING: some clipping may occur with this gain change!\r\n
            Max PCM sample at current gain: 29305.602048\r\n
            Max mp3 global gain field: 183\r\n
            Min mp3 global gain field: 121\r\n
            \r\n
            \r\n
            Recommended \"Album\" dB change for all files: 1.800000\r\n
            Recommended \"Album\" mp3 gain change for all files: 1\r\n
            WARNING: with this global gain change, some clipping may occur in file C:\\mp3gaintest\\1987 - In My Tribe\\04 - Cherry Tree.mp3\r\n"

            "C:\\mp3gaintest\\1987 - In My Trube\\01 - What's the Matter Here.mp3\r\n
            Can't open C:\\mp3gaintest\\1987 - In My Trube\\01 - What's the Matter Here.mp3 for reading\r\n"

            StringFormat.TabDelimited:
            -------------------------------

            "File\t                                                                                                   MP3 gain\t  dB gain\t    Max Amplitude\t  Max global_gain\t  Min global_gain\r\n
            C:\\mp3gaintest\\1987 - In My Tribe\\01 - What's the Matter Here.mp3\t  1\t              1.760000\t  24142.217216\t    210\t                     94\r\n\
            "Album\"\t                                                                                           1\t               1.800000\t  24142.217216\t   210\t                      94\r\n"

            "File\t                                                                                                   MP3 gain\t  dB gain\t    Max Amplitude\t  Max global_gain\t  Min global_gain\r\n
            C:\\mp3gaintest\\1987 - In My Tribe\\04 - Cherry Tree.mp3\t                   1\t              1.850000\t  29305.602048\t     183\t                    121\r\n
            \"Album\"\t                                                                                          1\t              1.850000\t  29305.602048\t     183\t                    121\r\n"

            "File\t                                                                                                   MP3 gain\t  dB gain\t    Max Amplitude\t  Max global_gain\t  Min global_gain\r\n
            C:\\mp3gaintest\\1987 - In My Tribe\\01 - What's the Matter Here.mp3\t  1\t              1.760000\t  24142.226249\t    210\t                     94\r\n
            C:\\mp3gaintest\\1987 - In My Tribe\\04 - Cherry Tree.mp3\t                   1\t              1.850000\t  29305.598260\t    183\t                     121\r\n
            \"Album\"\t                                                                                          1\t              1.800000\t  29305.602048\t    210\t                     94\r\n"

            "File\t                                                                                                  MP3 gain\t  dB gain\t    Max Amplitude\t  Max global_gain\t  Min global_gain\r\n
            Can't open C:\\mp3gaintest\\1987 - In My Trube\\04 - Cherry Tree.mp3 for reading\r\n"

            */
        }

        private void ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            return;
        }

        private void OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            return;
        }
    }
}
