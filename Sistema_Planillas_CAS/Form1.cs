using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sistema_Planillas_CAS
{
    public partial class Form1 : Form
    {
        Panel panelBusqueda;  // Panel dinámico para los campos de búsqueda
        Panel panelBotones;    // Panel para los botones superiores
        Button btnBuscar, btnSalir;  // Botones de buscar y salir
        Panel panelBorde;      // Panel para el borde con degradado
        DataGridView dataGridView; // DataGridView para mostrar los datos de empleados
        PictureBox pictureBox; // PictureBox para la imagen

        public Form1()
        {
            InitializeComponent();

            // Llamamos a la ventana de bienvenida
            MostrarVentanaBienvenida();
        }

        // Ventana de bienvenida
        private void MostrarVentanaBienvenida()
        {
            Form ventanaBienvenida = new Form
            {
                Text = "Bienvenido",
                Size = new System.Drawing.Size(300, 150),
                StartPosition = FormStartPosition.CenterScreen
            };

            Label lblBienvenida = new Label
            {
                Text = "Bienvenido al Sistema de Planillas CAS",
                AutoSize = false,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
            };

            ventanaBienvenida.Controls.Add(lblBienvenida);

            ventanaBienvenida.Shown += (s, e) =>
            {
                Timer timer = new Timer();
                timer.Interval = 1000; // 1 segundo
                timer.Tick += (sender, args) =>
                {
                    timer.Stop();
                    ventanaBienvenida.Close();
                    MostrarMenuPrincipal();
                };
                timer.Start();
            };

            ventanaBienvenida.ShowDialog();
        }

        // Nueva ventana con el menú principal
        private void MostrarMenuPrincipal()
        {
            Form menuPrincipal = new Form
            {
                Text = "Menú Principal - Sistema de Planillas CAS",
                Size = new System.Drawing.Size(600, 400),
                StartPosition = FormStartPosition.CenterScreen
            };

            TabControl tabControl = new TabControl
            {
                Dock = DockStyle.Fill
            };

            TabPage tabEmpleados = new TabPage("Empleados");

            // Panel fijo para los botones superiores con borde
            panelBotones = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BorderStyle = BorderStyle.Fixed3D  // Borde alrededor del panel
            };

            // Panel dinámico para los campos de búsqueda
            panelBusqueda = new Panel
            {
                Dock = DockStyle.Fill // Ocupa el espacio restante
            };

            // Botón de Código
            Button btnCodigo = new Button
            {
                Text = "Código",
                Location = new System.Drawing.Point(100, 10),  // Ajusta la posición central
                AutoSize = true
            };
            btnCodigo.Click += BtnCodigo_Click;

            // Botón de Apellidos y Nombres
            Button btnApellido = new Button
            {
                Text = "Apellidos y Nombres",
                Location = new System.Drawing.Point(200, 10),  // Ajusta la posición central
                AutoSize = true
            };
            btnApellido.Click += BtnApellido_Click;

            // Botón de Buscar
            btnBuscar = new Button
            {
                Text = "Buscar",
                Location = new System.Drawing.Point(400, 10),  // Ubicación en la parte amarilla según la imagen
                AutoSize = true
            };
            btnBuscar.Click += btnBuscar_Click;  // Conectar el evento del botón buscar

            // Panel para los botones superiores
            panelBotones.Controls.Add(btnCodigo);
            panelBotones.Controls.Add(btnApellido);
            panelBotones.Controls.Add(btnBuscar);  // Solo dejamos los botones que solicitaste

            // Crear el panelBorde con degradado
            panelBorde = new Panel
            {
                Location = new System.Drawing.Point(50, 180),  // Ubicación debajo de los campos de entrada
                Height = 5,
                Width = 700, // Ancho para que cubra todo el área
            };
            panelBorde.Paint += PanelBorde_Paint; // Asignar el evento de Paint para el degradado

            // Mostrar el DataGridView con las cabeceras desde el inicio
            MostrarDatosEmpleado();

            // Agregar la imagen debajo de los campos de búsqueda
            pictureBox = new PictureBox
            {
                Location = new System.Drawing.Point(520, 50),  // Ajustar la posición de la imagen
                Size = new System.Drawing.Size(230, 70),  // Ajustar el tamaño de la imagen
                SizeMode = PictureBoxSizeMode.StretchImage // Ajustar la imagen al tamaño del PictureBox
            };

            // Obtener el directorio base del proyecto
            string rutaBase = AppDomain.CurrentDomain.BaseDirectory;

            // Combinar la ruta base con la ubicación de la imagen en la carpeta Images
            string rutaImagen = System.IO.Path.Combine(rutaBase, @"Images\logo.png");

            // Cargar la imagen
            pictureBox.Image = Image.FromFile(rutaImagen);

            // Agregar la imagen al panel de búsqueda
            panelBusqueda.Controls.Add(pictureBox);

            // Agregar paneles al TabPage
            tabEmpleados.Controls.Add(panelBusqueda); // Panel dinámico primero
            tabEmpleados.Controls.Add(panelBotones);  // Panel de botones fijo arriba
            tabEmpleados.Controls.Add(panelBorde);    // Agregar el borde fijo directamente al TabPage

            TabPage tabNominas = new TabPage("Nóminas");
            Label lblNomina = new Label
            {
                Text = "Gestión de Nóminas",
                Location = new System.Drawing.Point(20, 30),
                AutoSize = true
            };
            tabNominas.Controls.Add(lblNomina);

            tabControl.TabPages.Add(tabEmpleados);
            tabControl.TabPages.Add(tabNominas);

            // Botones en la parte inferior izquierda
            Button btnNuevo = new Button
            {
                Text = "Nuevo",
                Location = new System.Drawing.Point(20, 500),
                AutoSize = true
            };
            btnNuevo.Click += BtnNuevo_Click;

            Button btnAceptar = new Button
            {
                Text = "Aceptar",
                Location = new System.Drawing.Point(100, 500),
                AutoSize = true
            };

            Button btnBoleta = new Button
            {
                Text = "Boleta",
                Location = new System.Drawing.Point(180, 500),
                AutoSize = true
            };

            // Botón salir en la parte inferior derecha
            Button btnSalirInferior = new Button
            {
                Text = "Salir",
                Location = new System.Drawing.Point(650, 500),
                AutoSize = true
            };
            btnSalirInferior.Click += (sender, e) => Application.Exit();

            // Agregar botones inferiores al menú principal
            menuPrincipal.Controls.Add(btnNuevo);
            menuPrincipal.Controls.Add(btnAceptar);
            menuPrincipal.Controls.Add(btnBoleta);
            menuPrincipal.Controls.Add(btnSalirInferior);
            menuPrincipal.Controls.Add(tabControl);

            menuPrincipal.ShowDialog();
        }

        // Evento Paint para dibujar el degradado en el panelBorde
        private void PanelBorde_Paint(object sender, PaintEventArgs e)
        {
            // Crear el degradado de color
            using (LinearGradientBrush brush = new LinearGradientBrush(panelBorde.ClientRectangle, Color.Gray, Color.Black, LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, panelBorde.ClientRectangle);
            }
        }

        // Evento al hacer clic en "Código"
        private void BtnCodigo_Click(object sender, EventArgs e)
        {
            panelBusqueda.Controls.Clear();

            Label lblDni = new Label
            {
                Text = "Ingrese DNI:",
                Location = new System.Drawing.Point(100, 50),
                AutoSize = true
            };

            TextBox txtDniBusqueda = new TextBox
            {
                Location = new System.Drawing.Point(200, 50),
                Width = 200
            };

            panelBusqueda.Controls.Add(lblDni);
            panelBusqueda.Controls.Add(txtDniBusqueda);

            // Mostrar el DataGridView con las cabeceras
            MostrarDatosEmpleado();

            // Agregar la imagen debajo de los campos de búsqueda
            panelBusqueda.Controls.Add(pictureBox);
        }

        // Evento al hacer clic en "Apellidos y Nombres"
        private void BtnApellido_Click(object sender, EventArgs e)
        {
            panelBusqueda.Controls.Clear();

            Label lblNombre = new Label
            {
                Text = "Nombre:",
                Location = new System.Drawing.Point(100, 50),
                AutoSize = true
            };

            TextBox txtNombreBusqueda = new TextBox
            {
                Location = new System.Drawing.Point(200, 50),
                Width = 200
            };

            Label lblApellidoPaterno = new Label
            {
                Text = "Apellido Paterno:",
                Location = new System.Drawing.Point(100, 90),
                AutoSize = true
            };

            TextBox txtApellidoPaternoBusqueda = new TextBox
            {
                Location = new System.Drawing.Point(200, 90),
                Width = 200
            };

            Label lblApellidoMaterno = new Label
            {
                Text = "Apellido Materno:",
                Location = new System.Drawing.Point(100, 130),
                AutoSize = true
            };

            TextBox txtApellidoMaternoBusqueda = new TextBox
            {
                Location = new System.Drawing.Point(200, 130),
                Width = 200
            };

            panelBusqueda.Controls.Add(lblNombre);
            panelBusqueda.Controls.Add(txtNombreBusqueda);
            panelBusqueda.Controls.Add(lblApellidoPaterno);
            panelBusqueda.Controls.Add(txtApellidoPaternoBusqueda);
            panelBusqueda.Controls.Add(lblApellidoMaterno);
            panelBusqueda.Controls.Add(txtApellidoMaternoBusqueda);

            // Mostrar el DataGridView con las cabeceras
            MostrarDatosEmpleado();

            // Agregar la imagen debajo de los campos de búsqueda
            panelBusqueda.Controls.Add(pictureBox);
        }

        // Evento al hacer clic en "Buscar"
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            panelBusqueda.Controls.Clear();  // Limpiar panel

            // Simular búsqueda de DNI
            string dniBuscado = "12345678"; // Puedes reemplazarlo con una lógica real de búsqueda
            TextBox txtDniBusqueda = new TextBox(); // Esto sería el campo donde se ingresa el DNI para buscar

            // Suponemos que este es el DNI ingresado por el usuario
            if (txtDniBusqueda.Text == dniBuscado)
            {
                // Si el DNI coincide, mostrar los datos en el DataGridView
                MostrarDatosEmpleado();
            }
            else
            {
                // Mostrar mensaje si no se encuentra al trabajador
                MessageBox.Show("Trabajador no registrado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Mantener el borde visible
            if (!panelBusqueda.Controls.Contains(panelBorde))
            {
                panelBusqueda.Controls.Add(panelBorde);
            }

            // Agregar la imagen debajo de los campos de búsqueda
            panelBusqueda.Controls.Add(pictureBox);
        }

        // Método para mostrar los datos del empleado en el DataGridView
        private void MostrarDatosEmpleado()
        {
            // Si el DataGridView ya está agregado, no lo creamos de nuevo
            if (dataGridView == null)
            {
                // Crear DataGridView
                dataGridView = new DataGridView
                {
                    Location = new System.Drawing.Point(50, 200),  // Ajusta la posición según lo necesites
                    Size = new System.Drawing.Size(700, 150),  // Ajusta el tamaño según lo necesites
                    ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                    AllowUserToAddRows = false,
                    ReadOnly = true
                };

                // Agregar columnas
                dataGridView.Columns.Add("Numero", "N°");
                dataGridView.Columns.Add("Documento", "DOCUMENTO");
                dataGridView.Columns.Add("Nombres", "NOMBRES");
                dataGridView.Columns.Add("ApellidoPaterno", "APELLIDO PATERNO");
                dataGridView.Columns.Add("ApellidoMaterno", "APELLIDO MATERNO");
                dataGridView.Columns.Add("Cargo", "CARGO");
                dataGridView.Columns.Add("CodigoCargo", "CÓDIGO DE CARGO");
                dataGridView.Columns.Add("Situacion", "SITUACIÓN");

                // Agregar una fila de ejemplo
                dataGridView.Rows.Add("1", "12345678", "Juan", "Pérez", "Gómez", "Analista", "00001", "Activo");
            }

            // Agregar el DataGridView al panel de búsqueda si aún no está agregado
            if (!panelBusqueda.Controls.Contains(dataGridView))
            {
                panelBusqueda.Controls.Add(dataGridView);
            }
        }

        // Evento para el botón "Nuevo"
        // Evento para el botón "Nuevo"
        // Evento para el botón "Nuevo"
        // Evento para el botón "Nuevo"
        // Evento para el botón "Nuevo"
        // Evento para el botón "Nuevo"
        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            Form ventanaNuevo = new Form
            {
                Text = "Nuevo Registro de Empleado",
                Size = new System.Drawing.Size(800, 1050),
                StartPosition = FormStartPosition.CenterScreen
            };

            // Crear y organizar las etiquetas y campos para los datos
            Label lblTipoDoc = new Label { Text = "Tipo Doc:", Location = new System.Drawing.Point(20, 20), AutoSize = true };
            ComboBox cbTipoDoc = new ComboBox { Location = new System.Drawing.Point(120, 20), Width = 150 };
            cbTipoDoc.Items.AddRange(new string[] { "DNI", "Lib. Electoral", "Pasaporte" });

            Label lblDocumento = new Label { Text = "Documento:", Location = new System.Drawing.Point(20, 60), AutoSize = true };
            TextBox txtDocumento = new TextBox { Location = new System.Drawing.Point(120, 60), Width = 150 };

            Label lblApellidoPaterno = new Label { Text = "Apellido Paterno:", Location = new System.Drawing.Point(20, 100), AutoSize = true };
            TextBox txtApellidoPaterno = new TextBox { Location = new System.Drawing.Point(120, 100), Width = 150 };

            Label lblApellidoMaterno = new Label { Text = "Apellido Materno:", Location = new System.Drawing.Point(20, 140), AutoSize = true };
            TextBox txtApellidoMaterno = new TextBox { Location = new System.Drawing.Point(120, 140), Width = 150 };

            Label lblNombre = new Label { Text = "Nombres:", Location = new System.Drawing.Point(20, 180), AutoSize = true };
            TextBox txtNombre = new TextBox { Location = new System.Drawing.Point(120, 180), Width = 150 };

            Label lblRegimen = new Label { Text = "Régimen:", Location = new System.Drawing.Point(20, 220), AutoSize = true };
            ComboBox cbRegimen = new ComboBox { Location = new System.Drawing.Point(120, 220), Width = 150 };
            cbRegimen.Items.AddRange(new string[] { "Cas", "Nombrado", "Tercero" });

            Label lblCodigoNexus = new Label { Text = "Código Nexus:", Location = new System.Drawing.Point(20, 260), AutoSize = true };
            TextBox txtCodigoNexus = new TextBox { Location = new System.Drawing.Point(120, 260), Width = 150 };

            Label lblCodigoAirhsp = new Label { Text = "Código AIRHSP:", Location = new System.Drawing.Point(20, 300), AutoSize = true };
            TextBox txtCodigoAirhsp = new TextBox { Location = new System.Drawing.Point(120, 300), Width = 150 };

            Label lblSexo = new Label { Text = "Sexo:", Location = new System.Drawing.Point(20, 340), AutoSize = true };
            RadioButton rbMasculino = new RadioButton { Text = "Masculino", Location = new System.Drawing.Point(120, 340) };
            RadioButton rbFemenino = new RadioButton { Text = "Femenino", Location = new System.Drawing.Point(200, 340) };

            Label lblRegimenPensionario = new Label { Text = "Régimen Pensionario:", Location = new System.Drawing.Point(20, 380), AutoSize = true };
            ComboBox cbRegimenPensionario = new ComboBox { Location = new System.Drawing.Point(160, 380), Width = 150 };
            cbRegimenPensionario.Items.AddRange(new string[] { "AFP", "ONP", "Retiro 95%", "Pensión de Invalidez", "Pensionista ONP" });

            Label lblTipoAfp = new Label { Text = "Tipo AFP:", Location = new System.Drawing.Point(20, 420), AutoSize = true };
            ComboBox cbTipoAfp = new ComboBox { Location = new System.Drawing.Point(160, 420), Width = 150 };
            cbTipoAfp.Items.AddRange(new string[] { "AFP Integra", "AFP Prima", "AFP Habitat", "AFP Profuturo" });

            Label lblTipoComision = new Label { Text = "Tipo de Comisión:", Location = new System.Drawing.Point(20, 460), AutoSize = true };
            ComboBox cbTipoComision = new ComboBox { Location = new System.Drawing.Point(160, 460), Width = 150 };
            cbTipoComision.Items.AddRange(new string[] { "Comisión por Flujo", "Comisión Mixta" });

            Label lblCussp = new Label { Text = "CUSPP:", Location = new System.Drawing.Point(20, 500), AutoSize = true };
            TextBox txtCussp = new TextBox { Location = new System.Drawing.Point(160, 500), Width = 150 };

            // Cambiar formato de Fecha de Inicio y Término
            Label lblFechaInicio = new Label { Text = "Fecha de Inicio:", Location = new System.Drawing.Point(20, 540), AutoSize = true };
            DateTimePicker dtpFechaInicio = new DateTimePicker { Location = new System.Drawing.Point(160, 540), Width = 150, Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy" };

            Label lblFechaTermino = new Label { Text = "Fecha de Término:", Location = new System.Drawing.Point(20, 580), AutoSize = true };
            DateTimePicker dtpFechaTermino = new DateTimePicker { Location = new System.Drawing.Point(160, 580), Width = 150, Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy" };

            // Nuevos campos agregados
            Label lblEstablecimiento = new Label { Text = "Establecimiento:", Location = new System.Drawing.Point(20, 620), AutoSize = true };
            TextBox txtEstablecimiento = new TextBox { Location = new System.Drawing.Point(160, 620), Width = 150 };

            Label lblCodigoModIe = new Label { Text = "Código MOD I.E.:", Location = new System.Drawing.Point(20, 660), AutoSize = true };
            TextBox txtCodigoModIe = new TextBox { Location = new System.Drawing.Point(160, 660), Width = 150 };

            Label lblJornadaLaboral = new Label { Text = "Jornada Laboral:", Location = new System.Drawing.Point(20, 700), AutoSize = true };
            TextBox txtJornadaLaboral = new TextBox { Location = new System.Drawing.Point(160, 700), Width = 150 };

            Label lblClasificador = new Label { Text = "Clasificador:", Location = new System.Drawing.Point(20, 740), AutoSize = true };
            ComboBox cbClasificador = new ComboBox { Location = new System.Drawing.Point(160, 740), Width = 150 };
            cbClasificador.Items.AddRange(new string[] { "23.12.11", "23.12.12", "23.12.14", "23.12.15" });

            Label lblMeta = new Label { Text = "Meta:", Location = new System.Drawing.Point(20, 780), AutoSize = true };
            TextBox txtMeta = new TextBox { Location = new System.Drawing.Point(160, 780), Width = 150 };

            // Botones en la parte inferior
            Button btnGrabar = new Button { Text = "Grabar", Location = new System.Drawing.Point(20, 960), AutoSize = true };
            Button btnActualizar = new Button { Text = "Actualizar", Location = new System.Drawing.Point(120, 960), AutoSize = true };
            Button btnHaberes = new Button { Text = "Haberes", Location = new System.Drawing.Point(220, 960), AutoSize = true };
            Button btnDescuentos = new Button { Text = "Descuentos", Location = new System.Drawing.Point(320, 960), AutoSize = true };
            Button btnBoleta = new Button { Text = "Boleta", Location = new System.Drawing.Point(420, 960), AutoSize = true };
            Button btnSalir = new Button { Text = "Salir", Location = new System.Drawing.Point(520, 960), AutoSize = true };
            btnSalir.Click += (s, eventArgs) => ventanaNuevo.Close();

            // Agregar controles a la ventana
            ventanaNuevo.Controls.Add(lblTipoDoc);
            ventanaNuevo.Controls.Add(cbTipoDoc);
            ventanaNuevo.Controls.Add(lblDocumento);
            ventanaNuevo.Controls.Add(txtDocumento);
            ventanaNuevo.Controls.Add(lblApellidoPaterno);
            ventanaNuevo.Controls.Add(txtApellidoPaterno);
            ventanaNuevo.Controls.Add(lblApellidoMaterno);
            ventanaNuevo.Controls.Add(txtApellidoMaterno);
            ventanaNuevo.Controls.Add(lblNombre);
            ventanaNuevo.Controls.Add(txtNombre);
            ventanaNuevo.Controls.Add(lblRegimen);
            ventanaNuevo.Controls.Add(cbRegimen);
            ventanaNuevo.Controls.Add(lblCodigoNexus);
            ventanaNuevo.Controls.Add(txtCodigoNexus);
            ventanaNuevo.Controls.Add(lblCodigoAirhsp);
            ventanaNuevo.Controls.Add(txtCodigoAirhsp);
            ventanaNuevo.Controls.Add(lblSexo);
            ventanaNuevo.Controls.Add(rbMasculino);
            ventanaNuevo.Controls.Add(rbFemenino);

            ventanaNuevo.Controls.Add(lblRegimenPensionario);
            ventanaNuevo.Controls.Add(cbRegimenPensionario);
            ventanaNuevo.Controls.Add(lblTipoAfp);
            ventanaNuevo.Controls.Add(cbTipoAfp);
            ventanaNuevo.Controls.Add(lblTipoComision);
            ventanaNuevo.Controls.Add(cbTipoComision);
            ventanaNuevo.Controls.Add(lblCussp);
            ventanaNuevo.Controls.Add(txtCussp);
            ventanaNuevo.Controls.Add(lblFechaInicio);
            ventanaNuevo.Controls.Add(dtpFechaInicio);
            ventanaNuevo.Controls.Add(lblFechaTermino);
            ventanaNuevo.Controls.Add(dtpFechaTermino);
            ventanaNuevo.Controls.Add(lblEstablecimiento);
            ventanaNuevo.Controls.Add(txtEstablecimiento);
            ventanaNuevo.Controls.Add(lblCodigoModIe);
            ventanaNuevo.Controls.Add(txtCodigoModIe);
            ventanaNuevo.Controls.Add(lblJornadaLaboral);
            ventanaNuevo.Controls.Add(txtJornadaLaboral);
            ventanaNuevo.Controls.Add(lblClasificador);
            ventanaNuevo.Controls.Add(cbClasificador);
            ventanaNuevo.Controls.Add(lblMeta);
            ventanaNuevo.Controls.Add(txtMeta);

            ventanaNuevo.Controls.Add(btnGrabar);
            ventanaNuevo.Controls.Add(btnActualizar);
            ventanaNuevo.Controls.Add(btnHaberes);
            ventanaNuevo.Controls.Add(btnDescuentos);
            ventanaNuevo.Controls.Add(btnBoleta);
            ventanaNuevo.Controls.Add(btnSalir);

            ventanaNuevo.ShowDialog();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            // Lógica para listar empleados
            MessageBox.Show("Lista de empleados.");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Lógica para guardar empleados
            MessageBox.Show("Empleado guardado correctamente.");
        }
    }
}
