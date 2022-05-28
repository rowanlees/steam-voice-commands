using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SVC
{
    public partial class FormListOfInstalledGames : Form
    {
        String currentDirectory = Directory.GetCurrentDirectory();
        public FormListOfInstalledGames()
        {
            InitializeComponent();
            var gamesList = File.ReadAllLines(currentDirectory + "/gameslist.txt").OrderByDescending(item => item, StringComparer.Ordinal);
            
            
            String games = "";
            foreach (var entry in gamesList)
            {
                if(entry.Contains("Game Name:"))
                {
                    games = games.Insert(games.LastIndexOf(games), entry.TextAfter("Game Name: ") + "\r\n");
                }                
            }
            listOfGamesTextBox.Text = games;
        }

        private void listOfGamesTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
