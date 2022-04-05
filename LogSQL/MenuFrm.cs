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
    public partial class MenuFrm : Form
    {
        public MenuFrm()
        {
            InitializeComponent();
        }
        public MenuFrm(string pUser, int pIdRol, int pIdUsu)
        {
            InitializeComponent();
            label1.Text = pUser;
            IdRol = pIdRol;
            IdUsu = pIdUsu;
        }
        int IdRol, IdUsu;
        private List<Permiso> SelectOpcion(int pId)
        {
            using (dbLoginEntities db = new dbLoginEntities())
            {
                return db.Permiso.Where(i => i.RolUsuId == pId).ToList();
            }
        }
        private void ConsultarRol(Control pCon)
        {
            var LstOp = SelectOpcion(IdRol);
            foreach(Control c in pCon.Controls)
            {
                if(c is Button)
                {
                    foreach(Permiso opc in LstOp)
                    {
                        if (opc.OpcionId == Convert.ToInt32(c.Tag))
                        {
                            if (!opc.Permitido)
                                c.Enabled = false;
                        }
                    }
                }
            }
        }
        #region Diseño
        //Panel que utilizamos como línea
        Panel p = new Panel();
        //Handler del mouse enter, este sucede cuando el mouse se encuentra
        //por encima del button
        private void btnMouseEnter(object sender, EventArgs e)
        {
            //Button que hace la invocación
            Button btn = sender as Button;
            //agregamos un panel(línea) en el panel de nombre pMenu
            pMenu.Controls.Add(p);
            //Color de fondo
            p.BackColor = Color.FromArgb(90, 210, 2);
            //tamaño igual al button(largo)
            p.Size = new Size(140, 5);
            //nueva ubicación, aparezca debajo del button
            p.Location = new
                Point(btn.Location.X, btn.Location.Y + 40);
            //hay que agregar en el evento del button
        }
        //Handler del mouse leave, este sucede cuando el mouse sale del button
        private void btnMouseLeave(object sender, EventArgs e)
        {
            //removemos el panel(línea) del panel de nombre pMenu
            pMenu.Controls.Remove(p);
            //hay que agregar en el evento del button
        }
        //Método para ocultar el subMenu de cada button
        private void ocultarSubMenu()
        {
            //Condiciones para mostrar el subMenu si esta visible lo oculta 
            if (pAdmin.Visible == true)
                pAdmin.Visible = false;
            if (pServicios.Visible == true)
                pServicios.Visible = false;
            if (pMantenimiento.Visible == true)
                pMantenimiento.Visible = false;
            if (pClientes.Visible == true)
                pClientes.Visible = false;
        }
        //Método para mostrar el subMenu, recibe un panel que será el subMenu(panel)
        //Este método lo agregamos en cada button(click) que queremos muestre el subMenu
        private void mostrarSubMenu(Panel subMenu)
        {
            //si no es visible el subMenu
            if (subMenu.Visible == false)
            {
                //oculta todos los subMenus
                ocultarSubMenu();
                //Muestra el subMenu que recibio
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }
        private void btnAdmin_Click(object sender, EventArgs e)
        {
            mostrarSubMenu(pAdmin);
        }
        private void btnServicios_Click(object sender, EventArgs e)
        {
            mostrarSubMenu(pServicios);
        }
        private void btnMantenimiento_Click(object sender, EventArgs e)
        {
            mostrarSubMenu(pMantenimiento);
        }
        private void btnCliente_Click(object sender, EventArgs e)
        {
            mostrarSubMenu(pClientes);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion
        private void button9_Click(object sender, EventArgs e)
        {
            UsuarioFrm usu = new UsuarioFrm();
            usu.ShowDialog();
        }

        private void MenuFrm_Load(object sender, EventArgs e)
        {
            ConsultarRol(pAdmin);
            ConsultarRol(pServicios);
            ConsultarRol(pMantenimiento);
            ConsultarRol(pClientes);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            RolUsuFrm rol = new RolUsuFrm();
            rol.ShowDialog();
        }
    }
}
