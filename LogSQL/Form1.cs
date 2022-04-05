using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogSQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private List<Usuario> BuscarUsuario(string pUser, string pPas)
        {
            try
            {
                using(dbLoginEntities db = new dbLoginEntities())
                {
                    return db.Usuario.Where(us => us.Usuario1 == pUser
                            && us.Password == pPas).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        int IdRol, IdUsu;
        string nomUsu;
        private bool ValidarCampos()
        {
            var usuario = BuscarUsuario(txtUsuario.Text, txtPasword.Text);
            foreach(var usu in usuario)
            {
                IdRol = usu.RolId;
                IdUsu = usu.IdUsuario;
                nomUsu = usu.Usuario1;
            }
            if (txtUsuario.Text == string.Empty)
            {
                MessageBox.Show("Ingrese usuario");
                txtUsuario.Focus();
                return false;
            }
            else if (txtPasword.Text == string.Empty)
            {
                MessageBox.Show("Ingrese password");
                txtPasword.Focus();
                return false;
            }
            else if (usuario.Count <= 0)
            {
                MessageBox.Show("Usuario no registrado");
                return false;
            }
            return true;
        }
        private void btnAcceder_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                MenuFrm m = new MenuFrm(nomUsu, IdRol, IdUsu);
                m.Show();
                this.Hide();
            }
        }
    }
}
