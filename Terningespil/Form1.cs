using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Terningespil
{
    public partial class Form1 : Form
    {
        Random rng = new Random();
        bool checkBoxesAreHidden = true;
        int[] thrownDices = new int[5];
        int numberOfTries = 0;
        //resultarray skal høre til den enkelte player-class, men den skal først laves!
        int[] resultarray = new int[5];
        bool[] diceIsChosen = new bool[] { false, false, false, false, false };

        //Constructor
        public Form1()
        {
            InitializeComponent();
            chosen_dice_lbl_1.Visible = false;
            chosen_dice_lbl_2.Visible = false;
            chosen_dice_lbl_3.Visible = false;
            chosen_dice_lbl_4.Visible = false;
            chosen_dice_lbl_5.Visible = false;
            label2.Visible = false;

            player playerOne = new player();
            player playerTwo = new player();
            playerOne.name = "Player One";
            playerTwo.name = "Player Two";

            hideCheckboxes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (numberOfTries < 2)
            {
                checkCheckboxMarks();
                setSecondaryLabels();
                rollDice();
                setPrimaryDiceLabels();

                if (checkBoxesAreHidden)
                {
                    showCheckboxes();
                    label2.Visible = true;
                }      
                          
                numberOfTries++;              
            }
            else
            {
                checkCheckboxMarks();
                updateResultarray();
                //Problem: Secondarylabels sættes ikke
                setSecondaryLabels();
                endRound();
            }
        }
        /// <summary>
        /// fylder thrownDices-array med tilfældige terningekast
        /// </summary>
        private void rollDice()
        {
            for (int i = 0; i < thrownDices.Length; i++)
            {
                if (diceIsChosen[i] == false)
                {
                    thrownDices[i] = rng.Next(1, 7);
                }
                else
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// returnerer billede af terning svarende til den parsede værdi
        /// </summary>
        /// <param name="theRoll"></param>
        /// <returns></returns>
        /// 

        private void setPrimaryDiceLabels()
        {
            dice_lbl_1.Image = findImage(thrownDices[0]);
            dice_lbl_2.Image = findImage(thrownDices[1]);
            dice_lbl_3.Image = findImage(thrownDices[2]);
            dice_lbl_4.Image = findImage(thrownDices[3]);
            dice_lbl_5.Image = findImage(thrownDices[4]);
        }

        private void setSecondaryLabels()
        {
            if (resultarray[0] != 0)
            {
                chosen_dice_lbl_1.Image = findImage(resultarray[0]);
            }
            if (resultarray[1] != 0)
            {
                chosen_dice_lbl_2.Image = findImage(resultarray[1]);
            }
            if (resultarray[2] != 0)
            {
                chosen_dice_lbl_3.Image = findImage(resultarray[2]);
            }
            if (resultarray[3] != 0)
            {
                chosen_dice_lbl_4.Image = findImage(resultarray[3]);
            }
            if (resultarray[4] != 0)
            {
                chosen_dice_lbl_5.Image = findImage(resultarray[4]);
            }
        }
        
        private Image findImage(int theRoll)
        {
            Image dicePic = Properties.Resources.dice_blank;

            switch (theRoll)
            {
                case 1:
                    dicePic = Properties.Resources.dice_1;
                    break;
                case 2:
                    dicePic = Properties.Resources.dice_2;
                    break;
                case 3:
                    dicePic = Properties.Resources.dice_3;
                    break;
                case 4:
                    dicePic = Properties.Resources.dice_4;
                    break;
                case 5:
                    dicePic = Properties.Resources.dice_5;
                    break;
                case 6:
                    dicePic = Properties.Resources.dice_6;
                    break;
                default:
                    dicePic = Properties.Resources.dice_blank;
                    break;
            }
            return dicePic;
        }

        /// <summary>
        /// Tjekker om alle checkbokse er synlige
        /// </summary>
        /// <returns></returns>
        private bool checkboxIsVisible()
        {
            bool isTrue = false;
            if (checkBox1.Visible && checkBox2.Visible && checkBox3.Visible && checkBox4.Visible && checkBox5.Visible)
            {
                isTrue = true;
            }
            return isTrue;
        }
        /// <summary>
        /// sætter alle checkbokse til synlig
        /// </summary>
        private void showCheckboxes()
        {
            checkBox1.Visible = true;
            checkBox2.Visible = true;
            checkBox3.Visible = true;
            checkBox4.Visible = true;
            checkBox5.Visible = true;

            checkBoxesAreHidden = false;
        }
        /// <summary>
        /// Skjuler alle checkbokse
        /// </summary>
        private void hideCheckboxes()
        {
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            checkBox3.Visible = false;
            checkBox4.Visible = false;
            checkBox5.Visible = false;

            checkBoxesAreHidden = true;
        }

        /// <summary>
        /// sætter antal forsøg til 0, skjuler tjekbokse og nulstiller terningerne i formen
        /// </summary>
        private void resetTable()
        {
            hideCheckboxes();
            label2.Visible = false;
            numberOfTries = 0;

            dice_lbl_1.Image = Properties.Resources.dice_blank;
            dice_lbl_2.Image = Properties.Resources.dice_blank;
            dice_lbl_3.Image = Properties.Resources.dice_blank;
            dice_lbl_4.Image = Properties.Resources.dice_blank;
            dice_lbl_5.Image = Properties.Resources.dice_blank;

            for (int i = 0; i < diceIsChosen.Length; i++)
            {
                diceIsChosen[i] = false;
            }

        }

        private void updateResultarray()
        {
            for (int i = 0; i < resultarray.Length; i++)
            {
                if (resultarray[i] == 0)
                {
                    resultarray[i] = thrownDices[i];
                }
            }
        }
        /// <summary>
        /// Checker hvilke terninger spilleren vælger at gemme, og gemmer dem i resultarray
        /// </summary>
        private void checkCheckboxMarks()
        {
            if (checkBox1.Checked)
            {
                resultarray[0] = thrownDices[0];
                chosen_dice_lbl_1.Visible = true;
                checkBox1.Visible = false;
                dice_lbl_1.Visible = false;
                diceIsChosen[0] = true;
                checkBox1.Checked = false;
            }
            if (checkBox2.Checked)
            {
                resultarray[1] = thrownDices[1];
                chosen_dice_lbl_2.Visible = true;
                checkBox2.Visible = false;
                dice_lbl_2.Visible = false;
                diceIsChosen[1] = true;
                checkBox2.Checked = false;
            }
            if (checkBox3.Checked)
            {
                resultarray[2] = thrownDices[2];
                chosen_dice_lbl_3.Visible = true;
                checkBox3.Visible = false;
                dice_lbl_3.Visible = false;
                diceIsChosen[2] = true;
                checkBox3.Checked = false;
            }
            if (checkBox4.Checked)
            {
                resultarray[3] = thrownDices[3];
                chosen_dice_lbl_4.Visible = true;
                checkBox4.Visible = false;
                dice_lbl_4.Visible = false;
                diceIsChosen[3] = true;
                checkBox4.Checked = false;
            }
            if (checkBox5.Checked)
            {
                resultarray[4] = thrownDices[4];
                chosen_dice_lbl_5.Visible = true;
                checkBox5.Visible = false;
                dice_lbl_5.Visible = false;
                diceIsChosen[4] = true;
                checkBox5.Checked = false;
            }
        }

        private void endRound()
        {
            button1.Enabled = false;
            hideCheckboxes();
            setSecondaryLabels();
            MessageBox.Show("Your score was : " + resultarray.Sum().ToString());
        }

    }

    public class player
    {
        public string name;
        public int score = 0;
        public bool hasTurn; 

    }

    public class Game
    {

    }
}
