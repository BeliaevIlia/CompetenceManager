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
    public partial class EditProjects : System.Web.UI.Page
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
            var allp = from p in _eJDataContext.Projects.ToList()
                       select p;

            if (allp.ToList().Count > 0)
            {
                GridView1.DataSource = allp;
                GridView1.DataBind();
            }
            else
            {
                allp.ToList().Add(new Projects());
                GridView1.DataSource = allp;
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

            var p = new Projects();

            p.Name = tb_NewName.Text;
            p.About = tb_NewAbout.Text;

            _eJDataContext.Projects.InsertOnSubmit(p);
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

                var delP = from p in _eJDataContext.Projects.ToList()
                            where p.Id == id
                            select p;
                _eJDataContext.Projects.DeleteOnSubmit(delP.First());
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

            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0].ToString());

            var updP = from p in _eJDataContext.Projects.ToList()
                        where p.Id == id
                        select p;
            foreach (Projects updT in updP)
            {
                updT.Name = tb_Name.Text;
                updT.About = tb_About.Text;
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