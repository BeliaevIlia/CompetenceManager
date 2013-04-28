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
    public partial class EditStaff : System.Web.UI.Page
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
            var allStaff = from staff in _eJDataContext.Staff.ToList()
                           select staff;

            if (allStaff.ToList().Count > 0)
            {
                GridView1.DataSource = allStaff;
                GridView1.DataBind();
            }
            else
            {
                allStaff.ToList().Add(new Staff());
                GridView1.DataSource = allStaff;
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
            TextBox tb_NewFIO = (TextBox)GridView1.FooterRow.FindControl("tb_NewFIO");
            DropDownList ddl_NewGender = (DropDownList)GridView1.FooterRow.FindControl("ddl_NewGender");
            TextBox tb_NewDate = (TextBox)GridView1.FooterRow.FindControl("tb_NewDate");
            TextBox tb_NewPassword = (TextBox)GridView1.FooterRow.FindControl("tb_NewPassword");
            TextBox tb_NewRole = (TextBox)GridView1.FooterRow.FindControl("tb_NewRole");
            DropDownList ddl_NewPost = (DropDownList)GridView1.FooterRow.FindControl("ddl_NewPost");

            var staff = new Staff();

            staff.FIO = tb_NewFIO.Text;
            staff.Gender = ddl_NewGender.SelectedValue;
            try
            {
                staff.BirthDate = Convert.ToDateTime(tb_NewDate.Text).Date;
            }
            catch (Exception exept)
            {
                Console.WriteLine("Error:  " + exept);
            }
            staff.Password = tb_NewPassword.Text;
            staff.Role = tb_NewRole.Text;
            staff.Post = ddl_NewPost.SelectedValue;

            _eJDataContext.Staff.InsertOnSubmit(staff);
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

                var delStaff = from staff in _eJDataContext.Staff.ToList()
                               where staff.Id == id
                               select staff;
                _eJDataContext.Staff.DeleteOnSubmit(delStaff.First());
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
            ((TextBox)GridView1.Rows[e.RowIndex].FindControl("tb_Date")).Text = GridView1.DataKeys[e.RowIndex].Values[3].ToString();
            TextBox tb_NewFIO = (TextBox)GridView1.Rows[e.RowIndex].FindControl("tb_FIO");
            DropDownList ddl_NewGender = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddl_Gender");
            TextBox tb_NewDate = (TextBox)GridView1.Rows[e.RowIndex].FindControl("tb_Date");
            TextBox tb_NewPassword = (TextBox)GridView1.Rows[e.RowIndex].FindControl("tb_Password");
            TextBox tb_NewRole = (TextBox)GridView1.Rows[e.RowIndex].FindControl("tb_Role");
            DropDownList ddl_NewPost = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddl_Post");

            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0].ToString());

            var updStaff = from staff in _eJDataContext.Staff.ToList()
                           where staff.Id == id
                           select staff;
            foreach (Staff updStaf in updStaff)
            {
                updStaf.FIO = tb_NewFIO.Text;
                updStaf.Gender = ddl_NewGender.SelectedValue;
                try
                {
                    updStaf.BirthDate = Convert.ToDateTime(tb_NewDate.Text).Date;
                }
                catch (Exception exept)
                {
                    Console.WriteLine("Error:  " + exept);
                }
                updStaf.Password = tb_NewPassword.Text;
                updStaf.Role = tb_NewRole.Text;
                updStaf.Post = ddl_NewPost.SelectedValue;
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