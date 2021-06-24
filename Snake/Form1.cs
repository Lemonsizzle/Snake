using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        System.Timers.Timer gameTime = new System.Timers.Timer();

        int cellSize = 20, space = 4, gridX = 20, gridY = 15;
        PictureBox[,] grid;

        int direction = 2;
        List<int> snakeX, snakeY;
        List<int> preSnakeX, preSnakeY;

        int foodX, foodY;
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            Shown += gridGen;
        }

        private void gridGen(object sender, EventArgs e)
        {
            grid = new PictureBox[gridX,gridY];
            for (int j = 0; j < gridY; j++)
            {
                for(int i = 0; i < gridX; i++)
                {
                    grid[i, j] = new PictureBox();
                    grid[i, j].Size = new Size(cellSize, cellSize);
                    grid[i, j].Location = new Point((cellSize+space)*i+space,(cellSize + space) * j + space);
                    grid[i, j].BackColor = Color.Gray;

                    this.Controls.Add(grid[i, j]);
                }
            }
            this.Size = new Size((cellSize + space) * gridX + 2 * space+10, (cellSize + space) * gridY + 2 * space+40);

            snakeGen();
        }

        private void snakeGen()
        {
            snakeX = new List<int>();
            snakeX.Add(gridX / 2);
            snakeX.Add(snakeX[0] + 1);
            snakeY = new List<int>();
            snakeY.Add(gridY / 2);
            snakeY.Add(snakeY[0]);

            grid[snakeX[0], snakeY[0]].BackColor = Color.White;
            grid[snakeX[1], snakeY[1]].BackColor = Color.White;

            foodGen();
        }

        private void foodGen()
        {
            foodX = rnd.Next(gridX);
            foodY = rnd.Next(gridY);
            grid[foodX, foodY].BackColor = Color.Green;

            
            gameTime.Elapsed += Tick;
            gameTime.AutoReset = false;
            gameTime.Interval = 500;
            gameTime.Start();
            
        }

        private void downPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    direction = 0;
                    break;

                case Keys.Down:
                    direction = 1;
                    break;

                case Keys.Left:
                    direction = 2;
                    break;

                case Keys.Right:
                    direction = 3;
                    break;
            }
        }

        private void upPress(object sender, KeyEventArgs e)
        {

        }

        private void moveUp()
        {
            if (snakeY[0] - 1 < 0)
            {
                MessageBox.Show("Game Over");
                gameTime.Stop();
            }
            else
            {
                snakeY[0]--;
                /*
                grid[snakeX[0], snakeY[0]].BackColor = Color.White;
                if (snakeY.Count == 1)
                {
                    grid[snakeX[0], snakeY[0] + 1].BackColor = Color.Gray;
                }
                else
                {
                    grid[preSnakeX[snakeX.Count - 1], preSnakeY[snakeY.Count - 1]].BackColor = Color.Gray;
                }
                */
            }
        }

        private void moveDown()
        {
            if (snakeY[0] + 1 == gridY)
            {
                MessageBox.Show("Game Over");
                gameTime.Stop();
            }
            else
            {
                snakeY[0]++;
                /*
                grid[snakeX[0], snakeY[0]].BackColor = Color.White;
                if (snakeY.Count == 1)
                {
                    grid[snakeX[0], snakeY[0] - 1].BackColor = Color.Gray;
                }
                else
                {
                    grid[preSnakeX[snakeX.Count - 1], preSnakeY[snakeY.Count - 1]].BackColor = Color.Gray;
                }
                */
            }
        }

        private void moveLeft()
        {
            if (snakeX[0] - 1 < 0)
            {
                MessageBox.Show("Game Over");
                gameTime.Stop();
            }
            else
            {
                snakeX[0]--;
                /*
                grid[snakeX[0], snakeY[0]].BackColor = Color.White;
                if (snakeX.Count == 1)
                {
                    grid[snakeX[0] + 1, snakeY[0]].BackColor = Color.Gray;
                }
                else
                {
                    grid[preSnakeX[snakeX.Count - 1], preSnakeY[snakeY.Count - 1]].BackColor = Color.Gray;
                }
                */
            }
        }

        private void moveRight()
        {
            if (snakeX[0] + 1 == gridX)
            {
                MessageBox.Show("Game Over");
                gameTime.Stop();
            }
            else
            {
                snakeX[0]++;
                grid[snakeX[0], snakeY[0]].BackColor = Color.White;
                /*
                if(snakeX.Count == 1)
                {
                    grid[snakeX[0] - 1, snakeY[0]].BackColor = Color.Gray;
                }
                else
                {
                    grid[preSnakeX[snakeX.Count-1], preSnakeY[snakeY.Count-1]].BackColor = Color.Gray;
                }
                */
            }
        }

        private void eat()
        {
            foodGen();
            if(snakeX.Count == 1)
            {
                switch (direction)
                {
                    case 0:
                        snakeX.Add(snakeX[0]);
                        snakeY.Add(snakeY[0] + 1);
                        break;

                    case 1:
                        snakeX.Add(snakeX[0]);
                        snakeY.Add(snakeY[0] - 1);
                        break;

                    case 2:
                        snakeX.Add(snakeX[0] + 1);
                        snakeY.Add(snakeY[0]);
                        break;

                    case 3:
                        snakeX.Add(snakeX[0] - 1);
                        snakeY.Add(snakeY[0]);
                        break;
                }
                
            }
            else
            {
                snakeX.Add(snakeX[snakeX.Count - 1] - snakeX[snakeX.Count - 2] + snakeX[snakeX.Count - 1]);
                snakeY.Add(snakeY[snakeY.Count - 1] - snakeY[snakeY.Count - 2] + snakeY[snakeY.Count - 1]);
            }
            grid[snakeX[snakeX.Count-1], snakeY[snakeY.Count-1]].BackColor = Color.White;
        }

        private void Tick(object sender, EventArgs e)
        {
            int holdlastX = snakeX[0];
            int holdlastY = snakeY[0];
            preSnakeX = snakeX;
            preSnakeY = snakeY;
            switch (direction)
            {
                case 0:
                    moveUp();
                    break;

                case 1:
                    moveDown();
                    break;

                case 2:
                    moveLeft();
                    break;

                case 3:
                    moveRight();
                    break;
            }

            if ((snakeX[0] == foodX) && (snakeY[0] == foodY))
            {
                eat();
            }

            for (int id = 1; id < snakeX.Count; id++)
            {
                int holdcurX = snakeX[id];
                int holdcurY = snakeY[id];
                snakeX[id] = holdlastX;
                snakeY[id] = holdlastY;
                holdlastX = holdcurX;
                holdlastY = holdcurY;

                /*
                if ((snakeX[0] == snakeX[id]) && (snakeY[0] == snakeY[id]))
                {
                    //MessageBox.Show("Game Over");
                    MessageBox.Show("check");
                    gameTime.Stop();
                }
                */
            }

            for(int j = 0; j < gridY; j++)
            {
                for(int i = 0; i < gridX; i++)
                {
                    for(int id = 0; id < snakeX.Count; id++)
                    {
                        if ((i == snakeX[id]) && (j == snakeY[id]))
                        {
                            grid[i, j].BackColor = Color.White;
                        }
                        else
                        {
                            grid[i, j].BackColor = Color.Gray;
                        }
                    }
                }
            }

            Thread.Sleep(100);
            gameTime.Start();
        }
    }
}
