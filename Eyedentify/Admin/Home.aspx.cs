using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace Eyedentify.Admin
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateRoleGrid();
                PopulateUserGrid();
                PopulateAssignedRole();
            }
        }

        private void PopulateUserGrid()
        {
            UsersGridView.DataSource = Membership.GetAllUsers();
            UsersGridView.DataBind();
        }

        private void PopulateRoleGrid()
        {
            RolesGridView.DataSource = Roles.GetAllRoles();
            RolesGridView.DataBind();
        }

        private void PopulateAssignedRole()
        {
            AssignedRoleListBox.DataSource = Roles.GetAllRoles();
            AssignedRoleListBox.DataBind();
        }

        protected void AddRoleButton_Click(object sender, EventArgs e)
        {
            string RoleName = RoleTextBox.Text.Trim();
            if (Roles.RoleExists(RoleName))
            {

            }
            else
            {
                Roles.CreateRole(RoleName);
                RoleTextBox.Text = string.Empty;
                PopulateRoleGrid();
            }
        }

        protected void UsersGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = UsersGridView.SelectedRow;
            AssignedRoleListBox.SelectedIndex = -1;

            string username = row.Cells[1].Text;
            SelectedUserLabel.Text = username;
            Panel1.Visible = true;

            foreach (string userRole in Roles.GetRolesForUser(username))
            {
                AssignedRoleListBox.Items.FindByText(userRole).Selected = true;
            }
        }

        protected void AssignRoleButton_Click(object sender, EventArgs e)
        {
            if (UsersGridView.SelectedIndex > -1)
            {
                foreach (string roles in Roles.GetAllRoles())
                {
                    try
                    {
                        Roles.RemoveUserFromRole(SelectedUserLabel.Text, roles);
                    }
                    catch { }
                }

                foreach (int i in AssignedRoleListBox.GetSelectedIndices())
                {
                    string selectedRole = AssignedRoleListBox.Items[i].Text;
                    Roles.AddUserToRole(SelectedUserLabel.Text, selectedRole);
                }

                UsersGridView.SelectedIndex = -1;
                Panel1.Visible = false;
                AssignedRoleListBox.SelectedIndex = -1;
            }
        }

        protected void DeselectAllButton_Click(object sender, EventArgs e)
        {
            AssignedRoleListBox.SelectedIndex = -1;
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            UsersGridView.SelectedIndex = -1;
            Panel1.Visible = false;
        }  
    }
}