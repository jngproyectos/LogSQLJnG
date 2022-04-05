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
    public partial class UsuarioFrm : Form
    {
        public UsuarioFrm()
        {
            InitializeComponent();
        }
        private List<RolUsuario> ComboRol()
        {
            using(dbLoginEntities db = new dbLoginEntities())
            {
                return db.RolUsuario.ToList();
            }
        }
        private void CargarCombo()
        {
            try
            {
                var Lst = ComboRol();
                cbRol.DataSource = Lst;
                cbRol.DisplayMember = "RolNombre";
                cbRol.ValueMember = "IdRol";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void GuardarUsuario(Usuario pUsuario)
        {
            try
            {
                using(dbLoginEntities db = new dbLoginEntities())
                {
                    db.Usuario.Add(pUsuario);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UsuarioFrm_Load(object sender, EventArgs e)
        {
            CargarCombo();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Usuario usuarioEnt = new Usuario();
            usuarioEnt.Usuario1 = txtUsuario.Text;
            usuarioEnt.Password = txtPasword.Text;
            usuarioEnt.RolId = (int)cbRol.SelectedValue;
            GuardarUsuario(usuarioEnt);
            MessageBox.Show("Usuario guardado");
        }
    }
}
