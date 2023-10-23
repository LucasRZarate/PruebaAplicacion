namespace AccessLibreria
{
    partial class Logging
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnIniciarSesion = new System.Windows.Forms.Button();
            this.btnRegistro = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInicioDNI = new System.Windows.Forms.TextBox();
            this.txtInicioPassword = new System.Windows.Forms.TextBox();
            this.txtRegPassword = new System.Windows.Forms.TextBox();
            this.txtRegDNI = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRegApellidos = new System.Windows.Forms.TextBox();
            this.txtRegNombre = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRegEdad = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbRegAdmin = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnIniciarSesion
            // 
            this.btnIniciarSesion.Location = new System.Drawing.Point(120, 367);
            this.btnIniciarSesion.Name = "btnIniciarSesion";
            this.btnIniciarSesion.Size = new System.Drawing.Size(124, 23);
            this.btnIniciarSesion.TabIndex = 0;
            this.btnIniciarSesion.Text = "Iniciar Sesion";
            this.btnIniciarSesion.UseVisualStyleBackColor = true;
            this.btnIniciarSesion.Click += new System.EventHandler(this.btnIniciarSesion_Click);
            // 
            // btnRegistro
            // 
            this.btnRegistro.Location = new System.Drawing.Point(548, 367);
            this.btnRegistro.Name = "btnRegistro";
            this.btnRegistro.Size = new System.Drawing.Size(126, 23);
            this.btnRegistro.TabIndex = 1;
            this.btnRegistro.Text = "Registrar";
            this.btnRegistro.UseVisualStyleBackColor = true;
            this.btnRegistro.Click += new System.EventHandler(this.btnRegistro_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtInicioPassword);
            this.groupBox1.Controls.Add(this.txtInicioDNI);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(39, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 258);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inicio sesion";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbRegAdmin);
            this.groupBox2.Controls.Add(this.txtRegEdad);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtRegApellidos);
            this.groupBox2.Controls.Add(this.txtRegNombre);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtRegPassword);
            this.groupBox2.Controls.Add(this.txtRegDNI);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(448, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(300, 258);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Registro";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "DNI:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Contraseña:";
            // 
            // txtInicioDNI
            // 
            this.txtInicioDNI.Location = new System.Drawing.Point(117, 77);
            this.txtInicioDNI.Name = "txtInicioDNI";
            this.txtInicioDNI.Size = new System.Drawing.Size(100, 20);
            this.txtInicioDNI.TabIndex = 2;
            // 
            // txtInicioPassword
            // 
            this.txtInicioPassword.Location = new System.Drawing.Point(117, 170);
            this.txtInicioPassword.Name = "txtInicioPassword";
            this.txtInicioPassword.Size = new System.Drawing.Size(100, 20);
            this.txtInicioPassword.TabIndex = 3;
            this.txtInicioPassword.UseSystemPasswordChar = true;
            // 
            // txtRegPassword
            // 
            this.txtRegPassword.Location = new System.Drawing.Point(100, 79);
            this.txtRegPassword.Name = "txtRegPassword";
            this.txtRegPassword.Size = new System.Drawing.Size(106, 20);
            this.txtRegPassword.TabIndex = 7;
            // 
            // txtRegDNI
            // 
            this.txtRegDNI.Location = new System.Drawing.Point(100, 40);
            this.txtRegDNI.Name = "txtRegDNI";
            this.txtRegDNI.Size = new System.Drawing.Size(106, 20);
            this.txtRegDNI.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Contraseña:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "DNI:";
            // 
            // txtRegApellidos
            // 
            this.txtRegApellidos.Location = new System.Drawing.Point(100, 155);
            this.txtRegApellidos.Name = "txtRegApellidos";
            this.txtRegApellidos.Size = new System.Drawing.Size(142, 20);
            this.txtRegApellidos.TabIndex = 11;
            // 
            // txtRegNombre
            // 
            this.txtRegNombre.Location = new System.Drawing.Point(100, 116);
            this.txtRegNombre.Name = "txtRegNombre";
            this.txtRegNombre.Size = new System.Drawing.Size(142, 20);
            this.txtRegNombre.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Nombre:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 158);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Apellidos:";
            // 
            // txtRegEdad
            // 
            this.txtRegEdad.Location = new System.Drawing.Point(100, 192);
            this.txtRegEdad.Name = "txtRegEdad";
            this.txtRegEdad.Size = new System.Drawing.Size(45, 20);
            this.txtRegEdad.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(45, 195);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Edad:";
            // 
            // cbRegAdmin
            // 
            this.cbRegAdmin.AutoSize = true;
            this.cbRegAdmin.Location = new System.Drawing.Point(100, 225);
            this.cbRegAdmin.Name = "cbRegAdmin";
            this.cbRegAdmin.Size = new System.Drawing.Size(89, 17);
            this.cbRegAdmin.TabIndex = 15;
            this.cbRegAdmin.Text = "Administrador";
            this.cbRegAdmin.UseVisualStyleBackColor = true;
            // 
            // Logging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRegistro);
            this.Controls.Add(this.btnIniciarSesion);
            this.Name = "Logging";
            this.Text = "Programa Libreria Access";
            this.Load += new System.EventHandler(this.Logging_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnIniciarSesion;
        private System.Windows.Forms.Button btnRegistro;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtInicioPassword;
        private System.Windows.Forms.TextBox txtInicioDNI;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbRegAdmin;
        private System.Windows.Forms.TextBox txtRegEdad;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRegApellidos;
        private System.Windows.Forms.TextBox txtRegNombre;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRegPassword;
        private System.Windows.Forms.TextBox txtRegDNI;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}