using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Gestão_Escolar
{
    public partial class FormConsultaNotasPresencas : Form
    {
        private GestorEscola gestor;

        public FormConsultaNotasPresencas(GestorEscola gestor)
        {
            InitializeComponent();
            this.gestor = gestor;
            //CarregarAlunos();
        }

        private void CarregarAlunos()
        {
            cmbAluno.DataSource = gestor.Alunos;
            cmbAluno.DisplayMember = "Nome";
            cmbAluno.ValueMember = "Id";
        }

        private void cmbAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            //    if (cmbAluno.SelectedValue is int alunoId)
            //    {
            //        CarregarNotas(alunoId);
            //        CarregarPresencas(alunoId);
            //    }
        }

        //private void CarregarNotas(int alunoId)
        //{
        //    dgvNotas.Rows.Clear();
        //    var notas = gestor.ObterNotasAluno(alunoId);

        //    foreach (var nota in notas)
        //    {
        //        dgvNotas.Rows.Add(nota.Disciplina, nota.Valor, nota.Feedback);
        //    }
        //}

        //private void CarregarPresencas(int alunoId)
        //{
        //    dgvPresencas.Rows.Clear();
        //    var presencas = gestor.ObterPresencasAluno(alunoId);

        //    foreach (var presenca in presencas)
        //    {
        //        dgvPresencas.Rows.Add(presenca.Data.ToString("dd/MM/yyyy"), presenca.Disciplina, presenca.Status);
        //    }
        //}

    }
}

