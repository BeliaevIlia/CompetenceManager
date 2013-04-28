using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CompetenceManager
{
    public partial class EditCompetence : System.Web.UI.Page
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
            var allComp = from comp in _eJDataContext.Competence.ToList()
                          select comp;

            if (allComp.ToList().Count > 0)
            {
                GridView1.DataSource = allComp;
                GridView1.DataBind();
            }
            else
            {
                allComp.ToList().Add(new Competence());
                GridView1.DataSource = allComp;
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
            DropDownList ddl_NewCompType = (DropDownList)GridView1.FooterRow.FindControl("ddl_NewCompType");

            var comp = new Competence();

            comp.Name = tb_NewName.Text;
            comp.Type = ddl_NewCompType.SelectedValue;

            _eJDataContext.Competence.InsertOnSubmit(comp);
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
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0].ToString());
            string compName = GridView1.DataKeys[e.RowIndex].Values[1].ToString();

            var delSPrograms = from sProg in _eJDataContext.StudyPrograms.ToList()
                               where sProg.Competence == compName
                               select sProg;
            foreach(var delSProgram in delSPrograms)
                _eJDataContext.StudyPrograms.DeleteOnSubmit(delSProgram);
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

            var delComp = from comp in _eJDataContext.Competence.ToList()
                          where comp.Id == id
                          select comp;
            _eJDataContext.Competence.DeleteOnSubmit(delComp.First());
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

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            FillCustomerInGrid();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox tb_NewName = (TextBox)GridView1.Rows[e.RowIndex].FindControl("tb_Name");
            DropDownList ddl_NewCompType = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddl_CompType");

            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0].ToString());
            string newCompName = tb_NewName.Text;
            string oldCompName = GridView1.DataKeys[e.RowIndex].Values[1].ToString();
            string compType = ddl_NewCompType.SelectedValue;

            var updSPrograms = from sProg in _eJDataContext.StudyPrograms.ToList()
                               where sProg.Competence == oldCompName
                               select sProg;
            foreach (StudyPrograms updSProgram in updSPrograms)
            {
                updSProgram.Competence = newCompName;
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
            var updComps = from comp in _eJDataContext.Competence.ToList()
                          where comp.Id == id
                          select comp;
            foreach (Competence updComp in updComps)
            {
                updComp.Name = newCompName;
                updComp.Type = compType;
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