using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory_game1
{
    public partial class Form1 : Form
    {
        Button[][] game = new Button[6][];
        List<int> name_Pictures = new List<int>();
        Random r = new Random();
        Button card1 = new Button();
        Button card2 = new Button();
        bool flag = false;
        private Timer timer = new Timer();
        bool turnPlayer1 = true;
        bool turnPlayer2 = false;
        int sumPlayer1 = 0;
        int sumPlayer2 = 0;
        Label player1 = new Label();
        Label player2 = new Label();
        Label whoIsTurn = new Label();
        int x = 100;
        int y = 150;

        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(1000, 900);
            timer.Interval = 3000;
            timer.Tick += timerTick;
            whoIsTurn.Size = new Size(200, 50);
            whoIsTurn.Text = "Player one";
            whoIsTurn.Font = new Font("serif", 20);
            whoIsTurn.Location = new Point(300, 0);
            this.Controls.Add(whoIsTurn);

            player1.Text = "Player one score:";
            player1.Location = new Point(650,70);
            player1.Size = new Size(300, 200);
            player1.Font = new Font("serif", 20);
            this.Controls.Add(player1);

            player2.Text = "Player two score:";
            player2.Location = new Point(650, 370);
            player2.Size = new Size(300, 200);
            player2.Font = new Font("serif", 20);
            this.Controls.Add(player2);

            for (int i = 0; i < 15; i++)
            {
                name_Pictures.Add(i + 1);
                name_Pictures.Add(i + 1);
            }
            
            for (int i = 0; i < game.Length; i++)
            {
                game[i] = new Button[5];
                for (int j = 0; j < game[i].Length; j++)
                {
                    int indexRandom = r.Next(name_Pictures.Count);                   
                    string rndText = name_Pictures[indexRandom].ToString();
                    name_Pictures.RemoveAt(indexRandom);
                    game[i][j] = new Button();
                    game[i][j].Size = new Size(x, y);
                    game[i][j].Name = rndText;
                    game[i][j].Location = new Point(i * x + 30, j * y + 50);
                    game[i][j].Click += cardOne;
                    this.Controls.Add(game[i][j]);                    
                }
            }
        }

        private void timerTick(object sender, EventArgs e)
        {
            turnPlayer1 = !turnPlayer1;
            turnPlayer2 = !turnPlayer2;

            if (card1.Name == card2.Name)
            {
                turnPlayer1 = !turnPlayer1;
                turnPlayer2 = !turnPlayer2;
                if (turnPlayer1)
                {
                    sumPlayer1 += 5;
                    player1.Text = $"Player one score: {sumPlayer1}";
                    turnPlayer1 = true;
                }
                if(turnPlayer2)
                {
                    sumPlayer2 += 5;
                    player2.Text = $"Player two score: {sumPlayer2}";
                    turnPlayer2 = true;
                }
                card1.Dispose();
                card2.Dispose();
            }
            else
            {                
                card1.BackgroundImage = null;
                card2.BackgroundImage = null;
            }

            timer.Stop();

            for (int i = 0; i < game.Length; i++)
            {
                for (int j = 0; j < game[i].Length; j++)
                {
                    game[i][j].Enabled = true;
                }
            }

            if (turnPlayer1)
            {
                whoIsTurn.Text = "Player one";
            }
            else
            {
                whoIsTurn.Text = "Player two";
            }

        }

        private void cardOne(object sender, EventArgs e)
        {
            if (!flag)
            {
                card1 = sender as Button;
                card1.BackgroundImage = Image.FromFile("../../" + card1.Name + ".png");
                card1.BackgroundImageLayout = ImageLayout.Zoom;
                card1.Enabled = false;
                flag = true;
            }
            else
            {
                card2 = sender as Button;               
                card2.BackgroundImage = Image.FromFile("../../" + card2.Name + ".png");
                card2.BackgroundImageLayout = ImageLayout.Zoom;
                flag = false;
                for (int i = 0; i < game.Length; i++)
                {
                    for (int j = 0; j < game[i].Length; j++)
                    {
                        game[i][j].Enabled = false;
                    }
                }
                timer.Start();
            }
        }

    }
}
