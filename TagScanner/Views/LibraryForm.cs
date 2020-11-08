namespace TagScanner.Views
{
    using System.Windows.Forms;

    public partial class LibraryForm : Form
    {
        public LibraryForm()
        {
            InitializeComponent();

            var songs = new[] {
                @"C:\mp3gaintest\1987 - In My Tribe\01 - What's the Matter Here.mp3",
                @"C:\mp3gaintest\1987 - In My Tribe\02 - Hey Jack Kerouac.mp3",
                @"C:\mp3gaintest\1987 - In My Tribe\03 - Like the Weather.mp3",
                @"C:\mp3gaintest\1987 - In My Tribe\04 - Cherry Tree.mp3",
                @"C:\mp3gaintest\1987 - In My Tribe\05 - The Painted Desert.mp3",
                @"C:\mp3gaintest\1987 - In My Tribe\06 - Don't Talk.mp3",
                @"C:\mp3gaintest\1987 - In My Tribe\07 - Peace Train.mp3",
                @"C:\mp3gaintest\1987 - In My Tribe\08 - Gun Shy.mp3",
                @"C:\mp3gaintest\1987 - In My Tribe\09 - My Sister Rose.mp3",
                @"C:\mp3gaintest\1987 - In My Tribe\10 - A Campfire Song.mp3",
                @"C:\mp3gaintest\1987 - In My Tribe\11 - City of Angels.mp3",
                @"C:\mp3gaintest\1987 - In My Tribe\12 - Verdi Cries.mp3",
            };

            new Controllers.GainController().GetGain(songs);
        }
    }
}
