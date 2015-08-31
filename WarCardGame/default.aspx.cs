using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WarCardGame
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void playButton_Click(object sender, EventArgs e)
        {
            Player player1 = new Player() { Name = player1TextBox.Text };
            Player player2 = new Player() { Name = player2TextBox.Text };
            Game game = new Game(player1, player2);
            game.Play();
            resultLabel.Text += game.DisplayResults();
        }
    }
}