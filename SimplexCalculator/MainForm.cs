using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System;
using Fractions;

namespace SimplexCalculator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();            
        }

        int constraintsCount = 0;
        int variablesCount = 0;

        int rowHeight = 22;
        private void FillConstraintsGrid()
        {
            constraintsGridView.Rows.Clear();
            constraintsGridView.ColumnCount = variablesCount + 2;
            constraintsGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            constraintsGridView.RowHeadersVisible = false;
            constraintsGridView.AllowUserToAddRows = false;
            constraintsGridView.AllowUserToDeleteRows = false;
            for (int i = 0; i < variablesCount + 2; i++)
            {
                constraintsGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (i < variablesCount)
                {
                    constraintsGridView.Columns[i].Name = "x" + (i + 1).ToString();
                }
                else if (i == variablesCount)
                {
                    constraintsGridView.Columns[i].Name = "";
                }
            }

            for (int i = 0; i < constraintsCount; i++)
            {
                string[] row = new string[variablesCount + 2];
                DataGridViewComboBoxCell comboBoxCell = new DataGridViewComboBoxCell();
                comboBoxCell.Items.AddRange("≤", "≥", "=");
                constraintsGridView.Rows.Add(row);
                constraintsGridView.Rows[i].Cells[variablesCount] = comboBoxCell;
            }
            constraintsGridView.Rows[0].Height = rowHeight;
        }

        private string GetProgramSign(string gridSign)
        {
            if (gridSign == "≤")
                return "<=";
            else if (gridSign == "≥")
                return ">=";
            return "=";
        }

        private void FillFunctionGrid()
        {
            functionGridView.Rows.Clear();
            functionGridView.ColumnCount = variablesCount + 1;
            functionGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            functionGridView.RowHeadersVisible = false;
            functionGridView.AllowUserToAddRows = false;
            functionGridView.AllowUserToDeleteRows = false;
            for (int i = 0; i < variablesCount + 1; i++)
            {
                functionGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                functionGridView.Columns[i].Visible = true;
                if (i < variablesCount)
                {
                    functionGridView.Columns[i].Name = "x" + (i + 1).ToString();
                }
                else
                {
                    functionGridView.Columns[i].Name = "C";
                    functionGridView.Columns[i].Visible = false;
                }

            }
            string[] row = new string[variablesCount + 2];
            functionGridView.Rows.Add(row);
            functionGridView.Rows[0].Height = rowHeight;
        }

        private bool isExtrMax;
        private void Calculate()
        {
            Constraint[] constraints = new Constraint[constraintsCount];
            for (int i = 0; i < constraintsCount; i++)
            {
                Fraction[] variables = new Fraction[variablesCount];
                Fraction b = Fraction.FromString(Convert.ToString(constraintsGridView.Rows[i].Cells[variablesCount + 1].Value));
                string sign = Convert.ToString(constraintsGridView.Rows[i].Cells[variablesCount].Value);
                for (int j = 0; j < variablesCount; j++)
                {
                    try
                    {
                        variables[j] = Fraction.FromString(Convert.ToString(constraintsGridView.Rows[i].Cells[j].Value));
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show($"Невірний формат в рядку {i+1}, стовпець {j+1}. " +
                            $"Будь ласка, введіть числове значення.", "Format Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                         return;
                    }
                }
                constraints[i] = new Constraint(variables, b, GetProgramSign(sign));
            }
            Fraction[] functionVariables = new Fraction[variablesCount];
            for (int i = 0; i < variablesCount; i++)
            {
                functionVariables[i] = Fraction.FromDouble(Convert.ToDouble(functionGridView.Rows[0].Cells[i].Value));
            }
            Fraction c = Fraction.FromDouble(Convert.ToDouble(functionGridView.Rows[0].Cells[variablesCount].Value));

            isExtrMax = extrComboBox.SelectedIndex == 0;

            Function function = new Function(functionVariables, c, isExtrMax);

            SimplexCalculator simplex = new SimplexCalculator(function, constraints);

            Tuple<List<SimplexSnap>, SimplexResult> result = simplex.GetResult();

            switch (result.Item2)
            {
                case SimplexResult.Found:
                    resultsLbl.Text = "Оптимальне рішення знайдено.";
                    break;
                case SimplexResult.Unbounded:
                    resultsLbl.Text = "Задача не може бути розв'язана.";
                    break;
                case SimplexResult.NotYetFound:
                    resultsLbl.Text = "Алгоритм пройшов 45 циклів та не знайшов оптимального розв'язку.";
                    break;
            }

            ShowResultsGrid(result.Item1);
        }

        private void ShowResultsGrid(List<SimplexSnap> snaps)
        {
            resultsGridView.Rows.Clear();
            resultsGridView.ColumnCount = snaps.First().Matrix.Length + 3;
            resultsGridView.RowHeadersVisible = false;
            resultsGridView.ColumnHeadersVisible = false;
            resultsGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            resultsGridView.AllowUserToAddRows = false;
            resultsGridView.AllowUserToDeleteRows = false;
            resultsGridView.ReadOnly = true;

            for (int i = 0; i < snaps.First().Matrix.Length + 3; i++)
            {
                resultsGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            foreach (SimplexSnap snap in snaps)
            {
                string[] firstRow = new string[snaps.First().Matrix.Length + 3];
                firstRow[0] = "";
                firstRow[1] = "C";
                firstRow[2] = "-";
                for (int i = 3; i < snaps.First().Matrix.Length + 3; i++)
                {
                    int j = i - 3;
                    if (snap.IsArtificial[j])
                        firstRow[i] = isExtrMax ? "-M" : "M";
                    else
                        firstRow[i] = isExtrMax ? $"{snap.FunctionVariables[j]}" : $"{-snap.FunctionVariables[j]}";
                }
                resultsGridView.Rows.Add(firstRow);

                string[] secondRow = new string[snaps.First().Matrix.Length + 3];
                secondRow[0] = "";
                secondRow[1] = "B";

                for (int i = 2; i < snaps.First().Matrix.Length + 3; i++)
                {
                    secondRow[i] = $"A{i - 2}";
                }

                resultsGridView.Rows.Add(secondRow);

                for (int i = 0; i < snaps.First().C.Length; i++)
                {
                    string[] row = new string[snaps.First().Matrix.Length + 3];
                    for (int j = 0; j < snaps.First().Matrix.Length + 3; j++)
                    {
                        if (j == 0)
                        {
                            if (snap.IsArtificial[snap.C[i]])
                            {
                                row[j] = isExtrMax ? "-M" : "M";
                            }
                            else
                            {
                                Fraction value = snap.FunctionVariables[snap.C[i]];
                                row[j] = isExtrMax ? $"{snap.FunctionVariables[snap.C[i]]}" : $"{-snap.FunctionVariables[snap.C[i]]}";
                            }
                        }
                        else if (j == 1)
                        {
                            row[j] = $"x{snap.C[i] + 1}";
                        }
                        else if (j == 2)
                        {
                            row[j] = snap.B[i].ToString();
                        }
                        else
                        {
                            row[j] = snap.Matrix[j - 3][i].ToString();
                        }
                    }
                    resultsGridView.Rows.Add(row);
                }
                string[] fRow = new string[snaps.First().Matrix.Length + 3];
                fRow[0] = "";
                fRow[1] = "Δ";
                fRow[2] = isExtrMax ? snap.FunctionValue.ToString() : (-snap.FunctionValue).ToString();
                for (int i = 3; i < snaps.First().Matrix.Length + 3; i++)
                {
                    fRow[i] = isExtrMax ? snap.F[i - 3].ToString() : (-snap.F[i - 3]).ToString();
                }
                resultsGridView.Rows.Add(fRow);
                string[] emptyRow = new string[snaps.First().Matrix.Length + 3];
                resultsGridView.Rows.Add(emptyRow);
                resultsGridView.Rows.Add(emptyRow);
            }
        }

        private void FillCustomConstraints(Fraction[][] consMatrx, string[] signs, Fraction[] freeVars)
        {

            constraintsCount = signs.Length;
            nOfContraintsTextBox.Text = constraintsCount.ToString();
            variablesCount = consMatrx.First().Length;
            nOfVariablesTextBox.Text = variablesCount.ToString();
            FillConstraintsGrid();

            for (int i = 0; i < constraintsCount; i++)
            {
                for (int j = 0; j < variablesCount + 2; j++)
                {
                    if (j < variablesCount)
                    {
                        constraintsGridView.Rows[i].Cells[j].Value = consMatrx[i][j].ToString();
                    }
                    else if (j < variablesCount + 1)
                    {
                        if (signs[i] == "<=")
                            constraintsGridView.Rows[i].Cells[j].Value = "≤";
                        else if (signs[i] == ">=")
                            constraintsGridView.Rows[i].Cells[j].Value = "≥";
                        else
                            constraintsGridView.Rows[i].Cells[j].Value = "=";
                    }
                    else if (j < variablesCount + 2)
                    {
                        constraintsGridView.Rows[i].Cells[j].Value = freeVars[i].ToString();
                    }

                }
            }
        }

        private void FillCustomFunction(Fraction[] funcVars, Fraction c, bool isExtrMax)
        {
            FillFunctionGrid();
            for (int i = 0; i < variablesCount + 1; i++)
            {
                if (i < variablesCount)
                {
                    functionGridView.Rows[0].Cells[i].Value = funcVars[i].ToString();
                }
                else
                {
                    functionGridView.Rows[0].Cells[i].Value = c.ToString();
                }
            }

            extrComboBox.SelectedIndex = isExtrMax ? 0 : 1;
        }

        private void defaultBtn_Click(object sender, EventArgs e)
        {
            Problem p = CustomProblem.GetCustomProblem();
            FillCustomConstraints(p.ConstraintMatrix, p.Signs, p.FreeVariables);
            FillCustomFunction(p.FunctionVariables, p.C, p.IsExtrMax);
            Calculate();
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            nOfContraintsTextBox.Clear();
            nOfVariablesTextBox.Clear();
            resultsGridView.Columns.Clear();
            functionGridView.Columns.Clear();
            constraintsGridView.Columns.Clear();
            extrComboBox.SelectedIndex = -1;
            resultsLbl.ResetText();
        }

        private void nOfVariablesTextBox_TextChanged(object sender, EventArgs e)
        {
            if (nOfVariablesTextBox.Text != "" && nOfContraintsTextBox.Text != "")
            {
                try
                {
                    constraintsCount = Convert.ToInt32(nOfContraintsTextBox.Text);
                    variablesCount = Convert.ToInt32(nOfVariablesTextBox.Text);
                    FillConstraintsGrid();
                    FillFunctionGrid();
                    resultsGridView.Rows.Clear();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }

        }

        private void goBtn_Click(object sender, EventArgs e)
        {
            Calculate();
        }

        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            dataGridView.ClearSelection();
        }
    }
}
