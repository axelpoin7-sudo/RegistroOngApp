using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace RegistroOngApp
{
    public class MainForm : Form
    {
        private TextBox txtNombre;
        private ComboBox cmbRol;
        private Button btnGuardar;

        public MainForm()
        {
            // Configuración del Formulario 
            this.Text = "Registro de Personal - ONG";
            this.Size = new Size(350, 250);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblNombre = new Label()
            {
                Text = "Nombre completo:",
                Location = new
                Point(20, 20),
                AutoSize = true
            };
            txtNombre = new TextBox() { Location = new Point(20, 45), Width = 280 };

            Label lblRol = new Label()
            {
                Text = "Rol:",
                Location = new Point(20, 80),
                AutoSize =true
            };
            cmbRol = new ComboBox()
            {
                Location = new Point(20, 105),
                Width = 280,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbRol.Items.AddRange(new string[] { "Voluntario", "Coordinador" });
            cmbRol.SelectedIndex = 0;

            btnGuardar = new Button()
            {
                Text = "Guardar Registro",
                Location = new Point(20,
                150),
                Width = 280,
                BackColor = Color.LightGreen
            };
            btnGuardar.Click += BtnGuardar_Click;

            this.Controls.Add(lblNombre);
            this.Controls.Add(txtNombre);
            this.Controls.Add(lblRol);
            this.Controls.Add(cmbRol);
            this.Controls.Add(btnGuardar);
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese un nombre válido.");
                return;
            }

            try
            {
                // 1. Uso del Factory Pattern para crear el objeto 
                Usuario nuevoUser = CreadorUsuario.Crear(cmbRol.SelectedItem.ToString(),
                txtNombre.Text);

                // 2. Uso del Singleton para obtener la conexión y guardar 
                var db = DatabaseConnection.Instance.Connection;
                if (db.State == System.Data.ConnectionState.Closed) db.Open();

                string query = "INSERT INTO Usuarios (Nombre, Rol) VALUES (@nombre, @rol)"; 
                using (SqlCommand cmd = new SqlCommand(query, db))
                {
                    cmd.Parameters.AddWithValue("@nombre", nuevoUser.Nombre);
                    cmd.Parameters.AddWithValue("@rol", nuevoUser.GetRol());
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show($"¡{nuevoUser.GetRol()} registrado exitosamente en la BD!");
                txtNombre.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }

    static class Program
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}