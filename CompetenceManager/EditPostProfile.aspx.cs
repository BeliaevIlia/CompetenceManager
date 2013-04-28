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
    public partial class EditPostProfile : System.Web.UI.Page
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
            var allpp = from pp in _eJDataContext.PostProfile.ToList()
                          select pp;

            if (allpp.ToList().Count > 0)
            {
                GridView1.DataSource = allpp;
                GridView1.DataBind();
            }
            else
            {
                allpp.ToList().Add(new PostProfile());
                GridView1.DataSource = allpp;
                GridView1.DataBind();

                int TotalColumns = GridView1.Rows[0].Cells.Count;
                GridView1.Rows[0].Cells.Clear();
                GridView1.Rows[0].Cells.Add(new TableCell());
                GridView1.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                GridView1.Rows[0].Cells[0].Text = "No Record Found";
            }
        }


        protected void LinkButton3_Click1(object sender, EventArgs e)
        {
            TextBox tb_NewName = (TextBox)GridView1.FooterRow.FindControl("tb_NewName");

            var pp = new PostProfile();

            pp.Name = tb_NewName.Text;

            _eJDataContext.PostProfile.InsertOnSubmit(pp);
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
            string ppName = GridView1.DataKeys[e.RowIndex].Values[1].ToString();

            var delStaff = from staff in _eJDataContext.Staff.ToList()
                           where staff.Post == ppName
                           select staff;
            foreach (var delSProgram in delStaff)
                _eJDataContext.Staff.DeleteOnSubmit(delSProgram);
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

            var delPP = from pp in _eJDataContext.PostProfile.ToList()
                          where pp.Id == id
                          select pp;
            _eJDataContext.PostProfile.DeleteOnSubmit(delPP.First());
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

            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0].ToString());
            string newPPName = tb_NewName.Text;
            string oldPPName = GridView1.DataKeys[e.RowIndex].Values[1].ToString();

            var updStaff = from sProg in _eJDataContext.Staff.ToList()
                           where sProg.Post == oldPPName
                           select sProg;
            foreach (Staff updSt in updStaff)
            {
                updSt.Post = newPPName;
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
            var updPP = from pp in _eJDataContext.PostProfile.ToList()
                           where pp.Id == id
                           select pp;
            foreach (PostProfile updP in updPP)
            {
                updP.Name = newPPName;
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