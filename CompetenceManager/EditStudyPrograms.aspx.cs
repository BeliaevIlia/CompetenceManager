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
    public partial class EditStudyPrograms : System.Web.UI.Page
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
            var allSP = from sp in _eJDataContext.StudyPrograms.ToList()
                           select sp;

            if (allSP.ToList().Count > 0)
            {
                GridView1.DataSource = allSP;
                GridView1.DataBind();
            }
            else
            {
                allSP.ToList().Add(new StudyPrograms());
                GridView1.DataSource = allSP;
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
            TextBox tb_NewName = (TextBox)GridView1.FooterRow.FindControl("tb_NewName");
            TextBox tb_NewAbout = (TextBox)GridView1.FooterRow.FindControl("tb_NewAbout");
            TextBox tb_NewStudyForm = (TextBox)GridView1.FooterRow.FindControl("tb_NewStudyForm");
            DropDownList ddl_NewCompetence = (DropDownList)GridView1.FooterRow.FindControl("ddl_NewCompetence");
 
            var sp = new StudyPrograms();

            sp.Name = tb_NewName.Text;
            sp.About = tb_NewAbout.Text;
            sp.StudyForm = tb_NewStudyForm.Text;
            sp.Competence = ddl_NewCompetence.SelectedValue;

            _eJDataContext.StudyPrograms.InsertOnSubmit(sp);
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

                var delSP = from sp in _eJDataContext.StudyPrograms.ToList()
                            where sp.Id == id
                            select sp;
                _eJDataContext.StudyPrograms.DeleteOnSubmit(delSP.First());
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
            TextBox tb_Name = (TextBox)GridView1.Rows[e.RowIndex].FindControl("tb_Name");
            TextBox tb_About = (TextBox)GridView1.Rows[e.RowIndex].FindControl("tb_About");
            TextBox tb_StudyForm = (TextBox)GridView1.Rows[e.RowIndex].FindControl("tb_StudyForm");
            DropDownList ddl_Competence = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddl_Competence");

            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0].ToString());

            var updSP = from sp in _eJDataContext.StudyPrograms.ToList()
                           where sp.Id == id
                           select sp;
            foreach (StudyPrograms updS in updSP)
            {
                updS.Name = tb_Name.Text;
                updS.About = tb_About.Text;
                updS.StudyForm = tb_StudyForm.Text;
                updS.Competence = ddl_Competence.SelectedValue;
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