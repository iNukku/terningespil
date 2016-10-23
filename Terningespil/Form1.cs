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
        const int max_rounds = 3;
        bool checkBoxesAreHidden = true;
        int[] thrownDices = new int[5];
        int numberOfTries = 1;
        int[] resultarray = new int[5];
        bool[] diceIsChosen = new bool[] { false, false, false, false, false };
        CheckBox[] the_check_boxes;
        Label[] diceLabels;
        Label[] chosendiesLabels;

        //Constructor
        public Form1()
        {
            InitializeComponent();

            the_check_boxes = new CheckBox[] {
                checkBox1, checkBox2, checkBox3, checkBox4, checkBox5 };
            diceLabels = new Label[] {
                dice_lbl_1, dice_lbl_2, dice_lbl_3, dice_lbl_4, dice_lbl_5 };
            chosendiesLabels = new Label[] {
                chosen_dice_lbl_1, chosen_dice_lbl_2, chosen_dice_lbl_3, chosen_dice_lbl_4, chosen_dice_lbl_5 };
            
            resetTable();

            player playerOne = new player();
            player playerTwo = new player();
            playerOne.name = "Player One";
            playerTwo.name = "Player Two";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (numberOfTries < max_rounds)
            {
                checkCheckboxMarks(the_check_boxes);
                setSecondaryLabels(chosendiesLabels, resultarray);
                rollDice();
                setPrimaryDiceLabels(diceLabels);

                if (checkBoxesAreHidden)
                {
                    showCheckboxes(the_check_boxes);
                    label2.Visible = true;
                }      
                          
                numberOfTries++;              
            }
            else
            {
                checkCheckboxMarks(the_check_boxes);
                rollDice();
                setPrimaryDiceLabels(diceLabels);
                updateResultarray();
                setSecondaryLabels(chosendiesLabels, resultarray);
                endRound();
            }
        }

        private void nextRoundButton_Click(object sender, EventArgs e)
        {
            resetTable();
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
        /// returnerer billede af terning svarende til den indsatte værdi
        /// </summary>
        /// <param name="theRoll"></param>
        /// <returns></returns>
        /// 

        private void setPrimaryDiceLabels(Label[] labelarray)
        {
            for (int i = 0; i < labelarray.Length; i++)
            {
                labelarray[i].Image = findImage(thrownDices[i]);
            }
        }

        private void displayPrimaryLabels(Label[] labelarray)
        {
            foreach (Label label in labelarray)
            {
                label.Visible = true;
            }
        }

        private void setPrimaryLabelsToBlank(Label[] labelarray)
        {
            foreach (Label label in labelarray)
            {
                label.Image = Properties.Resources.dice_blank;
            }
        }

        private void setSecondaryLabels(Label[] labelarray, int[] arrayOfResults)
        {
            for (int i = 0; i < labelarray.Length; i++)
            {
                if (arrayOfResults[i] != 0)
                {
                    labelarray[i].Image = findImage(arrayOfResults[i]);
                }
            }
        }

        private void hideSecondaryLabels(Label[] labelarray)
        {
            foreach (Label label in labelarray)
            {
                label.Visible = false;
            }
        }
                
        /// <summary>
        /// Returnerer billede, der passer til terningeværdi
        /// </summary>
        /// <param name="theRoll"></param>
        /// <returns></returns>
        private Image findImage(int theRoll)
        {
            Image dicePic;

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
        private void showCheckboxes(CheckBox[] checkboxArray)
        {
            foreach (CheckBox checkbox in checkboxArray)
            {
                checkbox.Visible = true;
            }
            checkBoxesAreHidden = false;
        }

        /// <summary>
        /// Skjuler alle checkbokse
        /// </summary>
        private void hideCheckboxes(CheckBox[] checkboxarray)
        {
            foreach (CheckBox checkbox in checkboxarray)
            {
                checkbox.Visible = false;
            }
            checkBoxesAreHidden = true;
        }

        private void setLabelImagesToBlank(Label[] labelarray)
        {
            foreach (Label label in labelarray)
            {
                label.Image = Properties.Resources.dice_blank;
            }
        }

        private void setDiceIsChosenToFalse()
        {
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
        private void checkCheckboxMarks(CheckBox[] checkboxarray)
        {
            for (int i = 0; i < checkboxarray.Length; i++)
            {
                if (checkboxarray[i].Checked)
                {
                    resultarray[i] = thrownDices[i];
                    chosendiesLabels[i].Visible = true;
                    checkboxarray[i].Visible = false;
                    diceLabels[i].Visible = false;
                    diceIsChosen[i] = true;
                    checkboxarray[i].Checked = false;
                }
            }
        }

        /// <summary>
        /// Afslutter runden
        /// </summary>
        private void endRound()
        {
            button1.Enabled = false;
            nextRoundButton.Visible = true;
            hideCheckboxes(the_check_boxes);
            setSecondaryLabels(chosendiesLabels, resultarray);
            MessageBox.Show("Your score was : " + resultarray.Sum().ToString());
        }

        /// <summary>
        /// resetter spillet
        /// </summary>
        private void resetTable()
        {
            displayPrimaryLabels(diceLabels);
            hideCheckboxes(the_check_boxes);
            hideSecondaryLabels(chosendiesLabels);
            setPrimaryLabelsToBlank(diceLabels);
            button1.Enabled = true;
            numberOfTries = 1;
            label2.Visible = false;
            nextRoundButton.Visible = false;
            setDiceIsChosenToFalse();
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
        private Dictionary<String, int> hands;
    }

    public class Dices
    {

    }
}
