using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CompetenceManager
{
    public partial class EditTestTasks : System.Web.UI.Page
    {
        DataClasses1DataContext _eJDataContext;
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["CompDBConnectionString"].ConnectionString;
            _eJDataContext = new DataClasses1DataContext(connStr);
            if (!IsPostBack)
                FillCustomerInGrid();
        }

        private void FillCustomerInGrid()
        {
            var alltt = from tt in _eJDataContext.TestTasks.ToList()
                          select tt;

            if (alltt.ToList().Count > 0)
            {
                GridView1.DataSource = alltt;
                GridView1.DataBind();
            }
            else
            {
                alltt.ToList().Add(new TestTasks());
                GridView1.DataSource = alltt;
                GridView1.DataBind();

                int TotalColumns = GridView1.Rows[0].Cells.Count;
                GridView1.Rows[0].Cells.Clear();
                GridView1.Rows[0].Cells.Add(new TableCell());
                GridView1.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                GridView1.Rows[0].Cells[0].Text = "No Record Found";
            }
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            TextBox tb_NewSubject = (TextBox)GridView1.FooterRow.FindControl("tb_NewSubject");
            TextBox tb_NewQuest = (TextBox)GridView1.FooterRow.FindControl("tb_NewQuest");
            TextBox tb_NewAnswers = (TextBox)GridView1.FooterRow.FindControl("tb_NewAnswers");
            TextBox tb_NewTrueAnswer = (TextBox)GridView1.FooterRow.FindControl("tb_NewTrueAnswer");
            TextBox tb_NewDifficult = (TextBox)GridView1.FooterRow.FindControl("tb_NewDifficult");

            var tt = new TestTasks();

            tt.Subject = tb_NewSubject.Text;
            tt.Quest = tb_NewQuest.Text;
            tt.Answers = tb_NewAnswers.Text;
            tt.TrueAnswer = tb_NewTrueAnswer.Text;
            tt.Dificult = tb_NewDifficult.Text;

            _eJDataContext.TestTasks.InsertOnSubmit(tt);
            try
            {
                _eJDataContext.SubmitChanges();
            }
            catch
            {
                _eJDataContext.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
                {
                    try
                    {
                        _eJDataContext.SubmitChanges();
                    }
                    catch (Exception exept)
                    {
                        Console.WriteLine("Error:  " + exept);
                    }
                }
            }

            FillCustomerInGrid();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if ((sender as GridView).Rows.Count > 1)
            {
                int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0].ToString());

                var delTT = from tt in _eJDataContext.TestTasks.ToList()
                              where tt.Id == id
                              select tt;
                _eJDataContext.TestTasks.DeleteOnSubmit(delTT.First());
                try
                {
                    _eJDataContext.SubmitChanges();
                }
                catch
                {
                    _eJDataContext.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
                    {
                        try
                        {
                            _eJDataContext.SubmitChanges();
                        }
                        catch (Exception exept)
                        {
                            Console.WriteLine("Error:  " + exept);
                        }
                    }
                }

                FillCustomerInGrid();
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            FillCustomerInGrid();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox tb_Subject = (TextBox)GridView1.Rows[e.RowIndex].FindControl("tb_Subject");
            TextBox tb_Quest = (TextBox)GridView1.Rows[e.RowIndex].FindControl("tb_Quest");
            TextBox tb_Answers = (TextBox)GridView1.Rows[e.RowIndex].FindControl("tb_Answers");
            TextBox tb_TrueAnswer = (TextBox)GridView1.Rows[e.RowIndex].FindControl("tb_TrueAnswer");
            TextBox tb_Difficult = (TextBox)GridView1.Rows[e.RowIndex].FindControl("tb_Difficult");

            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0].ToString());
         
            var updTT = from tt in _eJDataContext.TestTasks.ToList()
                           where tt.Id == id
                           select tt;
            foreach (TestTasks updT in updTT)
            {
                updT.Subject = tb_Subject.Text;
                updT.Quest = tb_Quest.Text;
                updT.Answers = tb_Answers.Text;
                updT.TrueAnswer = tb_TrueAnswer.Text;
                updT.Dificult = tb_Difficult.Text;
            }
            try
            {
                _eJDataContext.SubmitChanges();
            }
            catch
            {
                _eJDataContext.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
                {
                    try
                    {
                        _eJDataContext.SubmitChanges();
                    }
                    catch (Exception exept)
                    {
                        Console.WriteLine("Error:  " + exept);
                    }
                }
            }

            GridView1.EditIndex = -1;
            FillCustomerInGrid();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            FillCustomerInGrid();
        }
    }
}