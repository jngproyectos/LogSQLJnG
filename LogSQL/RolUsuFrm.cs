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
    public partial class RolUsuFrm : Form
    {
        public RolUsuFrm()
        {
            InitializeComponent();
        }
        private void GuardarRol()
        {
            try
            {
                using(dbLoginEntities db = new dbLoginEntities())
                {
                    RolUsuario rol = new RolUsuario();
                    rol.RolNombre = txtUsuario.Text.ToUpper().Trim();
                    db.RolUsuario.Add(rol);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void GuardarPermiso(Permiso pPermiso)
        {
            try
            {
                using(dbLoginEntities db = new dbLoginEntities())
                {
                    db.Permiso.Add(pPermiso);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private int UltimoRegistro()
        {
            using(dbLoginEntities db = new dbLoginEntities())
            {
                var ultimo = (from c in db.RolUsuario
                              orderby c.IdRol descending
                              select c.IdRol).FirstOrDefault();
                return ultimo;
            }
        }
        private void CheckRol()
        {
            Permiso permisoEntidad = new Permiso();
            int Id = UltimoRegistro();
            foreach (Control chk in panel1.Controls)
            {
                permisoEntidad.RolUsuId = Id;
                if(chk is CheckBox)
                {
                    if (((CheckBox)chk).Checked)
                    {
                        permisoEntidad.OpcionId = Convert.ToInt32(chk.Tag);
                        permisoEntidad.Permitido = true;
                        GuardarPermiso(permisoEntidad);
                    }
                    else
                    {
                        permisoEntidad.OpcionId = Convert.ToInt32(chk.Tag);
                        permisoEntidad.Permitido = false;
                        GuardarPermiso(permisoEntidad);
                    }
                }
            }
        }
        private void Limpiar()
        {
            txtUsuario.Text = string.Empty;
            txtUsuario.Focus();
            foreach(Control chk in panel1.Controls)
            {
                if(chk is CheckBox)
                {
                    if (((CheckBox)chk).Checked)
                        ((CheckBox)chk).Checked = false;
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarRol();
            CheckRol();
            Limpiar();
            MessageBox.Show("Rol de usuario guardado");
        }
    }
}
